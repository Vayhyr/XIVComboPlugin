using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Dalamud.Game.ClientState.Actors.Types;
using Dalamud.Game.Text;
using Dalamud.Hooking;
using Dalamud.Plugin;
using Structs = Dalamud.Game.ClientState.Structs;
using XIVComboExpandedPlugin.Combos;
using Dalamud.Game.ClientState;
using System.Reflection;

namespace XIVComboExpandedPlugin
{
    internal class IconReplacer
    {
        private readonly DalamudPluginInterface Interface;
        private readonly PluginAddressResolver Address;
        private readonly XIVComboExpandedConfiguration Configuration;

        private delegate ulong IsIconReplaceableDelegate(uint actionID);
        private delegate uint GetIconDelegate(IntPtr actionManager, uint actionID);
        private delegate IntPtr GetActionCooldownSlotDelegate(IntPtr actionManager, int cooldownGroup);

        private readonly Hook<IsIconReplaceableDelegate> IsIconReplaceableHook;
        private readonly Hook<GetIconDelegate> GetIconHook;

        private readonly GetActionCooldownSlotDelegate GetActionCooldownSlot;
        private IntPtr ActionManager = IntPtr.Zero;

        private readonly HashSet<uint> CustomIds = new();
        private List<CustomCombo> CustomCombos;

        public IconReplacer(DalamudPluginInterface pluginInterface, XIVComboExpandedConfiguration configuration)
        {
            Interface = pluginInterface;
            Configuration = configuration;

            Address = new PluginAddressResolver();
            Address.Setup(pluginInterface.TargetModuleScanner);

            CustomCombo.Initialize(this, configuration);
            UpdateCustomCombos();

            UpdateEnabledActionIDs();

            GetActionCooldownSlot = Marshal.GetDelegateForFunctionPointer<GetActionCooldownSlotDelegate>(Address.GetActionCooldown);

            GetIconHook = new Hook<GetIconDelegate>(Address.GetIcon, GetIconDetour);
            IsIconReplaceableHook = new Hook<IsIconReplaceableDelegate>(Address.IsIconReplaceable, IsIconReplaceableDetour);

            GetIconHook.Enable();
            IsIconReplaceableHook.Enable();

        }

        internal void Dispose()
        {
            GetIconHook.Dispose();
            IsIconReplaceableHook.Dispose();
        }

        private void UpdateCustomCombos()
        {
            CustomCombos = Assembly.GetAssembly(typeof(CustomCombo)).GetTypes()
                .Where(t => t.BaseType == typeof(CustomCombo))
                .Select(t => Activator.CreateInstance(t))
                .Cast<CustomCombo>()
                .ToList();
        }

        /// <summary>
        /// Maps to <see cref="XIVComboExpandedConfiguration.EnabledActions"/>, these actions can potentially update their icon per the user configuration.
        /// </summary>
        public void UpdateEnabledActionIDs()
        {
            var actionIDs = Enum
                .GetValues(typeof(CustomComboPreset))
                .Cast<CustomComboPreset>()
                .Select(preset => preset.GetAttribute<CustomComboInfoAttribute>())
                .OfType<CustomComboInfoAttribute>()
                .SelectMany(comboInfo => comboInfo.ActionIDs)
                .Concat(Configuration.DancerDanceCompatActionIDs)
                .ToHashSet();
            CustomIds.Clear();
            CustomIds.UnionWith(actionIDs);
        }

        public uint GetNewAction(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            foreach (var combo in CustomCombos)
            {
                if (combo.TryInvoke(actionID, lastComboMove, comboTime, level, out var newActionID))
                    return newActionID;
            }
            return OriginalHook(actionID);
        }

        private ulong IsIconReplaceableDetour(uint actionID) => 1;

        /// <summary>
        /// Replace an ability with another ability
        /// actionID is the original ability to be "used"
        /// Return either actionID (itself) or a new Action table ID as the
        /// ability to take its place.
        /// I tend to make the "combo chain" button be the last move in the combo
        /// For example, Souleater combo on DRK happens by dragging Souleater
        /// onto your bar and mashing it.
        /// </summary>
        private uint GetIconDetour(IntPtr actionManager, uint actionID)
        {
            try
            {
                ActionManager = actionManager;

                if (LocalPlayer == null || !CustomIds.Contains(actionID))
                    return OriginalHook(actionID);

                return GetNewAction(actionID, LastComboMove, ComboTime, LocalPlayer.Level);
            }
            catch (Exception ex)
            {
                PluginLog.Error(ex, "Don't crash the game");
                return GetIconHook.Original(actionManager, actionID);
            }
        }

        #region Getters

        internal bool HasCondition(ConditionFlag flag) => Interface.ClientState.Condition[flag];

        internal PlayerCharacter LocalPlayer => Interface.ClientState.LocalPlayer;

        internal Actor CurrentTarget => Interface.ClientState.Targets.CurrentTarget;

        internal uint LastComboMove => (uint)Marshal.ReadInt32(Address.LastComboMove);

        internal float ComboTime => Marshal.PtrToStructure<float>(Address.ComboTimer);

        internal T GetJobGauge<T>() => Interface.ClientState.JobGauges.Get<T>();

        internal uint OriginalHook(uint actionID) => GetIconHook.Original(ActionManager, actionID);

        #endregion

        #region Effects

        private float BuffDuration(short effectId)
        {
            var buff = FindEffect(effectId);
            if (buff.HasValue)
                return (byte)buff?.Duration;
            return 0;
        }
        private float DebuffDuration(short effectId)
        {
            var debuff = FindTargetEffect(effectId);
            if (debuff.HasValue)
                return (byte)debuff?.Duration;
            return 0;
        }
        internal bool HasEffect(short effectId) => FindEffect(effectId) != null;

        internal bool TargetHasEffect(short effectId) => FindTargetEffect(effectId) != null;

        internal Structs.StatusEffect? FindEffect(short effectId) => FindEffect(effectId, LocalPlayer, null);

        internal Structs.StatusEffect? FindTargetEffect(short effectId) => FindEffect(effectId, CurrentTarget, LocalPlayer?.ActorId);

        internal Structs.StatusEffect? FindEffect(short effectId, Actor actor, int? ownerId)
        {
            if (actor == null)
                return null;

            foreach (var status in actor.StatusEffects)
            {
                if (status.EffectId == effectId)
                    if (!ownerId.HasValue || status.OwnerId == ownerId)
                        return status;
            }

            return null;
        }

        #endregion

        #region Cooldowns

        private readonly Dictionary<uint, byte> CooldownGroups = new();

        private byte GetCooldownGroup(uint actionID)
        {
            if (CooldownGroups.TryGetValue(actionID, out var cooldownGroup))
                return cooldownGroup;

            var sheet = Interface.Data.GetExcelSheet<Lumina.Excel.GeneratedSheets.Action>();
            var row = sheet.GetRow(actionID);

            return CooldownGroups[actionID] = row.CooldownGroup;
        }

        internal CooldownData GetCooldown(uint actionID)
        {
            var cooldownGroup = GetCooldownGroup(actionID);
            if (ActionManager == IntPtr.Zero)
                return new CooldownData() { ActionID = actionID };

            var cooldownPtr = GetActionCooldownSlot(ActionManager, cooldownGroup - 1);
            return Marshal.PtrToStructure<CooldownData>(cooldownPtr);
        }

        #endregion
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct CooldownData
    {
        [FieldOffset(0x0)] public bool IsCooldown;
        [FieldOffset(0x4)] public uint ActionID;
        [FieldOffset(0x8)] public float CooldownElapsed;
        [FieldOffset(0xC)] public float CooldownTotal;

        public float CooldownRemaining => IsCooldown ? CooldownTotal - CooldownElapsed : 0;
    }
}
