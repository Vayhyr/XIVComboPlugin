using Dalamud.Game.ClientState.Structs.JobGauge;

namespace XIVComboExpandedPlugin.Combos
{
    internal static class GNB
    {
        public const byte JobID = 37;

        public const uint
            KeenEdge = 16137,
            NoMercy = 16138,
            BrutalShell = 16139,
            DemonSlice = 16141,
            SolidBarrel = 16145,
            GnashingFang = 16146,
            SavageClaw = 16147,
            DemonSlaughter = 16149,
            WickedTalon = 16150,
            SonicBreak = 16153,
            Continuation = 16155,
            JugularRip = 16156,
            AbdomenTear = 16157,
            EyeGouge = 16158,
            BowShock = 16159,
            BurstStrike = 16162,
            FatedCircle = 16163,
	    DangerZone = 16144,
            Bloodfest = 16164;

        public static class Buffs
        {
            public const short
                NoMercy = 1831,
                ReadyToRip = 1842,
                ReadyToTear = 1843,
                ReadyToGouge = 1844;
        }

        public static class Debuffs
        {
            public const short
                BowShock = 1838;
        }

        public static class Levels
        {
            public const byte
                BrutalShell = 4,
                SolidBarrel = 26,
		BurstStrike = 30,
                DemonSlaughter = 40,
                SonicBreak = 54,
                GnashingFang = 60,
                BowShock = 62,
                Continuation = 70,
                FatedCircle = 72,
                Bloodfest = 76;
        }
    }

    internal class GunbreakerSolidBarrelCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.GunbreakerSolidBarrelCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == GNB.SolidBarrel)
            {
                if (level >= GNB.Levels.Continuation)
                {
                    if (HasEffect(GNB.Buffs.ReadyToRip))
                        return GNB.JugularRip;

                    if (HasEffect(GNB.Buffs.ReadyToTear))
                        return GNB.AbdomenTear;

                    if (HasEffect(GNB.Buffs.ReadyToGouge))
                        return GNB.EyeGouge;
                }

                var gauge = GetJobGauge<GNBGauge>();
                if (gauge.AmmoComboStepNumber == 1)
                    return GNB.SavageClaw;
                if (gauge.AmmoComboStepNumber == 2)
                    return GNB.WickedTalon;

                if (gauge.NumAmmo == 2 && level >= GNB.Levels.BurstStrike)
                    return GNB.BurstStrike;
                if (comboTime > 0)
                {
                    if (lastComboMove == GNB.KeenEdge && level >= GNB.Levels.BrutalShell)
                        return GNB.BrutalShell;

                    if (lastComboMove == GNB.BrutalShell && level >= GNB.Levels.SolidBarrel)
                        return GNB.SolidBarrel;
                }

                return GNB.KeenEdge;
            }

            return actionID;
        }
    }

    internal class GunbreakerGnashingFangCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.GunbreakerGnashingFangCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == GNB.WickedTalon)
            {

                if (level >= GNB.Levels.Continuation)
                {
                    if (HasEffect(GNB.Buffs.ReadyToRip))
                        return GNB.JugularRip;

                    if (HasEffect(GNB.Buffs.ReadyToTear))
                        return GNB.AbdomenTear;

                    if (HasEffect(GNB.Buffs.ReadyToGouge))
                        return GNB.EyeGouge;
                }

                var gauge = GetJobGauge<GNBGauge>();
                if (gauge.NumAmmo == 0 && level >= GNB.Levels.Bloodfest && !GetCooldown(GNB.Bloodfest).IsCooldown)
                    return GNB.Bloodfest;

                if (level >= GNB.Levels.GnashingFang)
                {
                    if (gauge.AmmoComboStepNumber == 1)
                        return GNB.SavageClaw;

                    if (gauge.AmmoComboStepNumber == 2)
                        return GNB.WickedTalon;

                    if (!GetCooldown(GNB.GnashingFang).IsCooldown)
                        return GNB.GnashingFang;
                }
                return GNB.BurstStrike;
            }

            return actionID;
        }
    }

    internal class GunbreakerDemonSlaughterCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.GunbreakerDemonSlaughterCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == GNB.DemonSlaughter)
            {
                var gauge = GetJobGauge<GNBGauge>();
                if (gauge.NumAmmo == 2 && level >= GNB.Levels.FatedCircle)
                    return GNB.FatedCircle;
                if (comboTime > 0 && lastComboMove == GNB.DemonSlice && level >= GNB.Levels.DemonSlaughter)
                    return GNB.DemonSlaughter;
                return GNB.DemonSlice;
            }

            return actionID;
        }
    }

    internal class GunbreakerBloodfestOvercapFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.GunbreakerBloodfestOvercapFeature;
    
        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == GNB.BurstStrike)
            {
                var gauge = GetJobGauge<GNBGauge>();
                if (gauge.NumAmmo == 0 && level >= GNB.Levels.Bloodfest)
                    return GNB.Bloodfest;
            }
    
            return actionID;
        }
    }

    internal class GunbreakerNoMercyFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.GunbreakerNoMercyFeature;
    
        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == GNB.NoMercy)
            {
                if (!GetCooldown(GNB.NoMercy).IsCooldown)
                    return GNB.NoMercy;
                if (!GetCooldown(GNB.BowShock).IsCooldown && level >= GNB.Levels.BowShock)
                    return GNB.BowShock;
                if (!GetCooldown(GNB.SonicBreak).IsCooldown && level >= GNB.Levels.SonicBreak)
                    return GNB.SonicBreak;
                if (!GetCooldown(GNB.DangerZone).IsCooldown)
                    return OriginalHook(GNB.DangerZone);
            }
    
            return actionID;
        }
    }
}
