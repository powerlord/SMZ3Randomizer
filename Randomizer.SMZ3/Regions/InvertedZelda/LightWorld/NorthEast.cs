using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.InvertedZelda.LightWorld {

    class NorthEast : Zelda.LightWorld.NorthEast {

        public NorthEast(World world, Config config) : base(world, config) {
            Locations.Get("King Zora").CanAccess(items =>
                items.MoonPearl && (items.CanLiftLight() || items.Flippers));
            Locations.Get("Zora's Ledge").CanAccess(items =>
                items.MoonPearl && items.Flippers);
            Locations.Get("Waterfall Fairy - Left").CanAccess(items =>
                items.MoonPearl && items.Flippers);
            Locations.Get("Waterfall Fairy - Right").CanAccess(items =>
                items.MoonPearl && items.Flippers);
            Locations.Get("Potion Shop").CanAccess(items =>
                items.MoonPearl && items.Mushroom);
            Locations.Get("Sahasrahla's Hut - Left").CanAccess(items => items.MoonPearl);
            Locations.Get("Sahasrahla's Hut - Middle").CanAccess(items => items.MoonPearl);
            Locations.Get("Sahasrahla's Hut - Right").CanAccess(items => items.MoonPearl);
        }

        public override bool CanEnter(Progression items) {
            return World.CanAquire(items, Agahnim) ||
                items.MoonPearl && (items.CanLiftHeavy() || items.Hammer && items.CanLiftLight());
        }

    }

}
