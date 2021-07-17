using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Structs.JobGauge;

namespace XIVComboExpandedPlugin.Combos
{
    internal static class RDM
    {
        public const byte JobID = 35;

        public const uint
            Verthunder = 7505,
            Veraero = 7507,
            Veraero2 = 16525,
            Verthunder2 = 16524,
            Impact = 16526,
            Fleche = 7517,
            LucidDreaming = 7562,
            ContreSixte = 7519,
            Redoublement = 7516,
            EnchantedRedoublement = 7529,
            Zwerchhau = 7512,
            EnchantedZwerchhau = 7528,
            Riposte = 7504,
            EnchantedRiposte = 7527,
            Scatter = 7509,
            Verstone = 7511,
            Verfire = 7510,
            Jolt = 7503,
            Jolt2 = 7524,
            Verholy = 7526,
            Verflare = 7525,
            Scorch = 16530;

        public static class Buffs
        {
            public const short
                Swiftcast = 167,
                VerfireReady = 1234,
                VerstoneReady = 1235,
                Dualcast = 1249;
        }

        public static class Debuffs
        {
            // public const short placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                Jolt = 2,
                Verthunder = 4,
                Veraero = 10,
                Verthunder2 = 18,
                Veraero2 = 22,
                Verraise = 64,
                Zwerchhau = 35,
                Fleche = 45,
                Redoublement = 50,
                Vercure = 54,
                ContreSixte = 56,
                Jolt2 = 62,
                Impact = 66,
                Verflare = 68,
                Verholy = 70,
                Scorch = 80;
        }
    }

    internal class RedMageAoECombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.RedMageAoECombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            
            if (actionID == RDM.Veraero2)
            {
                if (HasEffect(RDM.Buffs.Swiftcast) || HasEffect(RDM.Buffs.Dualcast) || level < RDM.Levels.Verthunder2)
                    return OriginalHook(RDM.Impact);
                var gauge = GetJobGauge<RDMGauge>();
                if (level < RDM.Levels.Verthunder2)
                    return OriginalHook(RDM.Jolt2);
                if (gauge.WhiteGauge >= gauge.BlackGauge || level < RDM.Levels.Veraero2)
                    return RDM.Verthunder2;
                return RDM.Veraero2;
            }
            return actionID;
        }
    }

    internal class RedMageMeleeCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.RedMageMeleeCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == RDM.Redoublement)
            {
                var gauge = GetJobGauge<RDMGauge>();

                if (IsEnabled(CustomComboPreset.RedMageMeleeComboPlus))
                {
                    if (lastComboMove == RDM.EnchantedRedoublement)
                    {
                        if (gauge.BlackGauge > gauge.WhiteGauge && level >= RDM.Levels.Verholy)
                        {
                            if (HasEffect(RDM.Buffs.VerstoneReady) && !HasEffect(RDM.Buffs.VerfireReady) && (gauge.BlackGauge - gauge.WhiteGauge <= 9))
                                return RDM.Verflare;

                            return RDM.Verholy;
                        }
                        else if (level >= RDM.Levels.Verflare)
                        {
                            if ((!HasEffect(RDM.Buffs.VerstoneReady) && HasEffect(RDM.Buffs.VerfireReady)) && level >= RDM.Levels.Verholy && (gauge.WhiteGauge - gauge.BlackGauge <= 9))
                                return RDM.Verholy;

                            return RDM.Verflare;
                        }
                    }
                }

                if ((lastComboMove == RDM.Riposte || lastComboMove == RDM.EnchantedRiposte) && level >= RDM.Levels.Zwerchhau)
                    return OriginalHook(RDM.Zwerchhau);

                if (lastComboMove == RDM.Zwerchhau && level >= RDM.Levels.Redoublement)
                    return OriginalHook(RDM.Redoublement);

                if (IsEnabled(CustomComboPreset.RedMageMeleeComboPlus))
                {
                    if ((lastComboMove == RDM.Verflare || lastComboMove == RDM.Verholy) && level >= RDM.Levels.Scorch)
                        return RDM.Scorch;
                }

                return OriginalHook(RDM.Riposte);
            }

            return actionID;
        }
    }

    internal class RedMageVerprocCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.RedMageVerprocCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == RDM.Verstone)
            {
                var gauge = GetJobGauge<RDMGauge>();
                if (level >= RDM.Levels.Scorch && (lastComboMove == RDM.Verflare || lastComboMove == RDM.Verholy))
                    return RDM.Scorch;

                if (lastComboMove == RDM.EnchantedRedoublement)
                {
                    if (gauge.BlackGauge > gauge.WhiteGauge && level >= RDM.Levels.Verholy)
                    {
                        if (HasEffect(RDM.Buffs.VerstoneReady) && !HasEffect(RDM.Buffs.VerfireReady) && (gauge.BlackGauge - gauge.WhiteGauge <= 9))
                            return RDM.Verflare;

                        return RDM.Verholy;
                    }
                    else if (level >= RDM.Levels.Verflare)
                    {
                        if ((!HasEffect(RDM.Buffs.VerstoneReady) && HasEffect(RDM.Buffs.VerfireReady)) && level >= RDM.Levels.Verholy && (gauge.WhiteGauge - gauge.BlackGauge <= 9))
                            return RDM.Verholy;

                        return RDM.Verflare;
                    }
                }

                if (HasEffect(RDM.Buffs.Dualcast) || HasEffect(RDM.Buffs.Swiftcast))
                {
                    if (gauge.BlackGauge > gauge.WhiteGauge)
                        return RDM.Veraero;
                    return RDM.Verthunder;
                }

                if (HasEffect(RDM.Buffs.VerfireReady))
                {
                    if (HasEffect(RDM.Buffs.VerstoneReady) && (gauge.BlackGauge > gauge.WhiteGauge))
                        return RDM.Verstone;
                    return RDM.Verfire;
                }

                if (HasEffect(RDM.Buffs.VerstoneReady))
                    return RDM.Verstone;

                return OriginalHook(RDM.Jolt2);
            }
            return actionID;
        }
    }
        internal class RDMoGCD : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.RDMoGCD;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {

            if (actionID == RDM.Fleche)
            {
                if (!GetCooldown(RDM.LucidDreaming).IsCooldown && LocalPlayer.CurrentMp < 8000)
                    return SMN.LucidDreaming;
                if (!GetCooldown(RDM.ContreSixte).IsCooldown && level >= RDM.Levels.ContreSixte)
                    return OriginalHook(RDM.ContreSixte);
                if (!GetCooldown(RDM.Fleche).IsCooldown && level >= RDM.Levels.Fleche)
                    return OriginalHook(RDM.Fleche);

            }
            return actionID;
        }
    }
}
