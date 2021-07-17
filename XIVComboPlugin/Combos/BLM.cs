using Dalamud.Game.ClientState.Structs.JobGauge;

namespace XIVComboExpandedPlugin.Combos
{
    internal static class BLM
    {
        public const byte JobID = 25;

        public const uint
            Enochian = 3575,
            Blizzard4 = 3576,
            Fire = 141,
            Fire3 = 152,
            Fire4 = 3577,
            Blizzard = 142,
            Blizzard2 = 146,
            Blizzard3 = 154,
            Thunder = 144,
            Thunder3 = 153,
            Despair = 16505,
            Flare = 162,
            Freeze = 159,
            Transpose = 149,
            UmbralSoul = 16506,
            LeyLines = 3573,
            BetweenTheLines = 7419;

        public static class Buffs
        {
            public const short
                Thundercloud = 164,
                LeyLines = 737,
                Firestarter = 165;
        }
        public static class Debuffs
        {
            public const short
                Thunder = 161,
                Thunder3 = 163;
        }

        public static class Levels
        {
            public const byte
                Fire3 = 34,
                Freeze = 35,
                Blizzard3 = 40,
                Thunder3 = 45,
                Flare = 50,
                Enochian = 56,
                Blizzard4 = 58,
                Fire4 = 60,
                BetweenTheLines = 62,
                Despair = 72,
                UmbralSoul = 76;
        }
    }

    internal class BlackEnochianFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BlackEnochianFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Enochian)
            {
                var gauge = GetJobGauge<BLMGauge>();
                if (gauge.IsEnoActive())
                {
                    if (gauge.InUmbralIce() && level >= BLM.Levels.Blizzard4)
                        return BLM.Blizzard4;
                    if (level >= BLM.Levels.Fire4)
                        return BLM.Fire4;
                }

                if (level < BLM.Levels.Fire3)
                    return BLM.Fire;

                if (gauge.InAstralFire() && (level < BLM.Levels.Enochian || gauge.IsEnoActive()))
                {
                    if (HasEffect(BLM.Buffs.Firestarter))
                        return BLM.Fire3;
                    return BLM.Fire;
                }
            }

            return actionID;
        }
    }

    internal class BlackManaFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BlackManaFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Transpose)
            {
                var gauge = GetJobGauge<BLMGauge>();
                if (gauge.InUmbralIce() && gauge.IsEnoActive() && level >= BLM.Levels.UmbralSoul)
                    return BLM.UmbralSoul;
            }

            return actionID;
        }
    }

    internal class BlackLeyLinesFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BlackLeyLinesFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.LeyLines)
            {
                if (HasEffect(BLM.Buffs.LeyLines) && level >= BLM.Levels.BetweenTheLines)
                    return BLM.BetweenTheLines;
            }

            return actionID;
        }
    }

    internal class BlackBlizzardFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BlackBlizzardFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Blizzard)
            {
                var gauge = GetJobGauge<BLMGauge>();
                if (level >= BLM.Levels.Blizzard3 && !gauge.InUmbralIce())
                    return BLM.Blizzard3;
            }

            if (actionID == BLM.Freeze)
            {
                if (level < BLM.Levels.Freeze)
                    return BLM.Blizzard2;
            }

            return actionID;
        }
    }

    internal class BlackFireFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.BlackFireFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == BLM.Fire)
            {
                var gauge = GetJobGauge<BLMGauge>();
                if (level >= BLM.Levels.Fire3 && (!gauge.InAstralFire() || HasEffect(BLM.Buffs.Firestarter)))
                    return BLM.Fire3;
            }

            return actionID;
        }
    }
}
