using Dalamud.Game.ClientState.Structs.JobGauge;

namespace XIVComboExpandedPlugin.Combos
{
    internal static class DRG
    {
        public const byte JobID = 22;

        public const uint
            Jump = 92,
            HighJump = 16478,
	    SpineshatterDive = 95,
	    DragonfireDive = 96,
            MirageDive = 7399,
            BloodOfTheDragon = 3553,
            Stardiver = 16480,
            CoerthanTorment = 16477,
	    Geirskogul = 3555,
	    LifeSurge = 83,
            DoomSpike = 86,
            SonicThrust = 7397,
            ChaosThrust = 88,
            RaidenThrust = 16479,
            TrueThrust = 75,
            Disembowel = 87,
            FangAndClaw = 3554,
            WheelingThrust = 3556,
            FullThrust = 84,
            VorpalThrust = 78;

        public static class Buffs
        {
            public const short
                SharperFangAndClaw = 802,
		Disembowel = 1914,
		LifeSurge = 116,
                EnhancedWheelingThrust = 803,
                DiveReady = 1243,
                RaidenThrustReady = 1863;
        }

        public static class Debuffs
        {
            // public const short placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                VorpalThrust = 4,
                Disembowel = 18,
                FullThrust = 26,
                ChaosThrust = 50,
                FangAndClaw = 56,
                WheelingThrust = 58,
                SonicThrust = 62,
                CoerthanTorment = 72,
                HighJump = 74,
		SpineshatterDive = 45,
	    	DragonfireDive = 50,
                RaidenThrust = 76,
                Stardiver = 80;
        }
    }

    internal class DragoonJumpFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonJumpFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRG.Jump)
            {
                if (HasEffect(DRG.Buffs.DiveReady))
                    return DRG.MirageDive;

                return OriginalHook(DRG.HighJump);
            }

            return actionID;
        }
    }
    
    internal class DragoonJumpStackFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonJumpStackFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRG.DragonfireDive)
            {
                if (level >= DRG.Levels.DragonfireDive && !GetCooldown(DRG.DragonfireDive).IsCooldown)
                    return DRG.DragonfireDive;
                return DRG.SpineshatterDive;
            }

            return actionID;
        }
    }

    internal class DragoonBOTDFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonBOTDFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRG.BloodOfTheDragon)
            {
                if (level >= DRG.Levels.Stardiver)
                {
                    var gauge = GetJobGauge<DRGGauge>();
                    if (gauge.BOTDState == BOTDState.LOTD)
                        return DRG.Stardiver;
                }
            }

            return actionID;
        }
    }

    internal class DragoonCoerthanTormentCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonCoerthanTormentCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRG.CoerthanTorment)
            {
                if (comboTime > 0)
                {
                    if (lastComboMove == DRG.DoomSpike && level >= DRG.Levels.SonicThrust)
                        return DRG.SonicThrust;

                    if (lastComboMove == DRG.SonicThrust && level >= DRG.Levels.CoerthanTorment)
                        return DRG.CoerthanTorment;
                }

                return DRG.DoomSpike;
            }

            return actionID;
        }
    }

    internal class DragoonChaosThrustCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonChaosThrustCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRG.ChaosThrust)
            {
                if (comboTime > 0)
                {
                    if ((lastComboMove == DRG.TrueThrust || lastComboMove == DRG.RaidenThrust) && level >= DRG.Levels.Disembowel)
                        return DRG.Disembowel;

                    if (lastComboMove == DRG.Disembowel && level >= DRG.Levels.ChaosThrust)
                        return DRG.ChaosThrust;
                }

                if (HasEffect(DRG.Buffs.SharperFangAndClaw) && level >= DRG.Levels.FangAndClaw)
                    return DRG.FangAndClaw;

                if (HasEffect(DRG.Buffs.EnhancedWheelingThrust) && level >= DRG.Levels.WheelingThrust)
                    return DRG.WheelingThrust;

                return OriginalHook(DRG.TrueThrust);
            }

            return actionID;
        }
    }

    internal class DragoonFullThrustCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DragoonFullThrustCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRG.FullThrust)
            {
                if (comboTime > 0)
                {
                    if ((lastComboMove == DRG.TrueThrust || lastComboMove == DRG.RaidenThrust) && level >= DRG.Levels.Disembowel && BuffDuration(DRG.Buffs.Disembowel) <= 10)
                        return DRG.Disembowel;

                    if (lastComboMove == DRG.Disembowel && level >= DRG.Levels.ChaosThrust)
                        return DRG.ChaosThrust;

                    if ((lastComboMove == DRG.TrueThrust || lastComboMove == DRG.RaidenThrust) && level >= DRG.Levels.VorpalThrust)
                        return DRG.VorpalThrust;

                    if (lastComboMove == DRG.VorpalThrust && !HasEffect(DRG.Buffs.LifeSurge) && !GetCooldown(DRG.LifeSurge).IsCooldown)
                        return DRG.LifeSurge;
                    
                    if (lastComboMove == DRG.VorpalThrust && level >= DRG.Levels.FullThrust)
                        return DRG.FullThrust;
                }

                if (HasEffect(DRG.Buffs.SharperFangAndClaw) && level >= DRG.Levels.FangAndClaw)
                    return DRG.FangAndClaw;
                
                if (HasEffect(DRG.Buffs.EnhancedWheelingThrust) && level >= DRG.Levels.WheelingThrust)
                    return DRG.WheelingThrust;

                return OriginalHook(DRG.TrueThrust);
            }

            return actionID;
        }
    }
}
