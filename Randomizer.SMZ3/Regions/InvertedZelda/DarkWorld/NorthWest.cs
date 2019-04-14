namespace Randomizer.SMZ3.Regions.InvertedZelda.DarkWorld {

    class NorthWest : Zelda.DarkWorld.NorthWest {

        public NorthWest(World world, Config config) : base(world, config) {
            Locations.Get("Bumper Cave").CanAccess(items =>
                items.CanLiftLight() && items.Cape && items.MoonPearl && items.Mirror &&
                World.CanEnter("Light World North West", items));
            Locations.Get("Hammer Pegs").CanAccess(items =>
                items.Hammer && (
                    items.CanLiftHeavy() ||
                    items.Mirror && World.CanEnter("Light World North West", items)));
            Locations.Get("Blacksmith").CanAccess(items =>
                (items.CanLiftHeavy() || items.Mirror) &&
                World.CanEnter("Light World North West", items));
            Locations.Get("Purple Chest").CanAccess(items =>
                (items.CanLiftHeavy() || items.Mirror) &&
                World.CanEnter("Light World North West", items) &&
                World.CanEnter("Light World South", items));
        }

        public override bool CanEnter(Progression items) {
            return true;
        }

    }

}
