namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class ThievesTown : Zelda.ThievesTown {

        public ThievesTown(World world, Config config) : base(world, config) { }

        public override bool CanEnter(Progression items) {
            return World.CanEnter<DarkWorldNorthWest>(items);
        }

    }

}
