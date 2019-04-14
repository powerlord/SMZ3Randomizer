namespace Randomizer.SMZ3.Regions.InvertedZelda.LightWorld.DeathMountain {

    class East : Zelda.LightWorld.DeathMountain.East {

        public East(World world, Config config) : base(world, config) {
            Locations.Get("Floating Island").CanAccess();
            Locations.Get("Spiral Cave").CanAccess(items => items.MoonPearl);
            Locations.Get("Paradox Cave Upper - Left").CanAccess(items => items.MoonPearl);
            Locations.Get("Paradox Cave Upper - Right").CanAccess(items => items.MoonPearl);
            Locations.Get("Paradox Cave Lower - Far Left").CanAccess(items => items.MoonPearl);
            Locations.Get("Paradox Cave Lower - Left").CanAccess(items => items.MoonPearl);
            Locations.Get("Paradox Cave Lower - Middle").CanAccess(items => items.MoonPearl);
            Locations.Get("Paradox Cave Lower - Right").CanAccess(items => items.MoonPearl);
            Locations.Get("Paradox Cave Lower - Far Right").CanAccess(items => items.MoonPearl);
            Locations.Get("Mimic Cave").CanAccess(items =>
                items.MoonPearl && items.Hammer);
        }

        public override bool CanEnter(Progression items) {
            return items.CanLiftHeavy() && World.CanEnter("Dark World Death Mountain East", items) ||
                items.MoonPearl && items.Hookshot && World.CanEnter("Light World Death Mountain West", items);
        }

    }

}
