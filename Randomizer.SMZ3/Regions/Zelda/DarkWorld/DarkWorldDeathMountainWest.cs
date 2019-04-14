using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.Zelda {

    class DarkWorldDeathMountainWest : Z3Region {

        public override string Name => "Dark World Death Mountain West";
        public override string Area => "Dark World";

        public DarkWorldDeathMountainWest(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+64, 0xEA8B, LocationType.Regular, "Spike Cave",
                    items => (
                        items.MoonPearl ||
                        Logic.OwYba && items.Bottle && (
                            Logic.OneFrameClipOw ||
                            Logic.BootsClip && items.Boots
                        ) &&
                        (items.CanExtendMagic(3) && items.Cape || items.Byrna)
                    ) &&
                        items.CanLiftLight() && items.Hammer &&
                        (items.CanExtendMagic() && items.Cape || items.Byrna)),
            };
        }

        public override bool CanEnter(Progression items) {
            return World.CanEnter<LightWorldDeathMountainWest>(items);
        }

    }

}
