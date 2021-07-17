using Dalamud.Game.ClientState.Structs.JobGauge;

namespace XIVComboExpandedPlugin.Combos
{
    internal static class SCH
    {
        public const byte JobID = 28;

        public const uint
            FeyBless = 16543,
            Consolation = 16546,
            EnergyDrain = 167,
            Bio = 17864,
            Bio2 = 17865,
            Biolysis = 16540,
            Ruin = 17869,
            Ruin2 = 17870,
            Broil = 3584,
            Broil2 = 7435,
            Broil3 = 16541,
            Physick = 190,
            Adloquium = 185,
            Succor = 186,
            Lustrate = 189,
            Excogitation = 7434,
            Indomitability = 3583,
            WhisperingDawn = 16537,
            LucidDreaming =7562,
            Aetherflow = 166;

        public static class Buffs
        {
            public const short 
                Galvanize = 297;
        }

        public static class Debuffs
        {
            public const short 
                Bio = 179,
                Bio2 = 189,
                Biolysis = 1895;
        }

        public static class Levels
        {
            public const byte 
                Bio = 2,
                Bio2 = 26,
                Biolysis = 72,
                Broil = 52,
                Broi2 = 64,
                Broi3 = 72,
                Adloquium = 30,
                Succor = 35,
                Ruin2 = 38,
                WhisperingDawn = 20,
                Indomitability = 52,
                Excogitation = 62;
        }
    }

    internal class ScholarSeraphConsolationFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ScholarSeraphConsolationFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SCH.FeyBless)
            {
                var gauge = GetJobGauge<SCHGauge>();
                if (gauge.SeraphTimer > 0)
                    return SCH.Consolation;
            }

            return actionID;
        }
    }

    internal class ScholarEnergyDrainFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ScholarEnergyDrainFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SCH.EnergyDrain)
            {
                if (!GetCooldown(SCH.LucidDreaming).IsCooldown)
                    return SCH.LucidDreaming;  
                var gauge = GetJobGauge<SCHGauge>();
                if (gauge.NumAetherflowStacks == 0)
                    return SCH.Aetherflow;
            }

            return actionID;
        }
    }
    
    internal class AdloSaver : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AdloSaver;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SCH.Adloquium)
            {

                if (TargetHasEffect(SCH.Buffs.Galvanize) || level < SCH.Levels.Adloquium || LocalPlayer.CurrentMp < 1000)
                    return SCH.Physick;
                return SCH.Adloquium;
            }
            return actionID;
        }
    }
    
    internal class SingleTargetGCD : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SingleTargetGCD;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SCH.Biolysis || actionID == SCH.Bio2 || actionID == SCH.Bio)
            {
                if (level >= SCH.Levels.Biolysis)
                {
                    if (DebuffDuration(SCH.Debuffs.Biolysis) < 5)
                        return OriginalHook(SCH.Bio);
                    return OriginalHook(SCH.Ruin);
                }
                if (level >= SCH.Levels.Bio2)
                {
                    if (DebuffDuration(SCH.Debuffs.Bio2) < 5)
                        return OriginalHook(SCH.Bio);
                    return OriginalHook(SCH.Ruin);
                }
                if (DebuffDuration(SCH.Debuffs.Bio) < 5)
                    return OriginalHook(SCH.Bio);
                return OriginalHook(SCH.Ruin);
            }
            return actionID;
        }
    }
    
    internal class SingleTargetoGCD : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SingleTargetoGCD;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SCH.Lustrate)
            {
                var gauge = GetJobGauge<SCHGauge>();
                if (gauge.NumAetherflowStacks == 0 && !GetCooldown(SCH.Aetherflow).IsCooldown)
                    return SCH.Aetherflow;
                if (!GetCooldown(SCH.LucidDreaming).IsCooldown)
                    return SCH.LucidDreaming;              
                if (level >= SCH.Levels.Excogitation && !GetCooldown(SCH.Excogitation).IsCooldown)
                    return SCH.Excogitation;
                return SCH.Lustrate;
            }
            return actionID;
        }
    }
    
    internal class AoEoGCD : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AoEoGCD;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SCH.Indomitability)
            {
                if (level < SCH.Levels.Succor || !GetCooldown(SCH.WhisperingDawn).IsCooldown)
                    return SCH.WhisperingDawn;
                var gauge = GetJobGauge<SCHGauge>();
                if (gauge.NumAetherflowStacks == 0)
                    return SCH.Aetherflow;
//                 if (level >= SCH.Levels.Indomitability && !GetCooldown(SCH.Indomitability).IsCooldown && gauge.NumAetherflowStacks > 0)
//                     return SCH.Indomitability;
                return SCH.Indomitability;
            }
            return actionID;
        }
    }    
}
