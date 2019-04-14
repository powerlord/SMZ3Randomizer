using System.Linq;
using System.Collections.Generic;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class TurtleRock : Z3Region, IReward, IMedallionAccess {

        public override string Name => "Turtle Rock";
        public override string Area => "Turtle Rock";

        public RewardType Reward { get; set; } = RewardType.None;
        public ItemType Medallion { get; set; }

        public TurtleRock(World world, Config config) : base(world, config) {
            RegionItems = new[] { KeyTR, BigKeyTR, MapTR, CompassTR };

            Locations = new List<Location> {
                new Location(this, 256+177, 0xEA22, LocationType.Regular, "Turtle Rock - Compass Chest",
                    items => items.Somaria && ReachLobbyTransit(items, Location(
                        "Turtle Rock - Roller Room - Left",
                        "Turtle Rock - Roller Room - Right"
                    ))),
                new Location(this, 256+178, 0xEA1C, LocationType.Regular, "Turtle Rock - Roller Room - Left",
                    items => items.Somaria && items.Firerod && ReachLobbyTransit(items, Location(
                        "Turtle Rock - Roller Room - Right",
                        "Turtle Rock - Compass Chest"
                    ))),
                new Location(this, 256+179, 0xEA1F, LocationType.Regular, "Turtle Rock - Roller Room - Right",
                    items => items.Somaria && items.Firerod && ReachLobbyTransit(items, Location(
                        "Turtle Rock - Roller Room - Left",
                        "Turtle Rock - Compass Chest"
                    ))),
                new Location(this, 256+180, 0xEA16, LocationType.Regular, "Turtle Rock - Chain Chomps",
                    items => EnterTop(items) && items.KeyTR >= 1 ||
                        EnterMiddle(items) ||
                        EnterLower(items) && items.Lamp && items.Somaria),
                new Location(this, 256+181, 0xEA25, LocationType.Regular, "Turtle Rock - Big Key Chest",
                    items => items.KeyTR >=
                        (!Config.Keysanity || Location("Turtle Rock - Big Key Chest").ItemIs(BigKeyTR, World) ? 2 :
                            Location("Turtle Rock - Big Key Chest").ItemIs(KeyTR, World) ? 3 : 4))
                    .AlwaysAllow((item, items) => item.Is(KeyTR, World) && items.KeyTR >= 3),
                new Location(this, 256+182, 0xEA19, LocationType.Regular, "Turtle Rock - Big Chest",
                    items => items.BigKeyTR && (
                        EnterTop(items) && items.KeyTR >= 2 ||
                        EnterMiddle(items) && (items.Hookshot || items.Somaria) ||
                        EnterLower(items) && items.Lamp && items.Somaria
                    )),
                new Location(this, 256+183, 0xEA34, LocationType.Regular, "Turtle Rock - Crystaroller Room",
                    items => items.BigKeyTR && (
                        EnterTop(items) && items.KeyTR >= 2 ||
                        EnterMiddle(items)
                    ) ||
                    EnterLower(items) && items.Lamp && items.Somaria),
                new Location(this, 256+184, 0xEA28, LocationType.Regular, "Turtle Rock - Eye Bridge - Top Right", LaserBridge),
                new Location(this, 256+185, 0xEA2B, LocationType.Regular, "Turtle Rock - Eye Bridge - Top Left", LaserBridge),
                new Location(this, 256+186, 0xEA2E, LocationType.Regular, "Turtle Rock - Eye Bridge - Bottom Right", LaserBridge),
                new Location(this, 256+187, 0xEA31, LocationType.Regular, "Turtle Rock - Eye Bridge - Bottom Left", LaserBridge),
                new Location(this, 256+188, 0x180159, LocationType.Regular, "Turtle Rock - Trinexx",
                    items => items.BigKeyTR && items.KeyTR >= 4 && (items.Lamp || EnterLower(items)) &&
                        items.Somaria && CanBeatBoss(items)),
            };
        }

        bool ReachLobbyTransit(Progression items, IEnumerable<Location> locations) {
            return EnterTop(items) ||
                EnterMiddle(items) && items.KeyTR >= (locations.Any(l => l.ItemIs(BigKeyTR, World)) ? 2 : 4) ||
                EnterLower(items) && items.Lamp && items.KeyTR >= 4;
        }

        protected virtual bool LaserBridge(Progression items) {
            return (
                EnterLower(items) ||
                (EnterTop(items) || EnterMiddle(items)) && items.BigKeyTR && items.KeyTR >= 3 && items.Lamp && items.Somaria
            ) &&
                // Todo: Laser protection, to keep or not?
                (items.Cape || items.Byrna || items.CanBlockLasers);
        }

        protected bool CanBeatBoss(Progression items) {
            return items.Firerod && items.Icerod && (
                /*TemperedSword ||*/ items.Hammer ||
                items.CanExtendMagic(2) && items.MasterSword ||
                items.CanExtendMagic(4) && items.Sword
            );
        }

        public override bool CanEnter(Progression items) {
            return EnterTop(items) || EnterMiddle(items) || EnterLower(items);
        }

        bool EnterTop(Progression items) {
            return Medallion switch {
                    Bombos => items.Bombos,
                    Ether => items.Ether,
                    _ => items.Quake,
                } && items.Sword && (
                    items.MoonPearl ||
                    Logic.OwYba && items.Bottle && (
                        Logic.OneFrameClipOw ||
                        Logic.BootsClip && items.Boots
                    )
                ) && (
                    Logic.OneFrameClipOw ||
                    Logic.BootsClip && items.Boots ||
                    items.CanLiftHeavy() && items.Hammer && World.CanEnter<LightWorldDeathMountainEast>(items)
                ) && items.Somaria;
        }

        bool EnterMiddle(Progression items) {
            return (
                Logic.OneFrameClipOw && (
                    items.MoonPearl ||
                    Logic.DungeonRevive ||
                    Logic.OwYba && items.Bottle
                ) ||
                Logic.BootsClip && items.Boots && (
                    items.MoonPearl ||
                    Logic.OwYba && items.Bottle
                ) ||
                Logic.SuperSpeed && items.CanSpinSpeed() && items.MoonPearl ||
                Logic.MirrorClip && items.Mirror && (items.MoonPearl || Logic.DungeonRevive)
            ) &&
                // Todo: avoids damage in case of low health seeds, but do we skip cape/byrna?
                (items.Boots || items.Somaria || items.Hookshot || items.Cape || items.Byrna) &&
                World.CanEnter<DarkWorldDeathMountainEast>(items);
        }

        bool EnterLower(Progression items) {
            return
                Logic.MirrorWrap && items.Mirror && (
                    items.MoonPearl ||
                    Logic.OwYba && items.Bottle
                ) && ((
                        Logic.OneFrameClipOw ||
                        Logic.BootsClip && items.Boots
                    ) && World.CanEnter<LightWorldDeathMountainWest>(items) ||
                    Logic.SuperSpeed && items.CanSpinSpeed() && World.CanEnter<DarkWorldDeathMountainEast>(items)
                );
        }

        public bool CanComplete(Progression items) {
            return Location("Turtle Rock - Trinexx").Available(items);
        }

    }

}
