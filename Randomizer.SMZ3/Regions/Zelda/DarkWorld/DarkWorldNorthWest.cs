using System.Collections.Generic;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class DarkWorldNorthWest : Z3Region {

        public override string Name => "Dark World North West";
        public override string Area => "Dark World";

        public DarkWorldNorthWest(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+71, 0x180146, LocationType.Regular, "Bumper Cave",
                    items =>
                        Logic.OneFrameClipOw || (
                            items.MoonPearl ||
                            Logic.OwYba && items.Bottle ||
                            Logic.BunnyRevive && items.CanBunnyRevive()
                        ) && (
                            Logic.BootsClip && items.Boots ||
                            items.CanLiftLight() && items.Cape
                        )),
                new Location(this, 256+72, 0xEDA8, LocationType.Regular, "Chest Game",
                    items =>
                        items.MoonPearl ||
                        Logic.SuperBunny ||
                        Logic.OwYba && items.Bottle ||
                        Logic.BunnyRevive && items.CanBunnyRevive()),
                new Location(this, 256+73, 0xE9EF, LocationType.Regular, "C-Shaped House",
                    items =>
                        items.MoonPearl ||
                        Logic.SuperBunny ||
                        Logic.OwYba && items.Bottle ||
                        Logic.BunnyRevive && items.CanBunnyRevive()),
                new Location(this, 256+74, 0xE9EC, LocationType.Regular, "Brewery",
                    items =>
                        items.MoonPearl ||
                        Logic.OwYba && items.Bottle ||
                        Logic.BunnyRevive && items.CanBunnyRevive()),
                new Location(this, 256+75, 0x180006, LocationType.Regular, "Hammer Pegs",
                    items => items.Hammer && (
                        items.MoonPearl ||
                        Logic.OwYba && items.Bottle ||
                        Logic.BunnyRevive && items.CanBunnyRevive()
                    ) && (
                        items.CanLiftHeavy() ||
                        Logic.MirrorWrap && items.Mirror ||
                        (Logic.FakeFlipper || items.Flippers) && (
                            Logic.OneFrameClipOw ||
                            Logic.BootsClip && items.Boots ||
                            Logic.SuperSpeed && items.CanSpinSpeed()
                        )
                    )),
                new Location(this, 256+76, 0x18002A, LocationType.Regular, "Blacksmith",
                    items =>
                        Logic.OwYba && items.Bottle && (
                            Logic.OneFrameClipOw ||
                            Logic.BootsClip && items.Boots && (items.MoonPearl /*|| items.Bottles >= 2*/)
                        ) || (
                            items.MoonPearl ||
                            Logic.OwYba && items.Bottle ||
                            Logic.BunnyRevive && items.CanBunnyRevive()
                        ) && items.CanLiftHeavy()),
                new Location(this, 256+77, 0x33D68, LocationType.Regular, "Purple Chest",
                    items => Locations.Get("Blacksmith").Available(items) && (
                        Logic.MirrorWrap && items.Mirror || (
                            items.MoonPearl ||
                            Logic.OwYba && items.Bottle ||
                            Logic.BunnyRevive && items.CanBunnyRevive()
                        ) && (
                            items.CanLiftHeavy() ||
                            (Logic.FakeFlipper || items.Flippers) && (
                                Logic.OneFrameClipOw ||
                                Logic.BootsClip && items.Boots ||
                                Logic.SuperSpeed && items.CanSpinSpeed()
                            )
                        )
                    )),
            };
        }

        public override bool CanEnter(Progression items) {
            return
                Logic.OneFrameClipOw ||
                Logic.OwYba && items.Bottle ||
                items.Mirror && World.CanEnter<LightWorldDeathMountainWest>(items) && (
                    Logic.MirrorClip ||
                    Logic.MirrorWrap && (
                        Logic.BootsClip && items.Boots ||
                        Logic.SuperSpeed && items.CanSpinSpeed()
                    )
                ) ||
                Logic.MirrorWrap && Logic.BunnySurf && items.Mirror && items.Flippers && World.CanAquire(items, Agahnim) ||
                items.MoonPearl && (
                    World.CanEnter<DarkWorldNorthEast>(items) && (
                        items.CanLiftLight() || items.Hammer || items.Flippers ||
                        Logic.BunnyRevive && items.CanBunnyRevive()
                    ) && (
                        items.Hookshot ||
                        Logic.MirrorWrap && items.Mirror
                    ) ||
                    items.Hammer && items.CanLiftLight() ||
                    items.CanLiftHeavy() ||
                    Logic.BootsClip && items.Boots ||
                    Logic.SuperSpeed && items.CanSpinSpeed()
                );
            // Todo: access from SM and unravel aga
            //return items.MoonPearl && ((
            //        World.CanAquire(items, Agahnim) ||
            //        items.CanAccessDarkWorldPortal(Config) && items.Flippers
            //    ) && items.Hookshot && (items.Flippers || items.CanLiftLight() || items.Hammer) ||
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
