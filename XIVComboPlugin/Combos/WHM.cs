using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Structs.JobGauge;

namespace XIVComboExpandedPlugin.Combos
{
    internal static class WHM
    {
        public const byte JobID = 24;

        public const uint
            Cure = 120,
            Medica = 124,
            Medica2 = 133,
            Cure2 = 135,
            Cure3 = 131,
            Dia = 16532,
            LucidDreaming = 7562,
            Assize = 3571,
            PresenceOfMind = 136,
            DivineBenison = 7432,
            Tetragrammaton = 3570,
            ThinAir = 7430,
            PlenaryIndulgence = 7433,	
            AfflatusSolace = 16531,
            AfflatusRapture = 16534,
            AfflatusMisery = 16535;

        public static class Buffs
        {
            public const short
                Medica2 = 150;
        }

        public static class Debuffs
        {
            // public const short placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                Cure2 = 30,
                Medica2 = 50,
                AfflatusSolace = 52,
                Assize = 56,
                ThinAir = 58,
                Tetragrammaton = 60,
                DivineBenison = 66,
                PlenaryIndulgence = 70,
                AfflatusRapture = 76;
        }
    }

    internal class WHMoGCD : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.WHMoGCD;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == WHM.LucidDreaming)
            {
                if (!GetCooldown(WHM.LucidDreaming).IsCooldown && LocalPlayer.CurrentMp < 8000)
                    return WHM.LucidDreaming;
                if (!GetCooldown(WHM.Assize).IsCooldown && level >= WHM.Levels.Assize)
                    return WHM.Assize;
                if (!GetCooldown(WHM.PresenceOfMind).IsCooldown)
                    return WHM.PresenceOfMind;
                if (!GetCooldown(WHM.DivineBenison).IsCooldown && level >= WHM.Levels.DivineBenison)
                    return WHM.DivineBenison;
            }

            return actionID;
        }
    }

    internal class WHMHeal : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.WHMHeal;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == WHM.Cure2)
            {
                if (!GetCooldown(WHM.Tetragrammaton).IsCooldown && level >= WHM.Levels.Tetragrammaton)
                    return WHM.Tetragrammaton;
                var gauge = GetJobGauge<WHMGauge>();
                if (level >= WHM.Levels.AfflatusSolace && gauge.NumLilies > 0)
                    return WHM.AfflatusSolace;
                if (level < WHM.Levels.Cure2)
                    return WHM.Cure;
		return WHM.Cure2;
            }

            return actionID;
        }
    }



    internal class WHMDia : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.WHMDia;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == WHM.AfflatusMisery)
            {
                var gauge = GetJobGauge<WHMGauge>();
                if (gauge.NumBloodLily == 3)
                    return WHM.AfflatusMisery;
		return OriginalHook(WHM.Dia);
            }

            return actionID;
        }
    }

    internal class WHMCure3 : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.WHMCure3;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == WHM.Cure3)
            {
                if (!GetCooldown(WHM.PlenaryIndulgence).IsCooldown && level >= WHM.Levels.PlenaryIndulgence)
                    return WHM.PlenaryIndulgence;
                if (!GetCooldown(WHM.ThinAir).IsCooldown && level >= WHM.Levels.ThinAir)
                    return WHM.ThinAir;
                return WHM.Cure3;
            }

            return actionID;
        }
    }

    internal class WHMAoEHealing : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.WHMAoEHealing;
    
        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == WHM.Medica2)
            {
                if (!GetCooldown(WHM.PlenaryIndulgence).IsCooldown && level >= WHM.Levels.PlenaryIndulgence)
                    return WHM.PlenaryIndulgence;
                var gauge = GetJobGauge<WHMGauge>(); 
                if (level >= WHM.Levels.AfflatusRapture && gauge.NumLilies > 0)
                    return WHM.AfflatusRapture;
                if (!GetCooldown(WHM.ThinAir).IsCooldown && level >= WHM.Levels.ThinAir)
                    return WHM.ThinAir;
                if (level >= WHM.Levels.Medica2 && BuffDuration(WHM.Buffs.Medica2) < 8)
                    return WHM.Medica2;    		
                return WHM.Medica;
            }
            return actionID;
        }
    }
}
