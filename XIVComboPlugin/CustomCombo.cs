using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Actors.Types;
using System.Linq;
using Structs = Dalamud.Game.ClientState.Structs;

namespace XIVComboExpandedPlugin.Combos
{
    internal abstract class CustomCombo
    {
        #region static 

        private static IconReplacer IconReplacer;
        protected static XIVComboExpandedConfiguration Configuration;

        public static void Initialize(IconReplacer iconReplacer, XIVComboExpandedConfiguration configuration)
        {
            IconReplacer = iconReplacer;
            Configuration = configuration;
        }

        #endregion

        protected abstract CustomComboPreset Preset { get; }

        protected byte JobID { get; set; }

        protected virtual uint[] ActionIDs { get; set; }

        protected CustomCombo()
        {
            var presetInfo = Preset.GetInfo();
            JobID = presetInfo.JobID;
            ActionIDs = presetInfo.ActionIDs;
        }

        public bool TryInvoke(uint actionID, uint lastComboMove, float comboTime, byte level, out uint newActionID)
        {
            newActionID = 0;

            if (!IsEnabled(Preset))
                return false;

            if (JobID != LocalPlayer.ClassJob.Id || !ActionIDs.Contains(actionID))
                return false;

            var resultingActionID = Invoke(actionID, lastComboMove, comboTime, level);
            if (resultingActionID == 0 || actionID == resultingActionID)
                return false;

            newActionID = resultingActionID;
            return true;
        }

        protected abstract uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level);

        #region Passthru

        public float BuffDuration(short effectId)
        {
            var buff = FindEffect(effectId);
            if (buff.HasValue)
                return (byte)buff?.Duration;
            return 0;
        }
        public float DebuffDuration(short effectId)
        {
            var debuff = FindTargetEffect(effectId);
            if (debuff.HasValue)
                return (byte)debuff?.Duration;
            return 0;
        }

        protected uint OriginalHook(uint actionID) => IconReplacer.OriginalHook(actionID);

        protected PlayerCharacter LocalPlayer => IconReplacer.LocalPlayer;

        protected Actor CurrentTarget => IconReplacer.CurrentTarget;

        protected bool IsEnabled(CustomComboPreset preset) => Configuration.IsEnabled(preset);

        protected bool HasCondition(ConditionFlag flag) => IconReplacer.HasCondition(flag);

        protected bool HasEffect(short effectID) => IconReplacer.HasEffect(effectID);

        protected bool TargetHasEffect(short effectID) => IconReplacer.TargetHasEffect(effectID);

        protected Structs.StatusEffect? FindEffect(short effectId) => IconReplacer.FindEffect(effectId);

        protected Structs.StatusEffect? FindTargetEffect(short effectId) => IconReplacer.FindTargetEffect(effectId);

        protected CooldownData GetCooldown(uint actionID) => IconReplacer.GetCooldown(actionID);

        protected T GetJobGauge<T>() => IconReplacer.GetJobGauge<T>();

        #endregion
    }
}
