using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class SwampPalace : Z3Region, IReward {

        public override string Name => "Swamp Palace";
        public override string Area => "Swamp Palace";

        public RewardType Reward { get; set; } = RewardType.None;

        public SwampPalace(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeySP, BigKeySP, MapSP, CompassSP };

            Locations = new List<Location> {
                new Location(this, 256+135, 0xEA9D, LocationType.Regular, "Swamp Palace - Entrance")
                    .Allow((item, items) => Config.Keysanity || item.Is(KeySP, World) || EnterFromMire(items)),
                new Location(this, 256+136, 0xE986, LocationType.Regular, "Swamp Palace - Map Chest",
                    items => items.KeySP || EnterFromMire(items)),
                new Location(this, 256+137, 0xE989, LocationType.Regular, "Swamp Palace - Big Chest",
                    items => ReachCenterWestWing(items) && (
                        items.BigKeySP ||
                        EnterFromMire(items) && items.BigKeyMM ||
                        EnterFromHera(items) && items.BigKeyTH
                    ))
                    .AlwaysAllow((item, items) => item.Is(BigKeySP, World)),
                new Location(this, 256+138, 0xEAA0, LocationType.Regular, "Swamp Palace - Compass Chest", ReachCenterWestWing),
                new Location(this, 256+139, 0xEAA3, LocationType.Regular, "Swamp Palace - West Chest", ReachCenterWestWing),
                new Location(this, 256+140, 0xEAA6, LocationType.Regular, "Swamp Palace - Big Key Chest", ReachCenterWestWing),
                new Location(this, 256+141, 0xEAA9, LocationType.Regular, "Swamp Palace - Flooded Room - Left", ReachNorthWing),
                new Location(this, 256+142, 0xEAAC, LocationType.Regular, "Swamp Palace - Flooded Room - Right", ReachNorthWing),
                new Location(this, 256+143, 0xEAAF, LocationType.Regular, "Swamp Palace - Waterfall Room", ReachNorthWing),
                new Location(this, 256+144, 0x180154, LocationType.Regular, "Swamp Palace - Arrghus",
                    items => ReachNorthWing(items) && CanBeatBoss(items)),
            };
        }

        bool ReachNorthWing(Progression items) {
            return items.Hookshot && ReachCenterWestWing(items);
        }

        bool ReachCenterWestWing(Progression items) {
            return (items.KeySP || EnterFromMire(items)) &&
                (items.Hammer || EnterFromMire(items) || EnterFromHera(items));
        }

        bool CanBeatBoss(Progression items) {
            return items.Hookshot && (items.Hammer || items.Sword ||
                (items.CanExtendMagic() || items.Bow) &&
                (items.Firerod || items.Icerod));
        }

        public override bool CanEnter(Progression items) {
            return items.Flippers && World.CanEnter<DarkWorldSouth>(items) && (
                items.MoonPearl && items.Mirror ||
                Logic.OneFrameClipUw &&
                    EnterFromMire(items) && (items.BigKeyMM || items.BigKeyTH) &&
                    World.LocationIn<LightWorldDeathMountainWest>("Old Man").Available(items) && (
                        Logic.OneFrameClipOw ||
                        Logic.BootsClip && items.Boots ||
                        Logic.SuperSpeed && items.CanSpinSpeed()
                    )
            );
        }

        bool EnterFromMire(Progression items) => World.Region<MiseryMire>().EnterFromMire(items);

        bool EnterFromHera(Progression items) {
            return Logic.OneFrameClipUw && World.CanEnter<TowerOfHera>(items) && items.BigKeyTH;
        }

        public bool CanComplete(Progression items) {
            return Location("Swamp Palace - Arrghus").Available(items);
        }

    }

}
