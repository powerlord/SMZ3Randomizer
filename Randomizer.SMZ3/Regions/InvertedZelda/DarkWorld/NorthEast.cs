using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.InvertedZelda.DarkWorld {

    class NorthEast : Zelda.DarkWorld.NorthEast {

        public NorthEast(World world, Config config) : base(world, config) {
            Locations.Get("Catfish").CanAccess(items =>
                items.CanLiftLight());
            Locations.Get("Pyramid Fairy - Left").CanAccess(items =>
                items.Mirror && World.CanEnter("Light World South", items) &&
                items.MoonPearl && World.CanAquireAll(items, CrystalRed));
            Locations.Get("Pyramid Fairy - Right").CanAccess(items =>
                items.Mirror && World.CanEnter("Light World South", items) &&
                items.MoonPearl && World.CanAquireAll(items, CrystalRed));
        }

        public override bool CanEnter(Progression items) {
            return items.Hammer || items.Flippers ||
                items.Flute && items.MoonPearl && World.CanEnter("Light World North West", items) ||
                items.Mirror && World.CanEnter("Light World North East", items);
        }

    }

}
