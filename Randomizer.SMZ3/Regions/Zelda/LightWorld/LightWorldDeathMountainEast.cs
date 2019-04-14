using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.Zelda {

    class LightWorldDeathMountainEast : Z3Region {

        public override string Name => "Light World Death Mountain East";
        public override string Area => "Light World";

        public LightWorldDeathMountainEast(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+4, 0x180141, LocationType.Regular, "Floating Island",
                    items =>
                        Logic.OneFrameClipOw ||
                        Logic.OwYba && items.Bottle ||
                        Logic.BootsClip && items.Boots ||
                        items.Mirror && (Logic.MirrorWrap || items.MoonPearl && items.CanLiftLight()) &&
                            World.CanEnter<DarkWorldDeathMountainEast>(items)),
                new Location(this, 256+5, 0xE9BF, LocationType.Regular, "Spiral Cave"),
                new Location(this, 256+6, 0xEB39, LocationType.Regular, "Paradox Cave Upper - Left"),
                new Location(this, 256+7, 0xEB3C, LocationType.Regular, "Paradox Cave Upper - Right"),
                new Location(this, 256+8, 0xEB2A, LocationType.Regular, "Paradox Cave Lower - Far Left"),
                new Location(this, 256+9, 0xEB2D, LocationType.Regular, "Paradox Cave Lower - Left"),
                new Location(this, 256+10, 0xEB36, LocationType.Regular, "Paradox Cave Lower - Middle"),
                new Location(this, 256+11, 0xEB30, LocationType.Regular, "Paradox Cave Lower - Right"),
                new Location(this, 256+12, 0xEB33, LocationType.Regular, "Paradox Cave Lower - Far Right"),
                new Location(this, 256+13, 0xE9C5, LocationType.Regular, "Mimic Cave",
                    items => items.Mirror && items.Hammer && (
                        Logic.OneFrameClipOw ||
                        Logic.MirrorClip ||
                        Logic.BootsClip && items.Boots && (items.MoonPearl || Logic.OwYba && items.Bottle) ||
                        Logic.SuperSpeed && items.CanSpinSpeed() && items.MoonPearl &&
                            World.CanEnter<DarkWorldDeathMountainEast>(items) ||
                        items.KeyTR >= 2 && World.CanEnter<TurtleRock>(items)
                    )),
            };
        }

        public override bool CanEnter(Progression items) {
            return
                Logic.OneFrameClipOw ||
                Logic.BootsClip && items.Boots ||
                Logic.SuperSpeed && items.CanSpinSpeed() ||
                (items.Hookshot || (Logic.MirrorClip || Logic.MirrorWrap) && items.Mirror) &&
                    World.CanEnter<LightWorldDeathMountainWest>(items) ||
                items.Hammer && World.CanEnter<TowerOfHera>(items);
        }

    }

}
