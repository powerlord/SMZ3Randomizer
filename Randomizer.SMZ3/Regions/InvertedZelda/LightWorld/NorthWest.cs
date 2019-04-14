using System;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.InvertedZelda.LightWorld {

    class NorthWest : Zelda.LightWorld.NorthWest {

        public NorthWest(World world, Config config) : base(world, config) {
            Locations.Get("Mushroom").CanAccess(items => items.MoonPearl);
            Locations.Get("Lost Woods Hideout").CanAccess(items => items.MoonPearl);
            Locations.Get("Lumberjack Tree").CanAccess(items =>
                items.MoonPearl && World.CanAquire(items, Agahnim) && items.Boots);
            Locations.Get("Pegasus Rocks").CanAccess(items =>
                items.MoonPearl && items.Boots);
            Locations.Get("Graveyard Ledge").CanAccess(items => items.MoonPearl);
            Locations.Get("King's Tomb").CanAccess(items =>
                items.MoonPearl && items.CanLiftHeavy() && items.Boots);
            Locations.Get("Kakariko Well - Top").CanAccess(items => items.MoonPearl);
            Locations.Get("Kakariko Well - Left").CanAccess(items => items.MoonPearl);
            Locations.Get("Kakariko Well - Middle").CanAccess(items => items.MoonPearl);
            Locations.Get("Kakariko Well - Right").CanAccess(items => items.MoonPearl);
            Locations.Get("Kakariko Well - Bottom").CanAccess(items => items.MoonPearl);
            Locations.Get("Blind's Hideout - Top").CanAccess(items => items.MoonPearl);
            Locations.Get("Blind's Hideout - Far Left").CanAccess(items => items.MoonPearl);
            Locations.Get("Blind's Hideout - Left").CanAccess(items => items.MoonPearl);
            Locations.Get("Blind's Hideout - Right").CanAccess(items => items.MoonPearl);
            Locations.Get("Blind's Hideout - Far Right").CanAccess(items => items.MoonPearl);
            Locations.Get("Chicken House").CanAccess(items => items.MoonPearl);
            Locations.Get("Kakariko Tavern").CanAccess(items => items.MoonPearl);
            Locations.Get("Magic Bat").CanAccess(items =>
                 items.MoonPearl && items.Hammer && items.Powder);
        }

        public override bool CanEnter(Progression items) {
            return World.CanEnter("Light World North East", items);
        }

    }

}
