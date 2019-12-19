namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class SwampPalace : Zelda.SwampPalace {

        public SwampPalace(World world, Config config) : base(world, config) { }

        public override bool CanEnter(Progression items) {
            return
                items.Flippers && World.CanEnter<LightWorldSouth>(items) && (
                    items.MoonPearl ||
                    Logic.BunnyRevive && items.CanBunnyRevive() ||
                    Logic.OwYba && items.Bottle ||
                    Logic.SuperBunny && items.Mirror
                ) && (
                    items.Mirror ||
                    Logic.OneFrameClipUw && items.MoonPearl &&
                        EnterFromMire(items) && (items.BigKeyMM || items.BigKeyTH) &&
                        World.LocationIn<LightWorldDeathMountainWest>("Old Man").Available(items) && (
                            Logic.OneFrameClipOw ||
                            Logic.BootsClip && items.Boots ||
                            Logic.SuperSpeed && items.CanSpinSpeed()
                        )
                );
        }

    }

}
