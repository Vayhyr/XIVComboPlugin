using Dalamud.Game.ClientState.Structs.JobGauge;

namespace XIVComboExpandedPlugin.Combos
{
    internal static class SAM
    {
        public const byte JobID = 34;

        public const uint
            Hakaze = 7477,
            Yukikaze = 7480,
            Gekko = 7481,
            Jinpu = 7478,
            Kasha = 7482,
            Shifu = 7479,
            Mangetsu = 7484,
            Fuga = 7483,
            Oka = 7485,
            MeikyoShisui = 7499,
            MercifulEyes = 7502,
            Seigan = 7501,
            ThirdEye = 7498,
            Iaijutsu = 7867,
            TsubameGaeshi = 16483,
            KaeshiHiganbana = 16484,
            Kaiten = 7494,
            TrashHiganbana = 16484,
            HissatsuSenei = 16481,
            HissatsuShinten = 7490,
            Shoha = 16487;

        public static class Buffs
        {
            public const short
                MeikyoShisui = 1233,
                EyesOpen = 1252,
                Jinpu = 1298,
                Kaiten = 1229,
                Shifu = 1299;
        }

        public static class Debuffs
        {
            // public const short placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                Jinpu = 4,
                Shifu = 18,
                Gekko = 30,
                Mangetsu = 35,
                Kasha = 40,
                Oka = 45,
                Yukikaze = 50,
                Kaiten = 52,
                TsubameGaeshi = 76,
                HissatsuSenei = 72,
                HissatsuShinten = 62,
                Shoha = 80;
        }
    }

    internal class SamuraiYukikazeCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiYukikazeCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.Yukikaze)
            {
                if (HasEffect(SAM.Buffs.MeikyoShisui))
                {
                    var gauge = GetJobGauge<SAMGauge>();
                    if (level >= SAM.Levels.Kasha && gauge.Sen.HasFlag(Sen.KA) == false)
                        return SAM.Kasha;
                    if (level >= SAM.Levels.Gekko && gauge.Sen.HasFlag(Sen.GETSU) == false)
                        return SAM.Gekko;
                    if (level >= SAM.Levels.Yukikaze && gauge.Sen.HasFlag(Sen.SETSU) == false)
                        return SAM.Yukikaze;
                }

                if (comboTime > 0 && level >= SAM.Levels.Shifu)
                {
                    var gauge = GetJobGauge<SAMGauge>();
                    if (lastComboMove == SAM.Hakaze)
                    {
                        if (level >= SAM.Levels.Shifu && gauge.Sen.HasFlag(Sen.KA) == false && BuffDuration(SAM.Buffs.Shifu) < 10)
                            return SAM.Shifu;
                        if (level >= SAM.Levels.Jinpu && gauge.Sen.HasFlag(Sen.GETSU) == false && BuffDuration(SAM.Buffs.Jinpu) < 10)
                            return SAM.Jinpu;
                        if (level >= SAM.Levels.Yukikaze && gauge.Sen.HasFlag(Sen.SETSU) == false)
                            return SAM.Yukikaze;
                        if (level >= SAM.Levels.Shifu && (BuffDuration(SAM.Buffs.Shifu) < 5 || gauge.Sen.HasFlag(Sen.KA) == false))
                            return SAM.Shifu;
                        if (level >= SAM.Levels.Jinpu)
                            return SAM.Jinpu;
                    }
                    if (lastComboMove == SAM.Shifu && level >= SAM.Levels.Kasha)
                        return SAM.Kasha;
                    if (lastComboMove == SAM.Jinpu && level >= SAM.Levels.Gekko)
                        return SAM.Gekko;
                }
                return SAM.Hakaze;
            }

            return actionID;
        }
    }

    internal class SamuraiGekkoCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiGekkoCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.Gekko)
            {
                if (HasEffect(SAM.Buffs.MeikyoShisui))
                    return SAM.Gekko;

                if (comboTime > 0)
                {
                    if (lastComboMove == SAM.Hakaze && level >= SAM.Levels.Jinpu)
                        return SAM.Jinpu;

                    if (lastComboMove == SAM.Jinpu && level >= SAM.Levels.Gekko)
                        return SAM.Gekko;
                }

                return SAM.Hakaze;
            }

            return actionID;
        }
    }

    internal class SamuraiKashaCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiKashaCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.Kasha)
            {
                if (HasEffect(SAM.Buffs.MeikyoShisui))
                    return SAM.Kasha;

                if (comboTime > 0)
                {
                    if (lastComboMove == SAM.Hakaze && level >= SAM.Levels.Shifu)
                        return SAM.Shifu;

                    if (lastComboMove == SAM.Shifu && level >= SAM.Levels.Kasha)
                        return SAM.Kasha;
                }

                return SAM.Hakaze;
            }

            return actionID;
        }
    }

    internal class SamuraiMangetsuCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiMangetsuCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.Mangetsu)
            {
                if (HasEffect(SAM.Buffs.MeikyoShisui))
                    return SAM.Mangetsu;

                if (comboTime > 0 && lastComboMove == SAM.Fuga && level >= SAM.Levels.Mangetsu)
                    return SAM.Mangetsu;

                return SAM.Fuga;
            }

            return actionID;
        }
    }

    internal class SamuraiOkaCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiOkaCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.Oka)
            {
                if (HasEffect(SAM.Buffs.MeikyoShisui))
                    return SAM.Oka;

                if (comboTime > 0 && lastComboMove == SAM.Fuga && level >= SAM.Levels.Mangetsu)
                {
                    var gauge = GetJobGauge<SAMGauge>();
                    if (level >= SAM.Levels.Oka && gauge.Sen.HasFlag(Sen.KA) == false && BuffDuration(SAM.Buffs.Shifu) < 5)
                        return SAM.Oka;
                    if (level < SAM.Levels.Oka || gauge.Sen.HasFlag(Sen.GETSU) == false || (BuffDuration(SAM.Buffs.Jinpu) < BuffDuration(SAM.Buffs.Shifu)))
                        return SAM.Mangetsu;
                    if (level >= SAM.Levels.Oka)
                        return SAM.Oka;
                }
                return SAM.Fuga;
            }

            return actionID;
        }
    }

    internal class SamuraiThirdEyeFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiThirdEyeFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.MercifulEyes)
            {
                if (HasEffect(SAM.Buffs.EyesOpen))
                    return SAM.MercifulEyes;

                return SAM.ThirdEye;
            }

            return actionID;
        }
    }

        internal class SamuraiJinpuShifuFeature : CustomCombo
        {
            protected override CustomComboPreset Preset => CustomComboPreset.SamuraiJinpuShifuFeature;

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID == SAM.MeikyoShisui)
                {
                    if (HasEffect(SAM.Buffs.MeikyoShisui) && IsEnabled(CustomComboPreset.SamuraiJinpuShifuFeature))
                    {
                        if (!HasEffect(SAM.Buffs.Jinpu))
                            return SAM.Jinpu;

                        if (!HasEffect(SAM.Buffs.Shifu))
                            return SAM.Shifu;

                    }
                    return SAM.MeikyoShisui;
                }

                return actionID;
            }
        }

    internal class SamuraiTsubameGaeshiShohaFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiTsubameGaeshiShohaFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.TsubameGaeshi)
            {
                var gauge = GetJobGauge<SAMGauge>();
                if (level >= SAM.Levels.Shoha && gauge.MeditationStacks >= 3)
                    return SAM.Shoha;
            }

            return actionID;
        }
    }

    internal class SamuraiHissatsuFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiHissatsuFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.HissatsuShinten)
            {
                var gauge = GetJobGauge<SAMGauge>();
                if (level >= SAM.Levels.Shoha && gauge.MeditationStacks >= 3)
                    return SAM.Shoha;
                if (level >= SAM.Levels.HissatsuSenei && !GetCooldown(SAM.HissatsuSenei).IsCooldown)
                    return SAM.HissatsuSenei;
                if (level >= SAM.Levels.HissatsuShinten)
                    return SAM.HissatsuShinten;
            }

            return actionID;
        }
    }

    internal class SamuraiTsubameGaeshiIaijutsuFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiTsubameGaeshiIaijutsuFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.TsubameGaeshi)
            {
                var gauge = GetJobGauge<SAMGauge>();
                if (level >= SAM.Levels.TsubameGaeshi && gauge.Sen == Sen.NONE)
                    return OriginalHook(SAM.TsubameGaeshi);
                return OriginalHook(SAM.Iaijutsu);
            }

            return actionID;
        }
    }

    internal class SamuraiIaijutsuTsubameGaeshiFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SamuraiIaijutsuTsubameGaeshiFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SAM.Iaijutsu)
            {
                var gauge = GetJobGauge<SAMGauge>();
                if (level >= SAM.Levels.TsubameGaeshi && gauge.Sen == Sen.NONE)
                {
                    var kaeshi = OriginalHook(SAM.TsubameGaeshi);
                    if (kaeshi == SAM.TrashHiganbana)
                        return SAM.TsubameGaeshi;
                    else
                        return kaeshi;
                }
                if (level >= SAM.Levels.Kaiten && !HasEffect(SAM.Buffs.Kaiten) && gauge.Kenki >= 20)
                    return SAM.Kaiten;
                return OriginalHook(SAM.Iaijutsu);
            }

            return actionID;
        }
    }
}
