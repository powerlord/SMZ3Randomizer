namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class DarkWorldNorthEast : Zelda.DarkWorldNorthEast {

        public DarkWorldNorthEast(World world, Config config) : base(world, config) {
            Location("Catfish").CanAccess(items =>
                items.CanLiftLight() ||
                Logic.OneFrameClipOw ||
                Logic.BootsClip && items.Boots
            );
            Location("Pyramid Fairy - Left").CanAccess(CanReachPyramidFairy);
            Location("Pyramid Fairy - Right").CanAccess(CanReachPyramidFairy);
        }

        bool CanReachPyramidFairy(Progression items) {
            var lightWorldSouth = World.Region<LightWorldSouth>();
            return lightWorldSouth.CanEnter(items) &&
                lightWorldSouth.CanAcquireRedBomb(items) &&
                items.Mirror;
        }

        public override bool CanEnter(Progression items) {
            return items.Hammer || items.Flippers ||
                items.Flute && World.CanEnter<LightWorldNorthWest>(items) || // Todo: glitch flute activation
                items.Mirror && World.CanEnter<LightWorldNorthEast>(items) ||
                Logic.OneFrameClipOw ||
                Logic.OwYba && items.Bottle ||
                Logic.BunnyRevive && items.CanBunnyRevive() ||
                Logic.BootsClip && items.Boots ||
                Logic.SuperSpeed && items.CanSpinSpeed() ||
                Logic.FakeFlipper ||
                Logic.WaterWalk && items.Boots;
        }

    }

}
