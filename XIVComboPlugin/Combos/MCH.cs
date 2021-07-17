using Dalamud.Game.ClientState.Structs.JobGauge;

namespace XIVComboExpandedPlugin.Combos
{
    internal static class MCH
    {
        public const byte JobID = 31;

        public const uint
            CleanShot = 2873,
            HeatedCleanShot = 7413,
            SplitShot = 2866,
            HeatedSplitShot = 7411,
            SlugShot = 2868,
            GaussRound = 2874,
            Ricochet = 2890,
            HeatedSlugshot = 7412,
            Hypercharge = 17209,
            HeatBlast = 7410,
            SpreadShot = 2870,
            AutoCrossbow = 16497,
            Reassemble = 2876,
            Drill = 16498,
            AirAnchor = 16500,
            BarrelStabilizer = 7414,
            BioBlaster = 16499,
            Wildfire = 2878,
            RookAutoturret = 2864,
            RookOverdrive = 7415,
            AutomatonQueen = 16501,
            QueenOverdrive = 16502;

        public static class Buffs
        {
            public const short
                Reassemble = 851;
        }

        public static class Debuffs
        {
            // public const short placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                SlugShot = 2,
                GaussRound = 15,
                CleanShot = 26,
                Hypercharge = 30,
                HeatBlast = 35,
                RookOverdrive = 40,
                Wildfire = 45,
                Ricochet = 50,
                AutoCrossbow = 52,
                HeatedSplitShot = 54,
                Drill = 58,
                HeatedSlugshot = 60,
                HeatedCleanShot = 64,
                BarrelStabilizer = 66,
                BioBlaster = 72,
                ChargedActionMastery = 74,
                AirAnchor = 76,
                QueenOverdrive = 80;
        }
    }

    // [E]
    internal class MachinistMainCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.MachinistMainCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == MCH.CleanShot || actionID == MCH.HeatedCleanShot)
            {
                var gauge = GetJobGauge<MCHGauge>();
                if (gauge.IsOverheated() && level >= MCH.Levels.HeatBlast)
                    return MCH.HeatBlast;
                    
                if (!GetCooldown(MCH.AirAnchor).IsCooldown && level >= MCH.Levels.AirAnchor && GetCooldown(MCH.Reassemble).IsCooldown)
                    return MCH.AirAnchor;
                if (!GetCooldown(MCH.Drill).IsCooldown && level >= MCH.Levels.Drill && GetCooldown(MCH.Reassemble).IsCooldown)
                    return MCH.Drill;

                if (comboTime > 0)
                {
                    if (lastComboMove == MCH.SplitShot && level >= MCH.Levels.SlugShot)
                        return OriginalHook(MCH.SlugShot);

                    if (lastComboMove == MCH.SlugShot && level >= MCH.Levels.CleanShot)
                        return OriginalHook(MCH.CleanShot);
                }

                return OriginalHook(MCH.SplitShot);
            }

            return actionID;
        }
    }

    // [oGCD]
    internal class MachinistGaussRoundRicochetFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.MachinistGaussRoundRicochetFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == MCH.GaussRound || actionID == MCH.Ricochet)
            {
                var gauge = GetJobGauge<MCHGauge>();
                if (gauge.IsOverheated())
                {
                    if (!GetCooldown(MCH.Wildfire).IsCooldown && level >= MCH.Levels.Wildfire)
                        return MCH.Wildfire;                
                }
                else
                {
                    if (!GetCooldown(MCH.Reassemble).IsCooldown && (!GetCooldown(MCH.Drill).IsCooldown || !GetCooldown(MCH.AirAnchor).IsCooldown))
                        return MCH.Reassemble;
                    if (!GetCooldown(MCH.BarrelStabilizer).IsCooldown && level >= MCH.Levels.BarrelStabilizer && gauge.Heat <= 50)
                        return MCH.BarrelStabilizer;                
                }
                
                var gaussCd = GetCooldown(MCH.GaussRound);
                var ricochetCd = GetCooldown(MCH.Ricochet);

                // Prioritize the original if both are off cooldown
                if (!gaussCd.IsCooldown && !ricochetCd.IsCooldown)
                    return actionID;

                if (gaussCd.CooldownRemaining < ricochetCd.CooldownRemaining)
                    return MCH.GaussRound;
                else
                    return MCH.Ricochet;
            }

            return actionID;
        }
    }

    // [R]
    internal class MachinistOverheatFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.MachinistOverheatFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == MCH.HeatBlast || actionID == MCH.AutoCrossbow)
            {
                var gauge = GetJobGauge<MCHGauge>();
                if (level >= MCH.Levels.Hypercharge && gauge.Heat > 50)
                    return MCH.Hypercharge;
            }

            return actionID;
        }
    }
    // [C]
    internal class MachinistSpreadShotFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.MachinistSpreadShotFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == MCH.SpreadShot)
            {
                if (!GetCooldown(MCH.BioBlaster).IsCooldown && level >= MCH.Levels.BioBlaster)
                    return MCH.BioBlaster;
                var gauge = GetJobGauge<MCHGauge>();
                if (gauge.IsOverheated() && level >= MCH.Levels.AutoCrossbow)
                    return MCH.AutoCrossbow;
            }

            return actionID;
        }
    }
    // [F]
    internal class MachinistOverdriveFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.MachinistOverdriveFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == MCH.RookAutoturret || actionID == MCH.AutomatonQueen)
            {
                var gauge = GetJobGauge<MCHGauge>();
                if (gauge.IsRobotActive())
                    return OriginalHook(MCH.QueenOverdrive);
            }

            return actionID;
        }
    }
}
