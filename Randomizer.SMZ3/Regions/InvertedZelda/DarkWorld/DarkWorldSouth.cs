namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class DarkWorldSouth : Zelda.DarkWorldSouth {

        public DarkWorldSouth(World world, Config config) : base(world, config) {
            Locations.Add(new Location(this, 256 + 243, 0xE9BC, LocationType.Regular, "Link's House"));
            foreach (var location in Locations) {
                location.CanAccess();
            }
        }

        public override bool CanEnter(Progression items) {
            return true;
        }

    }

}
