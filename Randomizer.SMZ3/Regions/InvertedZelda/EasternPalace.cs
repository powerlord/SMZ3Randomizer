namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class EasternPalace : Zelda.EasternPalace {

        public EasternPalace(World world, Config config) : base(world, config) {
            /* Need a sword in super bunny state */
            Location("Eastern Palace - Compass Chest").CanAccess(items =>
                CanBeLinkInLightWorldDungeon(items) || items.Sword);
            Location("Eastern Palace - Big Chest").CanAccess(items =>
                (CanBeLinkInLightWorldDungeon(items) || items.Sword) && items.BigKeyEP);
            Location("Eastern Palace - Big Key Chest").CanAccess(items =>
                (CanBeLinkInLightWorldDungeon(items) || items.Sword) && items.Lamp);
            Location("Eastern Palace - Armos Knights").CanAccess(items =>
                /* Can not be super bunny since bow is needed */
                // Todo: firerod, to be or not to be?
                CanBeLinkInLightWorldDungeon(items) && items.BigKeyEP && items.Bow && (items.Lamp || items.Firerod));
        }

        bool CanBeLinkInLightWorldDungeon(Progression items) {
            return
                items.MoonPearl ||
                Logic.DungeonRevive ||
                Logic.BunnyRevive && items.CanBunnyRevive() ||
                Logic.OwYba && items.Bottle;
        }

        public override bool CanEnter(Progression items) {
            return (
                CanBeLinkInLightWorldDungeon(items) ||
                Logic.SuperBunny && items.Mirror
            ) &&
                World.CanEnter<LightWorldNorthEast>(items);
        }

    }

}
