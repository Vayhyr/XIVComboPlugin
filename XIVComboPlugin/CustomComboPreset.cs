using Dalamud.Game.Text;
using System;
using XIVComboExpandedPlugin.Combos;

namespace XIVComboExpandedPlugin
{
    public enum CustomComboPreset
    {
        // Last enum used: 109
        // ====================================================================================
        #region ASTROLOGIAN

        [CustomComboInfo("Draw on Play", "Play turns into Draw when no card is drawn, as well as the usual Play behavior", AST.JobID, AST.Play)]
        AstrologianCardsOnDrawFeature = 27,

        [CustomComboInfo("Minor Arcana to Sleeve Draw", "Changes Minor Arcana to Sleeve Draw when a card is not drawn.", AST.JobID, AST.MinorArcana)]
        AstrologianSleeveDrawFeature = 75,

        [CustomComboInfo("Benefic 2 to Benefic Level Sync", "Changes Benefic 2 to Benefic when below level 26 in synced content.", AST.JobID, AST.Benefic2)]
        AstrologianBeneficFeature = 73,

        #endregion
        // ====================================================================================
        #region BLACK MAGE

        [CustomComboInfo("Enochian Stance Switcher", "Change Enochian to Fire 4 or Blizzard 4 depending on stance", BLM.JobID, BLM.Enochian)]
        BlackEnochianFeature = 25,

        [CustomComboInfo("Umbral Soul/Transpose Switcher", "Change Transpose into Umbral Soul when Umbral Soul is usable", BLM.JobID, BLM.Transpose)]
        BlackManaFeature = 26,

        [CustomComboInfo("(Between the) Ley Lines", "Change Ley Lines into BTL when Ley Lines is active", BLM.JobID, BLM.LeyLines)]
        BlackLeyLinesFeature = 56,

        [CustomComboInfo("Fire 1/3 Feature", "Fire 1 becomes Fire 3 outside of Astral Fire, and when Firestarter proc is up.", BLM.JobID, BLM.Fire)]
        BlackFireFeature = 70,

        [CustomComboInfo("Blizzard 1/2/3 Feature", "Blizzard 1 becomes Blizzard 3 when out of Umbral Ice. Freeze becomes Blizzard 2 when synced.", BLM.JobID, BLM.Blizzard, BLM.Freeze)]
        BlackBlizzardFeature = 71,

        #endregion
        // ====================================================================================
        #region BARD

        [CustomComboInfo("[oGCD]", "Song -> Pitch Perfect -> Empyreal Arrow -> Bloodletter -> Sidewinder.", BRD.JobID, BRD.Bloodletter)]
        BRDoGCD = 41,

        [CustomComboInfo(" [Q] Heavy Shot into Straight Shot", "Replaces Heavy Shot/Burst Shot with Straight Shot/Refulgent Arrow when procced", BRD.JobID, BRD.HeavyShot, BRD.BurstShot)]
        BardStraightShotUpgradeFeature = 42,

        [CustomComboInfo("[E] Iron Jaws Feature", "Iron Jaws is replaced with Caustic Bite/Stormbite if one or both are not up.\nAlternates between the two if Iron Jaws isn't available.", BRD.JobID, BRD.IronJaws)]
        BardIronJawsFeature = 63,

        [CustomComboInfo("[C] burst shot/quick nock into apex arrow", "Rain of Death -> Shadowbite.", BRD.JobID, BRD.Shadowbite)]
        bardAoEfeature = 74,

        [CustomComboInfo("[R]", "Raging Strikes -> Barrage -> Straight Shot.", BRD.JobID, BRD.Barrage)]
        BardBuff = 108,

        #endregion
        // ====================================================================================
        #region DANCER

        [CustomComboInfo("Fan Dance Combos", "Change Fan Dance and Fan Dance 2 into Fan Dance 3 while flourishing", DNC.JobID, DNC.FanDance1, DNC.FanDance2)]
        DancerFanDanceCombo = 33,

        [CustomComboInfo("Dance Step Combo", "Change Standard Step and Technical Step into each dance step while dancing", DNC.JobID, DNC.StandardStep, DNC.TechnicalStep)]
        DancerDanceStepCombo = 31,

        [CustomComboInfo("Flourish Proc Saver", "Change Flourish into any available procs before using", DNC.JobID, DNC.Flourish)]
        DancerFlourishFeature = 34,

        [CustomComboInfo("Single Target Multibutton", "Change Cascade into procs and combos as available", DNC.JobID, DNC.Cascade)]
        DancerSingleTargetMultibutton = 43,

        [CustomComboInfo("AoE Multibutton", "Change Windmill into procs and combos as available", DNC.JobID, DNC.Windmill)]
        DancerAoeMultibutton = 50,

        [CustomComboInfo("Dance Step Feature", "Change actions into dance steps while dancing." +
            "\nThis helps ensure you can still dance with combos on, without using auto dance." +
            "\nYou can change the respective actions by inputting action IDs below for each dance step." +
            "\nThe defaults are Cascade, Flourish, Fan Dance and Fan Dance II. If set to 0, they will reset to these actions." +
            "\nYou can get Action IDs with Garland Tools by searching for the action and clicking the cog.", DNC.JobID)]
        DancerDanceComboCompatibility = 72,

        #endregion
        // ====================================================================================
        #region DRAGOON

        [CustomComboInfo("Jump + Mirage Dive", "Replace (High) Jump with Mirage Dive when Dive Ready", DRG.JobID, DRG.Jump, DRG.HighJump)]
        DragoonJumpFeature = 44,
        
        [CustomComboInfo("Spineshatter + Dragonfire Dive", "2 Dives, 1 Button", DRG.JobID, DRG.DragonfireDive)]
        DragoonJumpStackFeature = 95, 

        [CustomComboInfo("BOTD Into Stardiver", "Replace Blood of the Dragon with Stardiver when in Life of the Dragon", DRG.JobID, DRG.BloodOfTheDragon)]
        DragoonBOTDFeature = 46,

        [CustomComboInfo("Coerthan Torment Combo", "Replace Coerthan Torment with its combo chain", DRG.JobID, DRG.CoerthanTorment)]
        DragoonCoerthanTormentCombo = 0,

        [CustomComboInfo("Chaos Thrust Combo", "Replace Chaos Thrust with its combo chain", DRG.JobID, DRG.ChaosThrust)]
        DragoonChaosThrustCombo = 1,

        [CustomComboInfo("Full Thrust Combo", "Replace Full Thrust with its combo chain", DRG.JobID, DRG.FullThrust)]
        DragoonFullThrustCombo = 2,

        #endregion
        // ====================================================================================
        #region DARK KNIGHT

        [CustomComboInfo("[E] Souleater Combo", "Replace Souleater with its combo chain", DRK.JobID, DRK.Souleater)]
        DarkSouleaterCombo = 3,

        [CustomComboInfo("[C] Stalwart Soul Combo", "Replace Stalwart Soul with its combo chain", DRK.JobID, DRK.StalwartSoul)]
        DarkStalwartSoulCombo = 4,

        [CustomComboInfo("[oGCD] oGCDs", "BloodWeapon/Edge -> Abyssal Drain -> Carve and Spit", DRK.JobID, DRK.AbyssalDrain)]
        DRKoGCDFeature = 57,

        // [CustomComboInfo("Unused", "Replace AoE combo with gauge spender if you are about to overcap", DRK.JobID, DRK.StalwartSoul)]
        // DRKOvercapFeature = 85,

        #endregion
        // ====================================================================================
        #region GUNBREAKER

        [CustomComboInfo("Solid Barrel Combo", "Replace Solid Barrel with its combo chain", GNB.JobID, GNB.SolidBarrel)]
        GunbreakerSolidBarrelCombo = 20,

        [CustomComboInfo("Wicked Talon Combo", "Replace Wicked Talon with its combo chain", GNB.JobID, GNB.WickedTalon)]
        GunbreakerGnashingFangCombo = 21,

        [CustomComboInfo("Wicked Talon Continuation", "In addition to the Wicked Talon combo chain, put Continuation moves on Wicked Talon when appropriate", GNB.JobID, GNB.WickedTalon)]
        GunbreakerGnashingFangCont = 52,

        [CustomComboInfo("Demon Slaughter Combo", "Replace Demon Slaughter with its combo chain", GNB.JobID, GNB.DemonSlaughter)]
        GunbreakerDemonSlaughterCombo = 22,

        [CustomComboInfo("Fated Circle Feature", "In addition to the Demon Slaughter combo, add Fated Circle when charges are full", GNB.JobID, GNB.DemonSlaughter)]
        GunbreakerFatedCircleFeature = 30,

        [CustomComboInfo("Burst Strike to Bloodfest Feature", "Replace Burst Strike with Bloodfest if you have no powder gauge.", GNB.JobID, GNB.BurstStrike)]
        GunbreakerBloodfestOvercapFeature = 83,

        [CustomComboInfo("No Mercy Feature", "Replace No Mercy with Bow Shock, and then Sonic Break, while No Mercy is active.", GNB.JobID, GNB.NoMercy)]
        GunbreakerNoMercyFeature = 84,

        #endregion
        // ====================================================================================
        #region MACHINIST

        [CustomComboInfo("(Heated) Shot Combo", "Replace either form of Clean Shot with its combo chain", MCH.JobID, MCH.CleanShot, MCH.HeatedCleanShot)]
        MachinistMainCombo = 23,

        [CustomComboInfo("Spread Shot Heat", "Replace Spread Shot with Auto Crossbow when overheated", MCH.JobID, MCH.SpreadShot)]
        MachinistSpreadShotFeature = 24,

        [CustomComboInfo("Hypercharge Feature", "Replace Heat Blast and Auto Crossbow with Hypercharge when not overheated", MCH.JobID, MCH.HeatBlast, MCH.AutoCrossbow)]
        MachinistOverheatFeature = 47,

        [CustomComboInfo("Overdrive Feature", "Replace Rook Autoturret and Automaton Queen with Overdrive while active", MCH.JobID, MCH.RookAutoturret, MCH.AutomatonQueen)]
        MachinistOverdriveFeature = 58,

        [CustomComboInfo("Gauss Round / Ricochet Feature", "Replace Gauss Round and Ricochet with one or the other depending on which has more charges", MCH.JobID, MCH.GaussRound, MCH.Ricochet)]
        MachinistGaussRoundRicochetFeature = 66,

        #endregion
        // ====================================================================================
        #region MONK

        [CustomComboInfo("Monk AoE Combo", "Replaces Rockbreaker with the AoE combo chain, or Rockbreaker when Perfect Balance is active", MNK.JobID, MNK.Rockbreaker)]
        MnkAoECombo = 54,

        // [CustomComboInfo("Monk Bootshine Feature", "Replaces Dragon Kick with Bootshine if both a form and Leaden Fist are up.", MNK.JobID, MNK.DragonKick)]
        // MnkBootshineFeature = 82,

        #endregion
        // ====================================================================================
        #region NINJA

        [CustomComboInfo("Armor Crush Combo", "Replace Armor Crush with its combo chain", NIN.JobID, NIN.ArmorCrush)]
        NinjaArmorCrushCombo = 17,

        [CustomComboInfo("Aeolian Edge Combo", "Replace Aeolian Edge with its combo chain", NIN.JobID, NIN.AeolianEdge)]
        NinjaAeolianEdgeCombo = 18,

        [CustomComboInfo("Hakke Mujinsatsu Combo", "Replace Hakke Mujinsatsu with its combo chain", NIN.JobID, NIN.HakkeMujinsatsu)]
        NinjaHakkeMujinsatsuCombo = 19,

        [CustomComboInfo("Dream to Assassinate", "Replace Dream Within a Dream with Assassinate when Assassinate Ready", NIN.JobID, NIN.DreamWithinADream)]
        NinjaAssassinateFeature = 45,

        // [CustomComboInfo("Kassatsu to Trick", "Replaces Kassatsu with Trick Attack while Suiton or Hidden is up.\nCooldown tracking plugin recommended.", NIN.JobID, NIN.Kassatsu)]
        // NinjaKassatsuTrickFeature = 87,

        // [CustomComboInfo("Ten Chi Jin to Meisui", "Replaces Ten Chi Jin (the move) with Meisui while Suiton is up.\nCooldown tracking plugin recommended.", NIN.JobID, NIN.TenChiJin)]
        // NinjaTCJMeisuiFeature = 88,

        // [CustomComboInfo("Kassatsu Chi/Jin Feature", "Replaces Chi with Jin while Kassatsu is up if you have Enhanced Kassatsu.", NIN.JobID, NIN.Chi)]
        // NinjaKassatsuChiJinFeature = 89,

        // [CustomComboInfo("Hide to Mug", "Replaces Hide with Mug while in combat.", NIN.JobID, NIN.Hide)]
        // NinjaHideMugFeature = 90,

        // [CustomComboInfo("Aeolian to Ninjutsu Feature", "Replaces Aeolian Edge (combo) with Ninjutsu if any Mudra are used.", NIN.JobID, NIN.AeolianEdge)]
        // NinjaNinjutsuFeature = 91,

        // [CustomComboInfo("GCDs to Ninjutsu Feature", "Every GCD combo becomes Ninjutsu while Mudras are being used.", NIN.JobID, NIN.AeolianEdge, NIN.ArmorCrush, NIN.HakkeMujinsatsu)]
        // NinjaGCDNinjutsuFeature = 92,

        #endregion
        // ====================================================================================
        #region PALADIN

        [CustomComboInfo("Goring Blade Combo", "Replace Goring Blade with its combo chain", PLD.JobID, PLD.GoringBlade)]
        PaladinGoringBladeCombo = 5,

        [CustomComboInfo("Royal Authority Combo", "Replace Royal Authority/Rage of Halone with its combo chain", PLD.JobID, PLD.RoyalAuthority, PLD.RageOfHalone)]
        PaladinRoyalAuthorityCombo = 6,

        [CustomComboInfo("Atonement Feature", "Replace Royal Authority with Atonement when under the effect of Sword Oath", PLD.JobID, PLD.RoyalAuthority)]
        PaladinAtonementFeature = 59,

        [CustomComboInfo("Prominence Combo", "Replace Prominence with its combo chain", PLD.JobID, PLD.Prominence)]
        PaladinProminenceCombo = 7,

        [CustomComboInfo("Requiescat Confiteor", "Replace Requiescat with Confiter while under the effect of Requiescat", PLD.JobID, PLD.Requiescat)]
        PaladinRequiescatCombo = 55,

        [CustomComboInfo("Requiescat Feature", "Replace Royal Authority/Goring Blade combo with Holy Spirit and Prominence combo with Holy Circle while Requiescat is active.\nRequires said combos to be activated to work.", PLD.JobID, PLD.RoyalAuthority, PLD.GoringBlade, PLD.Prominence)]
        PaladinRequiescatFeature = 86,

        [CustomComboInfo("Confiteor Feature", "Replace Holy Spirit/Circle with Confiteor while MP is under 4000 and Requiescat is up.", PLD.JobID, PLD.HolySpirit, PLD.HolyCircle)]
        PaladinConfiteorFeature = 69,
        
        [CustomComboInfo("oGCD Feature", "Spirits Within and Circle of Scorn on 1 button.", PLD.JobID, PLD.SpiritsWithin)]
        PaladinoGCDFeature = 96,

        #endregion
        // ====================================================================================
        #region RED MAGE

        [CustomComboInfo("Red Mage AoE Combo", "Replaces Veraero/Verthunder 2 with Impact when Dualcast or Swiftcast are active", RDM.JobID, RDM.Veraero2)]
        RedMageAoECombo = 48,

        [CustomComboInfo("Redoublement combo", "Replaces Redoublement with its combo chain, following enchantment rules", RDM.JobID, RDM.Redoublement)]
        RedMageMeleeCombo = 49,

        [CustomComboInfo("Redoublement Combo Plus", "Replaces Redoublement with Verflare/Verholy after Enchanted Redoublement, whichever is more appropriate.\nRequires Redoublement Combo.", RDM.JobID, RDM.Redoublement)]
        RedMageMeleeComboPlus = 68,

        [CustomComboInfo("Verproc into Jolt", "Replaces Verstone/Verfire with Jolt/Scorch when no proc is available.", RDM.JobID, RDM.Verstone)]
        RedMageVerprocCombo = 53,

        [CustomComboInfo("RDM OGCDs", "Fleche -> Contre Sixte -> tbc.", RDM.JobID, RDM.Fleche)]
        RDMoGCD = 93,

        [CustomComboInfo("Verproc into Jolt Plus Opener Feature", "Turns Verfire into Verthunder when out of combat.\nRequires Verproc into Jolt Plus.", RDM.JobID, RDM.Verfire)]
        RedMageVerprocOpenerFeature = 94,

        #endregion
        // ====================================================================================
        #region SAMURAI

        [CustomComboInfo("Yukikaze Combo", "Replace Yukikaze with its combo chain", SAM.JobID, SAM.Yukikaze)]
        SamuraiYukikazeCombo = 11,

        [CustomComboInfo("Gekko Combo", "Replace Gekko with its combo chain", SAM.JobID, SAM.Gekko)]
        SamuraiGekkoCombo = 12,

        [CustomComboInfo("Kasha Combo", "Replace Kasha with its combo chain", SAM.JobID, SAM.Kasha)]
        SamuraiKashaCombo = 13,

        [CustomComboInfo("Mangetsu Combo", "Replace Mangetsu with its combo chain", SAM.JobID, SAM.Mangetsu)]
        SamuraiMangetsuCombo = 14,

        [CustomComboInfo("Oka Combo", "Replace Oka with its combo chain", SAM.JobID, SAM.Oka)]
        SamuraiOkaCombo = 15,

        [CustomComboInfo("Seigan to Third Eye", "Replace Seigan with Third Eye when not procced", SAM.JobID, SAM.MercifulEyes)]
        SamuraiThirdEyeFeature = 51,

        [CustomComboInfo("Jinpu/Shifu Feature", "Replace Meikyo Shisui with Jinpu or Shifu depending on what is needed.", SAM.JobID, SAM.MeikyoShisui)]
        SamuraiJinpuShifuFeature = 81,

        [CustomComboInfo("Tsubame-gaeshi to Iaijutsu", "Replace Tsubame-gaeshi with Iaijutsu when Sen is empty", SAM.JobID, SAM.TsubameGaeshi)]
        SamuraiTsubameGaeshiIaijutsuFeature = 60,

        [CustomComboInfo("Tsubame-gaeshi to Shoha", "Replace Tsubame-gaeshi with Shoha when meditation is 3", SAM.JobID, SAM.TsubameGaeshi)]
        SamuraiTsubameGaeshiShohaFeature = 61,

        [CustomComboInfo("Iaijutsu to Tsubame-gaeshi", "Replace Iaijutsu with Tsubame-gaeshi when Sen is not empty\n(Use either the Tsubame-gaeshi version or this)", SAM.JobID, SAM.Iaijutsu)]
        SamuraiIaijutsuTsubameGaeshiFeature = 64,

        [CustomComboInfo("Hissatsu Button", "Shoha and various Hissatsu on Shinten", SAM.JobID, SAM.HissatsuShinten)]
        SamuraiHissatsuFeature = 65,

        #endregion
        // ====================================================================================
        #region SCHOLAR

        [CustomComboInfo("Seraph Fey Blessing/Consolation", "Change Fey Blessing into Consolation when Seraph is out", SCH.JobID, SCH.FeyBless)]
        ScholarSeraphConsolationFeature = 29,

        [CustomComboInfo("ED Aetherflow", "Change Energy Drain into Aetherflow when you have no more Aetherflow stacks", SCH.JobID, SCH.EnergyDrain)]
        ScholarEnergyDrainFeature = 37,
        
        [CustomComboInfo("Physick + Adlo", "Use Physick when target has Adlo shield", SCH.JobID, SCH.Adloquium)]
        AdloSaver = 97,        
        
        [CustomComboInfo("SingleTargetGCD", "Ruin, and Bio when no DOT up", SCH.JobID, SCH.Bio, SCH.Bio2, SCH.Biolysis)]
        SingleTargetGCD = 98,
        
        [CustomComboInfo("SingleTargetoGCD", "Aetherflow, Excogitation, Lustrate, ", SCH.JobID, SCH.Lustrate)]
        SingleTargetoGCD = 99,

        [CustomComboInfo("AoEoGCD", "Whispering Dawn, Aetherflow, Indomitability", SCH.JobID, SCH.Indomitability)]
        AoEoGCD = 100,
        
        #endregion
        // ====================================================================================
        #region SUMMONER

        [CustomComboInfo("Demi-summon combiners [F]", "TriDisaster, Dreadwyrm Trance, Summon Bahamut, Firebird Trance, and Deathflare are now one button.", SMN.JobID, SMN.DreadwyrmTrance)]
        SummonerDemiCombo = 28,

        [CustomComboInfo("Ruin 2 [Q]", "Ruin4 -> Egi Assault 1/2 -> Ruin 3 under trance -> BoP", SMN.JobID, SMN.EgiAssault)]
        SMNSingleTargetGCD = 80,

        [CustomComboInfo("Ruin 3 [E]", "Ruin 3 -> BoP", SMN.JobID, SMN.EgiAssault2)]
        SMNSingleTargetGCD2 = 38,

        [CustomComboInfo("ED Fester [R]", "Energy Drain -> Fester -> Enkindle Bahamut/Phoenix", SMN.JobID, SMN.Fester)]
        SMNSingleTargetoGCD = 39,

        [CustomComboInfo("ES Painflare [X]", "Energy Syphon -> Painflare -> Enkindle Bahamut/Phoenix", SMN.JobID, SMN.Painflare)]
        SMNAoEoGCD = 40,
        
        [CustomComboInfo("AoE Combo [C]", "Egi Assault 1/2 -> Outburst under trance -> BoP -> Outburst", SMN.JobID, SMN.Outburst)]
        SMNAoEGCD = 101,
        
        [CustomComboInfo("More Dots [Z]", "Tri-Disaster -> Bio -> Miasma", SMN.JobID, SMN.Miasma, SMN.Miasma3)]
        SMNDots = 102,

        #endregion
        // ====================================================================================
        #region WARRIOR

        [CustomComboInfo("Storms Path Combo", "Replace Storms Path with its combo chain", WAR.JobID, WAR.StormsPath)]
        WarriorStormsPathCombo = 8,

        [CustomComboInfo("Storms Eye Combo", "Replace Storms Eye with its combo chain", WAR.JobID, WAR.StormsEye)]
        WarriorStormsEyeCombo = 9,

        [CustomComboInfo("Mythril Tempest Combo", "Replace Mythril Tempest with its combo chain", WAR.JobID, WAR.MythrilTempest)]
        WarriorMythrilTempestCombo = 10,

        [CustomComboInfo("Warrior Gauge Overcap Feature", "Replace Single-target or AoE combo with gauge spender if you are about to overcap and are before a step of a combo that would generate beast gauge", WAR.JobID, WAR.MythrilTempest, WAR.StormsEye, WAR.StormsPath)]
        WarriorGaugeOvercapFeature = 78,

        [CustomComboInfo("Inner Release Feature", "Replace Single-target and AoE combo with Fell Cleave/Decimate during Inner Release", WAR.JobID, WAR.MythrilTempest, WAR.StormsPath)]
        WarriorInnerReleaseFeature = 79,

        [CustomComboInfo("Nascent Flash Feature", "Replace Nascent Flash with Raw intuition when level synced below 76", WAR.JobID, WAR.NascentFlash)]
        WarriorNascentFlashFeature = 67,

        #endregion
        // ====================================================================================
        #region WHITE MAGE

        [CustomComboInfo("oGCD", "oGCD - Lucid Dreaming if under 8000 MP - assize on CD - Presence of Mind - Divine Benison", WHM.JobID, WHM.LucidDreaming)]
        WHMoGCD = 35,

        [CustomComboInfo("Resource Spender", "Resource Spender - Tetra - Lily - Cure 2", WHM.JobID, WHM.Cure2)]
        WHMHeal = 36,

        [CustomComboInfo("GCD Weave", "Lily - Dia", WHM.JobID, WHM.AfflatusMisery)]
        WHMDia = 76,

        [CustomComboInfo("Cure 3", "Plenary Indulgence - Lily - Cure 3", WHM.JobID, WHM.Cure3)]
        WHMCure3 = 77,

        [CustomComboInfo("AoE Healing", "Plenary Indulgence - Lily - Medica2 - Medica", WHM.JobID, WHM.Medica2)]
        WHMAoEHealing = 107,

        #endregion
        // ====================================================================================
        
        #region BLUE MAGE

        [CustomComboInfo("Song of Torment Combo", "Bristle -> Song of Torment", BLU.JobID, BLU.Bristle)]
        BluDoT = 103,

        [CustomComboInfo("Final Sting Combo", "Moon Flute -> Whistle -> Final Sting", BLU.JobID, BLU.FinalSting)]
        BluLimitBreak = 104,

        [CustomComboInfo("Primal Combo", "Moon Flute -> Primals", BLU.JobID, BLU.Nightbloom)]
        BluBurstCombo = 105,
        
        [CustomComboInfo("Moon Flute Opener", "Moon Flute Opener", BLU.JobID, BLU.TripleTrident)]
        BluTridentCombo = 106,

        #endregion
        // ====================================================================================
        
    }


    internal class CustomComboInfoAttribute : Attribute
    {
        internal CustomComboInfoAttribute(string fancyName, string description, byte jobID, params uint[] actionIDs)
        {
            FancyName = fancyName;
            Description = description;
            JobID = jobID;
            ActionIDs = actionIDs;
        }

        public string FancyName { get; }
        public string Description { get; }
        public byte JobID { get; }
        public string JobName => JobIDToName(JobID);
        public uint[] ActionIDs { get; }

        private static string JobIDToName(byte key)
        {
            return key switch
            {
                1 => "Gladiator",
                2 => "Pugilist",
                3 => "Marauder",
                4 => "Lancer",
                5 => "Archer",
                6 => "Conjurer",
                7 => "Thaumaturge",
                8 => "Carpenter",
                9 => "Blacksmith",
                10 => "Armorer",
                11 => "Goldsmith",
                12 => "Leatherworker",
                13 => "Weaver",
                14 => "Alchemist",
                15 => "Culinarian",
                16 => "Miner",
                17 => "Botanist",
                18 => "Fisher",
                19 => "Paladin",
                20 => "Monk",
                21 => "Warrior",
                22 => "Dragoon",
                23 => "Bard",
                24 => "White Mage",
                25 => "Black Mage",
                26 => "Arcanist",
                27 => "Summoner",
                28 => "Scholar",
                29 => "Rogue",
                30 => "Ninja",
                31 => "Machinist",
                32 => "Dark Knight",
                33 => "Astrologian",
                34 => "Samurai",
                35 => "Red Mage",
                36 => "Blue Mage",
                37 => "Gunbreaker",
                38 => "Dancer",
                _ => "Unknown",
            };
        }
    }

    internal static class CustomComboPresetExtensions
    {
        public static CustomComboInfoAttribute GetInfo(this CustomComboPreset preset) => preset.GetAttribute<CustomComboInfoAttribute>();
    }
}
