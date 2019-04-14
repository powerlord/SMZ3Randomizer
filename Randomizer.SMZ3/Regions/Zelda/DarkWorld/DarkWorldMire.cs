using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.Zelda {

    class DarkWorldMire : Z3Region {

        public override string Name => "Dark World Mire";
        public override string Area => "Dark World";

        public DarkWorldMire(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+89, 0xEA73, LocationType.Regular, "Mire Shed - Left",
                    CanAccessItemsInMireShed),
                new Location(this, 256+90, 0xEA76, LocationType.Regular, "Mire Shed - Right",
                    CanAccessItemsInMireShed),
            };
        }

        bool CanAccessItemsInMireShed(Progression items) {
            return items.MoonPearl ||
                Logic.SuperBunny && items.Mirror ||
                Logic.OwYba && items.Bottle && (
                    Logic.OneFrameClipOw ||
                    /*items.Bottles >= 2 ||*/
                    Logic.BunnyRevive && items.Mirror && items.Bugnet ||
                    Logic.BootsClip && items.Boots
                );
        }

        public override bool CanEnter(Progression items) {
            return Logic.OneFrameClipOw ||
                Logic.OwYba && items.Bottle ||
                items.CanLiftHeavy() && (items.Flute || Logic.BootsClip && items.Boots) ||
                World.CanEnter<DarkWorldSouth>(items) && (
                    Logic.MirrorWrap && items.Mirror ||
                    Logic.BootsClip && items.Boots && (items.MoonPearl || Logic.BunnyRevive && items.CanBunnyRevive())
                );
            // Todo: access from SM
            //return items.CanAccessMiseryMirePortal(Config) ||
            //    Logic switch {
            //        Nmg => items.Flute,
            //        Owg => items.Flute || items.Boots,
            //        _ => items.Flute || items.Boots || items.Bottle,
            //    } && items.CanLiftHeavy() ||
            //    Logic >= Mg && items.Bottle && World.CanEnter("Light World Death Mountain West", items) ||
            //    Logic switch {
            //        Owg => items.MoonPearl,
            //        Mg => items.CanBeLinkInDarkWorldWithMG(),
            //        _ => false,
            //    } && items.Boots && World.CanEnter("Dark World South", items);
        }

    }

}
