using static System.Reflection.BindingFlags;

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

        public static SMLogic Casual { get; }
        public static SMLogic Basic { get; }
        public static SMLogic Advanced { get; }

        static SMLogic() {
            Casual = new SMLogic(0, "Casual");
            Basic = new SMLogic(1, "Basic") {
                TrickyWallJump = true,
                SuitlessWater = true,
                SuitlessLava = true,
                IceClip = true,
                GreenGate = true,
                TrickyEnemyFreeze = true,
                HellRun = true,
                AdditionalDamage = true,
                MidAirMorph = true,
                SoftlockRisk = true,
                ShortCharge = true,
                MockBall = true,
            };
            Advanced = new SMLogic(2, "Advanced", Basic) {
                SpringBallJump = true,
                ClimbCwj = true,
                SnailClip = true,
                PseudoScrew = true,
                ThreeTapCharge = true,
                GuidedEnemyFreeze = true,
                ExcessiveDamage = true,
            };
        }

        public bool TrickyWallJump { get; private set; }
        public bool SuitlessWater { get; private set; }
        public bool IceClip { get; private set; }
        public bool GreenGate { get; private set; }
        public bool TrickyEnemyFreeze { get; private set; }
        public bool GuidedEnemyFreeze { get; private set; }
        public bool HellRun { get; private set; }
        public bool AdditionalDamage { get; private set; }
        public bool ExcessiveDamage { get; private set; }
        public bool MidAirMorph { get; private set; }
        public bool SoftlockRisk { get; private set; }
        public bool ShortCharge { get; private set; }
        public bool ThreeTapCharge { get; private set; }
        public bool SuitlessLava { get; private set; }
        public bool MockBall { get; private set; }
        public bool SpringBallJump { get; private set; }
        public bool ClimbCwj { get; private set; }
        public bool SnailClip { get; private set; }
        public bool PseudoScrew { get; private set; }

        public SMLogic(int ord, string name) : base(ord, name) { }
        public SMLogic(int ord, string name, SMLogic template) : base(ord, name) {
            foreach (var prop in typeof(SMLogic).GetProperties(Public|Instance|DeclaredOnly)) {
                prop.SetValue(this, prop.GetValue(template));
            }
        }

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
