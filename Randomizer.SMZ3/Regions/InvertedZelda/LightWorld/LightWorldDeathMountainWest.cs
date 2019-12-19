namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class LightWorldDeathMountainWest : Zelda.LightWorldDeathMountainWest {

        public LightWorldDeathMountainWest(World world, Config config) : base(world, config) {
            Locations.Remove(Location("Ether Tablet"));
            Locations.Remove(Location("Spectacle Rock"));
        }

        public override bool CanEnter(Progression items) {
            return
                items.Flute && World.CanEnter<LightWorldNorthWest>(items) || // Todo: glitch flute activation
                items.CanLiftLight() && items.Lamp ||
                Logic.OneFrameClipOw ||
                Logic.OwYba && items.Bottle ||
                Logic.BootsClip && items.Boots;
        }

    }

}
