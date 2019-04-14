using static System.Reflection.BindingFlags;

namespace Randomizer.SMZ3 {

    abstract class Logic {

        public int Ordinal { get; }
        public string Name { get; }
        public string Abbrevation => Name.Substring(0, 1);

        public Logic(int ordinal, string name) {
            Ordinal = ordinal;
            Name = name;
        }

    }

    class Z3Logic : Logic {

        public static Z3Logic Nmg { get; }
        public static Z3Logic Ow { get; }
        public static Z3Logic Mg { get; }

        static Z3Logic() {
            Nmg = new Z3Logic(0, "Nmg");
            Ow = new Z3Logic(1, "Owg") {
                SuperBunny = true,
                FakeFlipper = true,
                SuperSpeed = true,
                BootsClip = true,
                MirrorClip = true,
                WaterWalk = true,
                BunnySurf = true,
                MirrorWrap = true,
                DungeonRevive = true,
            };
            Mg = new Z3Logic(2, "Mg", Ow) {
                BunnyRevive = true,
                OwYba = true,
                OneFrameClipOw = true,
                OneFrameClipUw = true,
            };
        }

        public bool SuperBunny { get; set; }
        public bool FakeFlipper { get; set; }
        public bool SuperSpeed { get; set; }
        public bool BootsClip { get; set; }
        public bool MirrorClip { get; set; }
        public bool WaterWalk { get; set; }
        public bool BunnySurf { get; set; }
        public bool BunnyRevive { get; set; }
        public bool MirrorWrap { get; set; }
        public bool DungeonRevive { get; set; }
        public bool OwYba { get; set; }
        public bool OneFrameClipOw { get; set; }
        public bool OneFrameClipUw { get; set; }

        public Z3Logic(int ord, string name) : base(ord, name) { }
        public Z3Logic(int ord, string name, Z3Logic template) : base(ord, name) {
            foreach (var prop in typeof(Z3Logic).GetProperties(Public|Instance|DeclaredOnly)) {
                prop.SetValue(this, prop.GetValue(template));
            }
        }
    }

    enum SMLogic {
        Casual,
        Basic,
        Advanced,
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
