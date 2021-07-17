using Dalamud.Game.ClientState.Structs.JobGauge;

namespace XIVComboExpandedPlugin.Combos
{
    internal static class DRK
    {
        public const byte JobID = 32;

        public const uint
            HardSlash = 3617,
            Unleash = 3621,
            SyphonStrike = 3623,
            Souleater = 3632,
            Quietus = 7391,
            Bloodspiller = 7392,
            FloodOfDarkness = 16466,
            EdgeOfDarkness = 16467,
            StalwartSoul = 16468,
            AbyssalDrain = 3641,
            CarveAndSpit = 3643,
            BloodWeapon = 3625,
            LivingShadow = 16472,
            FloodOfShadow = 16469,
            EdgeOfShadow = 16470;

        public static class Buffs
        {
            public const short
                BloodWeapon = 742,
                Delirium = 1972;
        }
        public static class Debuffs
        {
            // public const short placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                SyphonStrike = 2,
                Souleater = 26,
                FloodOfDarkness = 30,
                BloodWeapon = 35,
                EdgeOfDarkness = 40,
                AbyssalDrain = 56,
                CarveAndSpit = 60,
                Bloodspiller = 62,
                Quietus = 64,
                Delirium = 68,
                StalwartSoul = 72,
                LivingShadow = 74;
        }
    }

    internal class DarkSouleaterCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DarkSouleaterCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRK.Souleater)
            {
                if (level >= DRK.Levels.Delirium && HasEffect(DRK.Buffs.Delirium))
                    return DRK.Bloodspiller;

                var gauge = GetJobGauge<DRKGauge>();
                if (gauge.Blood >= 90)
                    return DRK.Bloodspiller;

                if (comboTime > 0)
                {
                    if (lastComboMove == DRK.HardSlash && level >= DRK.Levels.SyphonStrike)
                        return DRK.SyphonStrike;

                    if (lastComboMove == DRK.SyphonStrike && level >= DRK.Levels.Souleater)
                        return DRK.Souleater;
                }

                return DRK.HardSlash;
            }

            return actionID;
        }
    }

    internal class DarkStalwartSoulCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DarkStalwartSoulCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRK.StalwartSoul)
            {
                var gauge = GetJobGauge<DRKGauge>();
                if (level >= DRK.Levels.FloodOfDarkness && gauge.DarksideTimeRemaining < 5 && LocalPlayer.CurrentMp > 6000)
                    return OriginalHook(DRK.FloodOfDarkness);

                if (level >= DRK.Levels.Delirium && HasEffect(DRK.Buffs.Delirium))
                    return DRK.Quietus;

                if (gauge.Blood >= 90)
                    return DRK.Quietus;

                if (comboTime > 0 && lastComboMove == DRK.Unleash && level >= DRK.Levels.StalwartSoul)
                    return DRK.StalwartSoul;

                if (level >= DRK.Levels.FloodOfDarkness && LocalPlayer.CurrentMp > 9000)
                    return OriginalHook(DRK.FloodOfDarkness);

                return DRK.Unleash;
            }

            return actionID;
        }
    }

    internal class DRKoGCDFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.DRKoGCDFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == DRK.AbyssalDrain)
            {

                var gauge = GetJobGauge<DRKGauge>();
                if (level >= DRK.Levels.EdgeOfDarkness && gauge.DarksideTimeRemaining < 5 && LocalPlayer.CurrentMp > 6000)
                    return OriginalHook(DRK.EdgeOfDarkness);

                if (level >= DRK.Levels.AbyssalDrain && !GetCooldown(DRK.AbyssalDrain).IsCooldown)
                    return DRK.AbyssalDrain;
                if (level >= DRK.Levels.CarveAndSpit && !GetCooldown(DRK.CarveAndSpit).IsCooldown)
                    return DRK.CarveAndSpit;
                if (level >= DRK.Levels.LivingShadow && !GetCooldown(DRK.LivingShadow).IsCooldown && gauge.Blood >= 50)
                    return DRK.LivingShadow;
                if (level >= DRK.Levels.BloodWeapon && !GetCooldown(DRK.BloodWeapon).IsCooldown)
                    return DRK.BloodWeapon;

                if (level >= DRK.Levels.EdgeOfDarkness &&  LocalPlayer.CurrentMp > 9000)
                    return OriginalHook(DRK.EdgeOfDarkness);
            }

            return actionID;
        }
    }
}
