namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class DarkWorldDeathMountainEast : Zelda.DarkWorldDeathMountainEast {

        public DarkWorldDeathMountainEast(World world, Config config) : base(world, config) {
            Location("Superbunny Cave - Top").CanAccess();
            Location("Superbunny Cave - Bottom").CanAccess();
        }

        protected override bool CanReachHookshotCave(Progression items) {
            return
                items.CanLiftLight() ||
                items.Mirror && World.CanEnter<LightWorldDeathMountainEast>(items) ||
                Logic.OneFrameClipOw ||
                Logic.BootsClip && items.Boots;
        }

        public override bool CanEnter(Progression items) {
            return World.CanEnter<DarkWorldDeathMountainWest>(items) ||
                items.Mirror && items.MoonPearl && items.Hookshot &&
                    World.CanEnter<LightWorldDeathMountainEast>(items);
        }

    }

}
