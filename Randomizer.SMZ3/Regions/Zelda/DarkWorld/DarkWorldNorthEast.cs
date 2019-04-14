using System.Collections.Generic;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class DarkWorldNorthEast : Z3Region {

        public override string Name => "Dark World North East";
        public override string Area => "Dark World";

        public DarkWorldNorthEast(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+78, 0xEE185, LocationType.Regular, "Catfish",
                    items => (
                        items.MoonPearl ||
                        Logic.OwYba && items.Bottle ||
                        Logic.BunnyRevive && items.CanBunnyRevive()
                    ) && (
                        items.CanLiftLight() ||
                        Logic.OneFrameClipOw ||
                        Logic.BootsClip && items.Boots
                    )),
                new Location(this, 256+79, 0x180147, LocationType.Regular, "Pyramid"),
                new Location(this, 256+80, 0xE980, LocationType.Regular, "Pyramid Fairy - Left", PyramidFairy),
                new Location(this, 256+81, 0xE983, LocationType.Regular, "Pyramid Fairy - Right", PyramidFairy),
            };
        }

        bool PyramidFairy(Progression items) {
            return
                Logic.MirrorClip && items.Mirror && (
                    Logic.OneFrameClipOw ||
                    Logic.BootsClip && items.Boots ||
                    Logic.SuperSpeed && items.CanSpinSpeed()
                ) ||
                World.CanAquireAll(items, CrystalRed) && World.CanEnter<DarkWorldSouth>(items) && (
                    Logic.OwYba && items.Bottle ||
                    items.MoonPearl && items.Hammer ||
                    items.Mirror && World.CanAquire(items, Agahnim)
                );
        }

        public override bool CanEnter(Progression items) {
            return
                Logic.OneFrameClipOw ||
                Logic.OwYba && items.Bottle ||
                Logic.BootsClip && items.Boots && items.MoonPearl ||
                items.Mirror && World.CanEnter<LightWorldDeathMountainWest>(items) && (
                    (Logic.MirrorClip || Logic.MirrorWrap && Logic.BunnyRevive && items.CanBunnyRevive()) && (
                        Logic.BootsClip && items.Boots ||
                        Logic.SuperSpeed && items.CanSpinSpeed()
                    ) ||
                    Logic.MirrorClip && (
                        Logic.BunnyRevive && items.CanBunnyRevive() ||
                        Logic.FakeFlipper && items.MoonPearl
                    )
                ) ||
                World.CanAquire(items, Agahnim) ||
                items.MoonPearl && items.Hammer && items.CanLiftLight() ||
                items.MoonPearl && (
                    items.CanLiftHeavy() ||
                    Logic.SuperSpeed && items.CanSpinSpeed() ||
                    Logic.MirrorClip && items.Mirror
                ) && (
                    items.Hammer || items.Flippers ||
                    Logic.FakeFlipper ||
                    Logic.WaterWalk && items.Boots ||
                    Logic.MirrorWrap && items.Mirror && items.CanLiftLight() ||
                    Logic.BunnyRevive && items.CanBunnyRevive()
                );
            // Todo: Access from SM
            //return World.CanAquire(items, Agahnim) || items.MoonPearl && (
            //    items.Hammer && items.CanLiftLight() ||
            //    items.CanLiftHeavy() && items.Flippers ||
            //    items.CanAccessDarkWorldPortal(Config) && items.Flippers
            //) || Logic >= Owg && (
            //    Logic >= Mg && items.Bottle ||
            //    items.Mirror && items.CanSpinSpeed() ||
            //    items.MoonPearl && (items.Mirror || items.Boots)
            //) && World.CanEnter("Light World Death Mountain West", items);
        }

    }

}
