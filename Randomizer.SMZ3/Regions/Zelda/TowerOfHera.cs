using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class TowerOfHera : Z3Region, IReward {

        public override string Name => "Tower of Hera";
        public override string Area => "Tower of Hera";

        public RewardType Reward { get; set; } = RewardType.None;

        public TowerOfHera(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeyTH, BigKeyTH, MapTH, CompassTH };

            Locations = new List<Location> {
                new Location(this, 256+115, 0x180162, LocationType.HeraStandingKey, "Tower of Hera - Basement Cage"),
                new Location(this, 256+116, 0xE9AD, LocationType.Regular, "Tower of Hera - Map Chest"),
                new Location(this, 256+117, 0xE9E6, LocationType.Regular, "Tower of Hera - Big Key Chest",
                    items => items.KeyTH && items.CanLightTorches())
                    .AlwaysAllow((item, items) => item.Type == KeyTH),
                new Location(this, 256+118, 0xE9FB, LocationType.Regular, "Tower of Hera - Compass Chest",
                    items => EnterFromTower(items) && items.BigKeyTH || EnterFromMire(items)),
                new Location(this, 256+119, 0xE9F8, LocationType.Regular, "Tower of Hera - Big Chest",
                    items =>
                        EnterFromTower(items) && items.BigKeyTH ||
                        EnterFromMire(items) && (items.BigKeyTH || items.BigKeyMM)),
                new Location(this, 256+120, 0x180152, LocationType.Regular, "Tower of Hera - Moldorm",
                    items => (
                        EnterFromTower(items) && items.BigKeyTH ||
                        EnterFromMire(items) && items.BigKeyMM
                    ) && CanBeatBoss(items)),
            };
        }

        protected bool CanBeatBoss(Progression items) {
            return items.Sword || items.Hammer;
        }

        public override bool CanEnter(Progression items) {
            return EnterFromTower(items) || EnterFromMire(items);
        }

        bool EnterFromTower(Progression items) {
            return Logic.OneFrameClipOw ||
                Logic.BootsClip && items.Boots ||
                Logic.SuperSpeed && items.CanSpinSpeed() ||
                (items.Mirror || items.Hookshot && items.Hammer) && World.CanEnter<LightWorldDeathMountainWest>(items);
        }

        bool EnterFromMire(Progression items) => World.Region<MiseryMire>().EnterFromMire(items);

        public virtual bool CanComplete(Progression items) {
            return Location("Tower of Hera - Moldorm").Available(items);
        }

    }

}
