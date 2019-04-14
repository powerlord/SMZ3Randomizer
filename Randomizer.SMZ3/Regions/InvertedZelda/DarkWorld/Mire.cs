namespace Randomizer.SMZ3.Regions.InvertedZelda.DarkWorld {

    class Mire : Zelda.DarkWorld.Mire {

        public Mire(World world, Config config) : base(world, config) {
            Locations.Get("Mire Shed - Left").CanAccess();
            Locations.Get("Mire Shed - Right").CanAccess();
        }

        public override bool CanEnter(Progression items) {
            return items.Flute && items.MoonPearl && World.CanEnter("Light World North West", items) ||
                items.Mirror && World.CanEnter("Light World South", items);
        }

    }

}
