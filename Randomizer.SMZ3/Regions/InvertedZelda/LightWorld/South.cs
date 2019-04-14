namespace Randomizer.SMZ3.Regions.InvertedZelda.LightWorld {

    class South : Zelda.LightWorld.South {

        public South(World world, Config config) : base(world, config) {
            Locations.Get("Link's House").Region = World.GetRegion("Dark World South");

            Locations.Get("Maze Race").CanAccess(items => items.MoonPearl);
            Locations.Get("Library").CanAccess(items =>
                items.MoonPearl && items.Boots);
            Locations.Get("Flute Spot").CanAccess(items =>
                items.MoonPearl && items.Shovel);
            Locations.Get("South of Grove").CanAccess(items => items.MoonPearl);
            Locations.Get("Aginah's Cave").CanAccess(items => items.MoonPearl);
            Locations.Get("Mini Moldorm Cave - Far Left").CanAccess(items => items.MoonPearl);
            Locations.Get("Mini Moldorm Cave - Left").CanAccess(items => items.MoonPearl);
            Locations.Get("Mini Moldorm Cave - NPC").CanAccess(items => items.MoonPearl);
            Locations.Get("Mini Moldorm Cave - Right").CanAccess(items => items.MoonPearl);
            Locations.Get("Mini Moldorm Cave - Far Right").CanAccess(items => items.MoonPearl);
            Locations.Get("Desert Ledge").CanAccess(items =>
                items.MoonPearl && World.CanEnter("Desert Palace", items));
            Locations.Get("Checkerboard Cave").CanAccess(items =>
                items.MoonPearl && items.CanLiftLight());
            Locations.Get("Bombos Tablet").CanAccess(items =>
                items.Book && items.MasterSword);
            Locations.Get("Floodgate Chest").CanAccess(items => items.MoonPearl);
            Locations.Get("Sunken Treasure").CanAccess(items => items.MoonPearl);
            Locations.Get("Lake Hylia Island").CanAccess(items =>
                items.MoonPearl && items.Flippers);
            Locations.Get("Hobo").CanAccess(items =>
                items.MoonPearl && items.Flippers);
            Locations.Get("Ice Rod Cave").CanAccess(items => items.MoonPearl);
        }

        public override bool CanEnter(Progression items) {
            return World.CanEnter("Light World North East", items);
        }

    }

}
