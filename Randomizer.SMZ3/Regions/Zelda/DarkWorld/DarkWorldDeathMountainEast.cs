using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.Zelda {

    class DarkWorldDeathMountainEast : Z3Region {

        public override string Name => "Dark World Death Mountain East";
        public override string Area => "Dark World";

        public DarkWorldDeathMountainEast(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+65, 0xEB51, LocationType.Regular, "Hookshot Cave - Top Right",
                    items => CanReachHookshotCave(items) && items.Hookshot),
                new Location(this, 256+66, 0xEB54, LocationType.Regular, "Hookshot Cave - Top Left",
                    items => CanReachHookshotCave(items) && items.Hookshot),
                new Location(this, 256+67, 0xEB57, LocationType.Regular, "Hookshot Cave - Bottom Left",
                    items => CanReachHookshotCave(items) && items.Hookshot),
                new Location(this, 256+68, 0xEB5A, LocationType.Regular, "Hookshot Cave - Bottom Right",
                    items => CanReachHookshotCave(items) && (items.Hookshot || items.Boots)),
                new Location(this, 256+69, 0xEA7C, LocationType.Regular, "Superbunny Cave - Top",
                    items => items.MoonPearl || Logic.SuperBunny ||
                        Logic.OwYba && items.Bottle && (
                            Logic.OneFrameClipOw ||
                            Logic.BootsClip && items.Boots
                        )),
                new Location(this, 256+70, 0xEA7F, LocationType.Regular, "Superbunny Cave - Bottom",
                    items => items.MoonPearl || Logic.SuperBunny ||
                        Logic.OwYba && items.Bottle && (
                            Logic.OneFrameClipOw ||
                            Logic.BootsClip && items.Boots
                        )),
            };
        }

        bool CanReachHookshotCave(Progression items) {
            return (
                items.MoonPearl ||
                Logic.OwYba && items.Bottle
            ) && (
                items.CanLiftLight() ||
                Logic.OneFrameClipOw ||
                Logic.BootsClip && items.Boots
            );
        }

        public override bool CanEnter(Progression items) {
            return Logic.OneFrameClipOw ||
                Logic.BootsClip && items.Boots && (
                    items.MoonPearl || items.Hammer ||
                    Logic.OwYba && items.Bottle
                ) ||
                (Logic.MirrorClip || Logic.MirrorWrap) && items.Mirror && World.CanEnter<LightWorldDeathMountainWest>(items) ||
                items.CanLiftHeavy() && World.CanEnter<LightWorldDeathMountainEast>(items);
        }

    }

}
