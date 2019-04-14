namespace Randomizer.SMZ3.Regions.InvertedZelda.DarkWorld.DeathMountain {

    class West : Zelda.DarkWorld.DeathMountain.West {

        public West(World world, Config config) : base(world, config) {
            Locations.Get("Spike Cave").CanAccess(items =>
                items.Hammer && items.CanLiftLight() &&
                (items.CanExtendMagic() && items.Cape || items.Byrna));
        }

        public override bool CanEnter(Progression items) {
            return items.Flute && items.MoonPearl && World.CanEnter("Light World North West", items) ||
                items.CanLiftLight() && items.Lamp;
        }

    }

}
