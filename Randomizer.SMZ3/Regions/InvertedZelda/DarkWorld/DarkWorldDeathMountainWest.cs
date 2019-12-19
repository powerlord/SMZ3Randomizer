namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class DarkWorldDeathMountainWest : Zelda.DarkWorldDeathMountainWest {

        public DarkWorldDeathMountainWest(World world, Config config) : base(world, config) {
            Location("Spike Cave").CanAccess(items =>
                items.Hammer && items.CanLiftLight() &&
                (items.CanExtendMagic() && items.Cape || items.Byrna));
        }

        public override bool CanEnter(Progression items) {
            return
                items.Flute && World.CanEnter<LightWorldNorthWest>(items) || // Todo: glitch flute activation
                items.CanLiftLight() && items.Lamp ||
                Logic.OneFrameClipOw ||
                Logic.BootsClip && items.Boots ||
                Logic.OwYba && items.Bottle;
        }

    }

}
