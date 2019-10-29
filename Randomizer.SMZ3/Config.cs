namespace Randomizer.SMZ3 {

    enum Z3Logic {
        Nmg,
        Owg,
        Mg,
    }

    abstract class Logic {

        public int Ordinal { get; }
        public string Name { get; }
        public string Abbrevation => Name.Substring(0, 1);

        public Logic(int ordinal, string name) {
            Ordinal = ordinal;
            Name = name;
        }

    }

    class SMLogic : Logic {

        public static SMLogic Casual => new SMLogic(0, "Casual");
        public static SMLogic Basic => new SMLogic(1, "Basic") {
            Suitless = true,
            IceClip = true,
            GreenGate = true,
            FrozenEnemy = true,
            HellRun = true,
            CanTakeAdditionalDamage = true,
            CanMidAirMorph = true,
            SoftlockRisk = true,
            ShortCharge = true,
            LavaDive = true,
        };
        public static SMLogic Advanced => new SMLogic(2, "Advanced") {
            Suitless = true,
            IceClip = true,
            GreenGate = true,
            FrozenEnemy = true,
            HellRun = true,
            CanTakeAdditionalDamage = true,
            CanMidAirMorph = true,
            SoftlockRisk = true,
            ShortCharge = true,
            LavaDive = true,
            SpringBallJump = true,
            ClimbCwj = true,
            SnailClip = true,
            PseudoScrew = true,
            ThreeTapSpeed = true,
            FrozenHostile = true,
            CanTakeExcessiveDamage = true,
        };

        public bool Suitless { get; private set; }
        public bool IceClip { get; private set; }
        public bool GreenGate { get; private set; }
        public bool FrozenEnemy { get; private set; }
        public bool HellRun { get; private set; }
        public bool CanTakeAdditionalDamage { get; private set; }
        public bool CanTakeExcessiveDamage { get; private set; }
        public bool CanMidAirMorph { get; private set; }
        public bool SoftlockRisk { get; private set; }
        public bool ShortCharge { get; private set; }
        public bool LavaDive { get; private set; }
        public bool SpringBallJump { get; private set; }
        public bool ClimbCwj { get; private set; }
        public bool SnailClip { get; private set; }
        public bool PseudoScrew { get; private set; }
        public bool ThreeTapSpeed { get; private set; }
        public bool FrozenHostile { get; private set; }

        public SMLogic(int ord, string name) : base(ord, name) { }

    }

    enum Difficulty {
        Easy = -1,
        Normal = 0,
        Hard = 1,
        Expert = 2,
        Insane = 3,
    }

    enum GanonInvincible {
        Never,
        BeforeCrystals,
        BeforeAllDungeons,
        Always,
    }

    class Config {

        public bool Multiworld { get; set; } = false;
        public bool Keysanity { get; set; } = false;
        public Z3Logic Z3Logic { get; set; } = Z3Logic.Nmg;
        public SMLogic SMLogic { get; set; } = SMLogic.Advanced;
        public Difficulty Difficulty { get; set; } = Difficulty.Normal;
        public GanonInvincible GanonInvincible { get; set; } = GanonInvincible.BeforeCrystals;

    }

}
