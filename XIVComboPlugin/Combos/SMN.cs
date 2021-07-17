using Dalamud.Game.ClientState.Structs.JobGauge;
using Dalamud.Game.ClientState;

namespace XIVComboExpandedPlugin.Combos
{
    internal static class SMN
    {
        public const byte JobID = 27;

        public const uint
            Deathflare = 3582,
            EnkindlePhoenix = 16516,
            EnkindleBahamut = 7429,
            DreadwyrmTrance = 3581,
            SummonBahamut = 7427,
            FirebirdTranceLow = 16513,
            FirebirdTranceHigh = 16549,
            Bio = 164, 
            Bio2 = 178,  
            Bio3 = 7424, 
            Ruin1 = 163,
            Ruin2 = 172,
            Ruin3 = 3579,
            Ruin4 = 7426,
            Miasma = 168,
            Miasma3 = 7425,
            TriDisaster = 3580,
            EgiAssault = 16509,
            EgiAssault2 = 16512,
            Outburst = 16511,
            BrandOfPurgatory = 16515,
            FountainOfFire = 16514,
            Fester = 181,
            EnergyDrain = 16508,
            Painflare = 3578,
	    LucidDreaming =7562,
            EnergySyphon = 16510;

        public static class Buffs
        {
            public const short
                HellishConduit = 1867,
                FurtherRuin = 1212;
        }

        public static class Debuffs
        {
            public const short 
                Bio = 179,
                Bio2 = 189,
                Bio3 = 1214,
                Miasma = 180,
                Miasma3 = 1215;
        }

        public static class Levels
        {
            public const byte
                EgiAssault2 = 40,
                Bio2 = 26,
                Bio3 = 66,
                Miasma3 = 66,
                Painflare = 52,
                Ruin3 = 54,
                TriDisaster = 56,
                Deathflare = 60,
                EnhancedFirebirdTrance = 80;
        }
    }
// Stances [F]
    internal class SummonerDemiCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerDemiCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            //Replace DWT with demi summons
            if (actionID == SMN.DreadwyrmTrance)
            {
                var gauge = GetJobGauge<SMNGauge>();
                
                if (gauge.IsBahamutReady())
                    return SMN.SummonBahamut;

                if (gauge.IsPhoenixReady())
                    return OriginalHook(SMN.FirebirdTranceLow);

                if (gauge.TimerRemaining > 0 && gauge.TimerRemaining < 10000)
                    return SMN.Deathflare;

                return actionID;
            }

            return actionID;
        }
    }

// Single GCD1 [Q]
// If Gauge: Deathflare -> BoP -> Ruin3
// Further Ruin at 4: Ruin4
// Egi Assaults
// Ruin2

    internal class SMNSingleTargetGCD : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SMNSingleTargetGCD;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.EgiAssault)
            {
		var gauge = GetJobGauge<SMNGauge>();
		if (gauge.TimerRemaining > 0)
		{
		    if (gauge.IsPhoenixReady())
		    {
			if (HasEffect(SMN.Buffs.HellishConduit))
			    return SMN.BrandOfPurgatory;
			return SMN.FountainOfFire;
		    }
		    if (gauge.ReturnSummon != SummonPet.NONE)
			return OriginalHook(SMN.Ruin2);
		    if (gauge.TimerRemaining < 3000)
			return SMN.Deathflare;
		    return SMN.Ruin3;
		}

                if (HasCondition(ConditionFlag.InCombat))
                {
                    var Egi1Cd = GetCooldown(SMN.EgiAssault).CooldownRemaining; 
                    var Egi2Cd = GetCooldown(SMN.EgiAssault2).CooldownRemaining;
                    if (Egi1Cd  <= 30 || (Egi2Cd <= 30 && level >= SMN.Levels.EgiAssault2))
                    {
			if (Egi1Cd < Egi2Cd)
			    return OriginalHook(SMN.EgiAssault);
			else
			    return OriginalHook(SMN.EgiAssault2);
                    }
                }
                return OriginalHook(SMN.Ruin2);
            }
            return actionID;
        }
    }

// Single GCD2 [E]
// If Gauge: Deathflare -> BoP -> Ruin3
// Egi Assaults
// Ruin3

    internal class SMNSingleTargetGCD2 : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SMNSingleTargetGCD2;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.EgiAssault2)
            {
                var gauge = GetJobGauge<SMNGauge>();
		        if (gauge.TimerRemaining > 0)
		        {
		            if (gauge.IsPhoenixReady())
		            {
		                if (HasEffect(SMN.Buffs.HellishConduit))
			            return SMN.BrandOfPurgatory;
			        return SMN.FountainOfFire;
		            }
                    if (gauge.TimerRemaining < 3000)
                        return SMN.Deathflare;
                    if (HasEffect(SMN.Buffs.FurtherRuin) && gauge.ReturnSummon != SummonPet.NONE)
                        return SMN.Ruin4;
                    return SMN.Ruin3;
		        }

                if (HasCondition(ConditionFlag.InCombat))
                {
                    var Egi1Cd = GetCooldown(SMN.EgiAssault).CooldownRemaining;
                    var Egi2Cd = GetCooldown(SMN.EgiAssault2).CooldownRemaining;
                    if (Egi1Cd <= 5 || (Egi2Cd <= 5 && level >= SMN.Levels.EgiAssault2))
                    {
                        if (Egi1Cd < Egi2Cd)
                            return OriginalHook(SMN.EgiAssault);
                        else
                            return OriginalHook(SMN.EgiAssault2);
                    }
                }

                return OriginalHook(SMN.Ruin3);
            }
            return actionID;
        }
    }
// Single oGCD [R]
// If Gauge: Deathflare -> Enkindle
// Energy Drain
// If Dots: Fester?
// Tri Disaster
// Fester

    internal class SMNSingleTargetoGCD : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SMNSingleTargetoGCD;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Fester)
            {
                var gauge = GetJobGauge<SMNGauge>();
		        if (gauge.TimerRemaining > 0)
		        {
		            if (gauge.IsPhoenixReady() && !GetCooldown(SMN.EnkindlePhoenix).IsCooldown)
		                return SMN.EnkindlePhoenix;

                    if (gauge.ReturnSummon != SummonPet.NONE && !GetCooldown(SMN.EnkindleBahamut).IsCooldown)
                    	return SMN.EnkindleBahamut;

                    if (gauge.TimerRemaining < 3000)
                        return SMN.Deathflare;
		        }

                if (!gauge.HasAetherflowStacks() && !GetCooldown(SMN.EnergyDrain).IsCooldown)
                    return SMN.EnergyDrain;

                if (!GetCooldown(SMN.LucidDreaming).IsCooldown && LocalPlayer.CurrentMp < 7000)
                    return SMN.LucidDreaming; 
		    
//                 if (!GetCooldown(SMN.TriDisaster).IsCooldown && (level >= SMN.Levels.TriDisaster))
//                     return SMN.TriDisaster;

            }

            return actionID;
        }
    }
// AoE oGCD [X]
    internal class SMNAoEoGCD : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SMNAoEoGCD;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Painflare)
            {
                var gauge = GetJobGauge<SMNGauge>();
		        if (gauge.TimerRemaining > 0)
		        {
                    if (gauge.IsPhoenixReady() && !GetCooldown(SMN.EnkindlePhoenix).IsCooldown)
                        return SMN.EnkindlePhoenix;

                    if (gauge.ReturnSummon != SummonPet.NONE && !GetCooldown(SMN.EnkindleBahamut).IsCooldown)
                        return SMN.EnkindleBahamut;

                    if (gauge.TimerRemaining < 3000)
                        return SMN.Deathflare;
		        }

                if (!gauge.HasAetherflowStacks() || (level < SMN.Levels.Painflare))
                    return SMN.EnergySyphon;
            }

            return actionID;
        }
    }
// AoE GCD [C]    
    internal class SMNAoEGCD : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SMNAoEGCD;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Outburst)
            {
                var gauge = GetJobGauge<SMNGauge>();
		        if (gauge.TimerRemaining > 0)
		        {
		            if (gauge.IsPhoenixReady())
		            {
		                if (HasEffect(SMN.Buffs.HellishConduit))
			            return SMN.BrandOfPurgatory;
			        return SMN.FountainOfFire;
		            }
                    if (gauge.TimerRemaining < 3000)
                        return SMN.Deathflare;
		            return SMN.Outburst;
		        }


                if (HasCondition(ConditionFlag.InCombat))
                {
                    var Egi1Cd = GetCooldown(SMN.EgiAssault).CooldownRemaining;
                    var Egi2Cd = GetCooldown(SMN.EgiAssault2).CooldownRemaining;
                    if (Egi1Cd <= 30 || (Egi2Cd <= 30 && level >= SMN.Levels.EgiAssault2))
                    {
                        if (Egi1Cd < Egi2Cd)
                            return OriginalHook(SMN.EgiAssault);
                        else
                            return OriginalHook(SMN.EgiAssault2);
                    }
                }
            }
            return actionID;
        }
    }
    
    internal class SMNDots : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SMNDots;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Miasma || actionID == SMN.Miasma3)
            {
	        if (!GetCooldown(SMN.TriDisaster).IsCooldown && (level >= SMN.Levels.TriDisaster))
                    return SMN.TriDisaster;
	    
                if (level >= SMN.Levels.Bio3)
                {
                    if (DebuffDuration(SMN.Debuffs.Bio3) < 5)
                        return OriginalHook(SMN.Bio);
                    return OriginalHook(SMN.Miasma);
                }
                if (level >= SMN.Levels.Bio2)
                {
                    if (DebuffDuration(SMN.Debuffs.Bio2) < 5)
                        return OriginalHook(SMN.Bio);
                    return OriginalHook(SMN.Miasma);
                }
                if (DebuffDuration(SMN.Debuffs.Bio) < 5)
                    return OriginalHook(SMN.Bio);
                return OriginalHook(SMN.Miasma);
            }
            return actionID;
        }
    }
}
