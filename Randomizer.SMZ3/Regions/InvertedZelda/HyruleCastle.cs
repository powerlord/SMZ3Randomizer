namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class HyruleCastle : Zelda.HyruleCastle {

        public HyruleCastle(World world, Config config) : base(world, config) {
            Location("Sanctuary").CanAccess(items => (
                    items.MoonPearl ||
                    Logic.BunnyRevive && items.CanBunnyRevive() ||
                    Logic.OwYba && items.Bottle ||
                    Logic.SuperBunny && items.Mirror
                ) &&
                    World.CanEnter<LightWorldNorthWest>(items) ||
                items.KeyHC && items.Lamp);
            Location("Sewers - Secret Room - Left").CanAccess(CanReachSecretRoom);
            Location("Sewers - Secret Room - Middle").CanAccess(CanReachSecretRoom);
            Location("Sewers - Secret Room - Right").CanAccess(CanReachSecretRoom);
            Location("Hyrule Castle - Boomerang Chest").CanAccess(CanBypassFrontSecurity);
            Location("Hyrule Castle - Zelda's Cell").CanAccess(CanBypassFrontSecurity);
            Location("Link's Uncle").CanAccess(items => CanReachSecretPassage(items));
            Location("Secret Passage").CanAccess(items => CanReachSecretPassage(items, superBunny: true));

            foreach (var location in Locations) {
                location.Weighted(null);
            }
        }

        bool CanReachSecretRoom(Progression items) {
            return
                items.CanLiftLight() && (
                    items.MoonPearl ||
                    Logic.BunnyRevive && items.CanBunnyRevive() ||
                    Logic.OwYba && items.Bottle
                ) ||
                items.Lamp && items.KeyHC && (
                    items.MoonPearl ||
                    Logic.DungeonRevive ||
                    Logic.BunnyRevive && items.CanBunnyRevive() ||
                    Logic.OwYba && items.Bottle ||
                    items.Sword
                );
        }

        bool CanBypassFrontSecurity(Progression items) {
            return (
                items.MoonPearl ||
                Logic.DungeonRevive ||
                Logic.BunnyRevive && items.CanBunnyRevive() ||
                Logic.OwYba && items.Bottle ||
                items.Sword
            ) && items.KeyHC;
        }

        bool CanReachSecretPassage(Progression items, bool superBunny = false) {
            return (
                items.MoonPearl ||
                Logic.OwYba && items.Bottle ||
                Logic.BunnyRevive && items.CanBunnyRevive() ||
                Logic.MirrorClip && (!superBunny || Logic.SuperBunny) && items.Mirror && (
                    Logic.OneFrameClipOw ||
                    Logic.BootsClip && items.Boots ||
                    Logic.SuperSpeed && items.CanSpinSpeed()
                ) &&
                    World.CanEnter<LightWorldDeathMountainWest>(items)
            ) &&
                World.CanEnter<LightWorldNorthEast>(items);
        }

        public override bool CanEnter(Progression items) {
            return (
                items.MoonPearl ||
                Logic.DungeonRevive ||
                Logic.BunnyRevive && items.CanBunnyRevive() ||
                Logic.OwYba && items.Bottle ||
                Logic.SuperBunny && items.Mirror
            ) &&
                World.CanEnter<LightWorldNorthEast>(items);
        }

    }

}
