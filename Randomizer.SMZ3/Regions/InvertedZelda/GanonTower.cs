using System.Linq;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class GanonTower : Zelda.GanonTower {

        public GanonTower(World world, Config config) : base(world, config) {
            foreach (var location in Locations.Except(Location(
                "Ganon's Tower - Hope Room - Left",
                "Ganon's Tower - Hope Room - Right"
            ))) {
                location.WrapCanAccess(access => items =>
                    access(items) && CanBeLinkInLightWorldDungeon(items)
                );
            }
        }

        bool CanBeLinkInLightWorldDungeon(Progression items) {
            return
                items.MoonPearl ||
                Logic.DungeonRevive || (
                    Logic.OneFrameClipOw ||
                    Logic.BootsClip && items.Boots
                ) && (
                    Logic.BunnyRevive && items.CanBunnyRevive() ||
                    Logic.OwYba && items.Bottle
                );
        }

        public override bool CanEnter(Progression items) {
            return (
                CanBeLinkInLightWorldDungeon(items) ||
                Logic.SuperBunny && items.Mirror
            ) &&
                World.CanAquireAll(items, CrystalBlue, CrystalRed, GoldenFourBoss) &&
                World.CanEnter<LightWorldNorthEast>(items);
        }

    }

}
