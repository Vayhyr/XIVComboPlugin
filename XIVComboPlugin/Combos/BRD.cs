using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Structs.JobGauge;

namespace XIVComboExpandedPlugin.Combos
{
    internal static class BRD
    {
        public const byte JobID = 23;

        public const uint
            HeavyShot = 97,
            StraightShot = 98,
            VenomousBite = 100,
            QuickNock = 106,
            Windbite = 113,
            WanderersMinuet = 3559,
            MagesBallad = 114,
            ArmysPaeon = 116,
            RagingStrikes = 101,
            IronJaws = 3560,
            PitchPerfect = 7404,
            CausticBite = 7406,
            Stormbite = 7407,
            RefulgentArrow = 7409,
            BurstShot = 16495,
            Bloodletter = 110,
            EmpyrealArrow = 3558,
            Sidewinder = 3562,
            Barrage = 107,
            BattleVoice = 118,
            RainofDeath = 117,
            Shadowbite = 16494,
            ApexArrow = 16496;

        public static class Buffs
        {
            public const short
                StraightShotReady = 122;
        }

        public static class Debuffs
        {
            public const short
                VenomousBite = 124,
                Windbite = 129,
                CausticBite = 1200,
                Stormbite = 1201;
        }

        public static class Levels
        {
            public const byte
                WanderersMinuet = 52,
                MagesBallad = 30,
                ArmysPaeon = 40,
                BattleVoice = 50,
                EmpyrealArrow = 54,
                Sidewinder = 60,
                Barrage = 38,
                RainofDeath = 45,
                Shadowbite = 72,
                Windbite = 30,
                IronJaws = 56,
                BiteUpgrade = 64,
                RefulgentArrow = 70,
                BurstShot = 76;
        }
    }

    internal class BRDoGCD : CustomCombo
    {
        //         [CustomComboInfo("[oGCD]", "Song -> Pitch Perfect -> Empyreal Arrow -> Bloodletter -> Sidewinder.", BRD.JobID, BRD.WanderersMinuet)]
        protected override CustomComboPreset Preset => CustomComboPreset.BRDoGCD;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BRD.Bloodletter)
            {
                var gauge = GetJobGauge<BRDGauge>();
                if (gauge.SongTimer < 1 || gauge.ActiveSong == CurrentSong.ARMY)
                {
                    if (!GetCooldown(BRD.WanderersMinuet).IsCooldown && (level >= BRD.Levels.WanderersMinuet))
                        return BRD.WanderersMinuet;
                    if (!GetCooldown(BRD.MagesBallad).IsCooldown && (level >= BRD.Levels.MagesBallad))
                        return BRD.MagesBallad;
                    if (!GetCooldown(BRD.ArmysPaeon).IsCooldown && (level >= BRD.Levels.ArmysPaeon))
                        return BRD.ArmysPaeon;
                }

                if (gauge.ActiveSong == CurrentSong.WANDERER && gauge.NumSongStacks == 3)
                    return BRD.PitchPerfect;
                if (!GetCooldown(BRD.EmpyrealArrow).IsCooldown && (level >= BRD.Levels.EmpyrealArrow))
                    return BRD.EmpyrealArrow;
                if (!GetCooldown(BRD.Bloodletter).IsCooldown)
                    return BRD.Bloodletter;
                if (!GetCooldown(BRD.Sidewinder).IsCooldown && (level >= BRD.Levels.Sidewinder))
                    return BRD.Sidewinder;
            }
            return actionID;
        }
    }

    internal class BardStraightShotUpgradeFeature : CustomCombo
    {
        //        [CustomComboInfo(" [Q] Heavy Shot into Straight Shot", "Replaces Heavy Shot/Burst Shot with Straight Shot/Refulgent Arrow when procced", BRD.JobID, BRD.HeavyShot, BRD.BurstShot)]
        protected override CustomComboPreset Preset => CustomComboPreset.BardStraightShotUpgradeFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BRD.HeavyShot || actionID == BRD.BurstShot)
            {
                 var gauge = GetJobGauge<BRDGauge>();
                 if (gauge.SoulVoiceValue == 100)
                     return BRD.ApexArrow;

                if (HasEffect(BRD.Buffs.StraightShotReady))
                    return OriginalHook(BRD.RefulgentArrow);
            }

            return actionID;
        }
    }

    internal class BardIronJawsFeature : CustomCombo
    {
        //        [CustomComboInfo("[E] Iron Jaws Feature", "Iron Jaws is replaced with Caustic Bite/Stormbite if one or both are not up.\nAlternates between the two if Iron Jaws isn't available.", BRD.JobID, BRD.IronJaws)]
        protected override CustomComboPreset Preset => CustomComboPreset.BardIronJawsFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BRD.IronJaws)
            {
                if (level >= BRD.Levels.BiteUpgrade)
                {
                    var caustic = DebuffDuration(BRD.Debuffs.CausticBite);
                    var stormbite = DebuffDuration(BRD.Debuffs.Stormbite);
                    if (caustic == 0)
                        return BRD.CausticBite;
                    if (stormbite == 0)
                        return BRD.Stormbite;
                    if (caustic < 4 || stormbite < 4)
                        return BRD.IronJaws;
                }

                else if (level >= BRD.Levels.IronJaws && level < BRD.Levels.BiteUpgrade)
                {
                    var venomous = DebuffDuration(BRD.Debuffs.VenomousBite);
                    var windbite = DebuffDuration(BRD.Debuffs.Windbite);
                    if (venomous == 0)
                        return BRD.VenomousBite;
                    if (windbite == 0)
                        return BRD.Windbite;
                    if (venomous < 4 || windbite < 4)
                        return BRD.IronJaws;
                }

                else if (level < BRD.Levels.IronJaws)
                {
                    var venomous = DebuffDuration(BRD.Debuffs.VenomousBite);
                    var windbite = DebuffDuration(BRD.Debuffs.Windbite);
                    if (venomous < 4)
                        return BRD.VenomousBite;
                    if (windbite < 4 && level >= BRD.Levels.Windbite)
                        return BRD.Windbite;
                }

                var gauge = GetJobGauge<BRDGauge>();
                if (gauge.SoulVoiceValue == 100)
                    return BRD.ApexArrow;

                if (HasEffect(BRD.Buffs.StraightShotReady))
                    return OriginalHook(BRD.RefulgentArrow);
                return OriginalHook(BRD.BurstShot);
            }

            return actionID;
        }
    }

    internal class bardAoEfeature : CustomCombo
    {
        //                 [CustomComboInfo("[C] burst shot/quick nock into apex arrow", "Rain of Death -> Shadowbite.", BRD.JobID, BRD.IronJaws)]
        protected override CustomComboPreset Preset => CustomComboPreset.bardAoEfeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BRD.Shadowbite)
            {
                if (HasCondition(ConditionFlag.InCombat))
                {
                    if (!GetCooldown(BRD.RainofDeath).IsCooldown && (level >= BRD.Levels.RainofDeath))
                        return BRD.RainofDeath;
                    if (!GetCooldown(BRD.Shadowbite).IsCooldown && (level >= BRD.Levels.Shadowbite))
                        return BRD.Shadowbite;
                }
                var gauge = GetJobGauge<BRDGauge>();
                if (gauge.SoulVoiceValue == 100)
                    return BRD.ApexArrow;
                return BRD.QuickNock;
            }

            return actionID;
        }
    }


    internal class BardBuff : CustomCombo
    {
        //         [CustomComboInfo("[R]", "Raging Strikes -> Barrage -> Straight Shot.", BRD.JobID, BRD.IronJaws)]
        protected override CustomComboPreset Preset => CustomComboPreset.BardBuff;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BRD.Barrage)
            {
                var gauge = GetJobGauge<BRDGauge>();
                if (!GetCooldown(BRD.BattleVoice).IsCooldown && (level >= BRD.Levels.BattleVoice))
                    return BRD.BattleVoice;
                if (!GetCooldown(BRD.RagingStrikes).IsCooldown)
                    return BRD.RagingStrikes;
                if (HasEffect(BRD.Buffs.StraightShotReady))
                    return OriginalHook(BRD.RefulgentArrow);
                return BRD.Barrage;
            }

            return actionID;
        }
    }

}
