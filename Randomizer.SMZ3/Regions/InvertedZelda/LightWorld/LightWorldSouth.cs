using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class LightWorldSouth : Zelda.LightWorldSouth {

        public LightWorldSouth(World world, Config config) : base(world, config) {
            Locations.Remove(Location("Link's House"));

            Location("Maze Race").CanAccess(items =>
                Logic.OneFrameClipOw ||
                items.MoonPearl);
            Location("Library").CanAccess(items => (
                    CanBeLinkInLightWorld(items) ||
                    Logic.SuperBunny && items.Mirror
                ) && items.Boots);
            Location("Flute Spot").CanAccess(items =>
                CanBeLinkInLightWorld(items) && items.Shovel);
            Location("South of Grove").CanAccess(items =>
                CanBeLinkInLightWorld(items) ||
                Logic.SuperBunny && items.Mirror);
            Location("Aginah's Cave").CanAccess(CanBeLinkInLightWorld);
            Location("Mini Moldorm Cave - Far Left").CanAccess(CanBeLinkInLightWorld);
            Location("Mini Moldorm Cave - Left").CanAccess(CanBeLinkInLightWorld);
            Location("Mini Moldorm Cave - NPC").CanAccess(CanBeLinkInLightWorld);
            Location("Mini Moldorm Cave - Right").CanAccess(CanBeLinkInLightWorld);
            Location("Mini Moldorm Cave - Far Right").CanAccess(CanBeLinkInLightWorld);
            Location("Desert Ledge").CanAccess(items =>
                Logic.OneFrameClipOw ||
                Logic.BootsClip && items.Boots || (
                    items.MoonPearl ||
                    Logic.DungeonRevive ||
                    Logic.OwYba && items.Bottle ||
                    Logic.SuperBunny && items.Mirror
                ) && World.CanEnter<DesertPalace>(items));
            Location("Checkerboard Cave").CanAccess(items =>
                CanBeLinkInLightWorld(items) && items.CanLiftLight());
            Location("Bombos Tablet").CanAccess(items =>
                items.Book && items.MasterSword);
            Location("Floodgate Chest").CanAccess(items =>
                CanBeLinkInLightWorld(items) ||
                Logic.SuperBunny && items.Mirror);
            Location("Sunken Treasure").CanAccess(items =>
                CanBeLinkInLightWorld(items) ||
                Logic.SuperBunny && items.Mirror);
            Location("Lake Hylia Island").CanAccess(items =>
                Logic.OneFrameClipOw ||
                CanBeLinkInLightWorld(items) && (
                    items.Flippers ||
                    Logic.BootsClip && items.Boots ||
                    Logic.SuperSpeed && items.CanSpinSpeed()
                ));
            Location("Hobo").CanAccess(items =>
                CanBeLinkInLightWorld(items) && (
                    Logic.FakeFlipper || items.Flippers ||
                    Logic.WaterWalk && items.Boots
                ));
            Location("Ice Rod Cave").CanAccess(items =>
                CanBeLinkInLightWorld(items) ||
                CanAcquireRedBomb(items) && Logic.SuperBunny && items.Mirror);

            foreach (var location in Locations) {
                location.Weighted(null);
            }
        }

        // Todo: helper method naming
        bool CanBeLinkInLightWorld(Progression items) {
            return
                items.MoonPearl ||
                Logic.OwYba && items.Bottle ||
                Logic.BunnyRevive && items.CanBunnyRevive();
        }

        public bool CanAcquireRedBomb(Progression items) {
            return (Logic.SuperBunny || items.MoonPearl) && World.CanAquireAll(items, CrystalRed);
        }

        public override bool CanEnter(Progression items) {
            return World.CanEnter<LightWorldNorthEast>(items);
        }

    }

}
