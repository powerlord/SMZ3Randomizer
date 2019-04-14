using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.Zelda {

    class DarkWorldSouth : Z3Region {

        public override string Name => "Dark World South";
        public override string Area => "Dark World";

        public DarkWorldSouth(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+82, 0x180148, LocationType.Regular, "Digging Game", CanBeLinkInDarkWorld),
                new Location(this, 256+83, 0x330C7, LocationType.Regular, "Stumpy",
                    items => items.MoonPearl ||
                        Logic.OwYba && items.Bottle ||
                        Logic.BunnyRevive && items.CanBunnyRevive() ||
                        Logic.MirrorWrap && items.Mirror),
                new Location(this, 256+84, 0xEB1E, LocationType.Regular, "Hype Cave - Top", CanBeLinkInDarkWorld),
                new Location(this, 256+85, 0xEB21, LocationType.Regular, "Hype Cave - Middle Right", CanBeLinkInDarkWorld),
                new Location(this, 256+86, 0xEB24, LocationType.Regular, "Hype Cave - Middle Left", CanBeLinkInDarkWorld),
                new Location(this, 256+87, 0xEB27, LocationType.Regular, "Hype Cave - Bottom", CanBeLinkInDarkWorld),
                new Location(this, 256+88, 0x180011, LocationType.Regular, "Hype Cave - NPC", CanBeLinkInDarkWorld),
            };
        }

        bool CanBeLinkInDarkWorld(Progression items) {
            return items.MoonPearl ||
                Logic.OwYba && items.Bottle ||
                Logic.BunnyRevive && items.CanBunnyRevive();
        }

        public override bool CanEnter(Progression items) {
            return
                Logic.OwYba && items.Bottle ||
                World.CanEnter<DarkWorldNorthWest>(items) ||
                World.CanEnter<DarkWorldNorthEast>(items) && (
                    items.MoonPearl ||
                    Logic.BunnyRevive && items.CanBunnyRevive()
                ) && (
                    items.Hammer ||
                    Logic.SuperSpeed && items.CanSpinSpeed() && (Logic.FakeFlipper || items.Flippers)
                );
            // Todo: access from SM and untangle aga (v31.0.3 changes removed the need for untangling)
            //return items.MoonPearl && ((
            //        World.CanAquire(items, Agahnim) ||
            //        items.CanAccessDarkWorldPortal(Config) && items.Flippers
            //    ) && (items.Hammer || items.Hookshot && (items.Flippers || items.CanLiftLight())) ||
            //    items.Hammer && items.CanLiftLight() ||
            //    items.CanLiftHeavy()
            //) || Logic switch {
            //    Owg => items.Mirror || items.Boots && items.MoonPearl,
            //    Mg => true,
            //    _ => false,
            //} && World.CanEnter("Light World Death Mountain West", items);
       }

    }

}
