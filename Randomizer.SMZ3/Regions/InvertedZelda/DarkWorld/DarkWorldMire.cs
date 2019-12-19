namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class DarkWorldMire : Zelda.DarkWorldMire {

        public DarkWorldMire(World world, Config config) : base(world, config) {
            Location("Mire Shed - Left").CanAccess();
            Location("Mire Shed - Right").CanAccess();
        }

        public override bool CanEnter(Progression items) {
            return
                items.Flute && World.CanEnter<LightWorldNorthWest>(items) || // Todo: glitch flute activation
                items.Mirror && World.CanEnter<LightWorldSouth>(items) ||
                Logic.OneFrameClipOw ||
                Logic.OwYba && items.Bottle ||
                Logic.BootsClip && items.Boots;
        }

    }

}
