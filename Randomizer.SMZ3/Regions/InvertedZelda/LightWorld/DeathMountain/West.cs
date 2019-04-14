namespace Randomizer.SMZ3.Regions.InvertedZelda.LightWorld.DeathMountain {

    class West : Zelda.LightWorld.DeathMountain.West {

        public West(World world, Config config) : base(world, config) {
            var lightWorldDeathMountainEast = World.GetRegion("Light World Death Mountain East");
            Locations.Get("Ether Tablet").CanAccess(lightWorldDeathMountainEast, items =>
                items.MoonPearl && items.Hammer && items.Book && items.MasterSword);
            Locations.Get("Spectacle Rock").CanAccess(lightWorldDeathMountainEast, items =>
                items.MoonPearl && items.Hammer);
        }

        public override bool CanEnter(Progression items) {
            return World.CanEnter("Dark World Death Mountain West", items);
        }

    }

}
