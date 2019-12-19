namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class DesertPalace : Zelda.DesertPalace {

        public DesertPalace(World world, Config config) : base(world, config) {
            Location("Desert Palace - Big Key Chest").CanAccess(items => (
                    items.MoonPearl ||
                    Logic.DungeonRevive ||
                    Logic.BunnyRevive && items.CanBunnyRevive() ||
                    Logic.OwYba && items.Bottle ||
                    items.Sword
                ) && items.KeyDP);
            Location("Desert Palace - Lanmolas").CanAccess(items => (
                    Logic.OneFrameClipOw && Logic.DungeonRevive ||
                    items.MoonPearl ||
                    Logic.BunnyRevive && items.CanBunnyRevive() ||
                    Logic.OwYba && items.Bottle
                ) && (
                    items.CanLiftLight() ||
                    Logic.OneFrameClipOw ||
                    Logic.BootsClip && items.Boots
                ) &&
                    items.KeyDP && items.BigKeyDP && items.CanLightTorches() && CanBeatBoss(items));
        }

        public override bool CanEnter(Progression items) {
            return (
                items.MoonPearl ||
                Logic.DungeonRevive ||
                Logic.BunnyRevive && items.CanBunnyRevive() ||
                Logic.OwYba && items.Bottle ||
                Logic.SuperBunny && items.Mirror
            ) &&
                (EnterFromPlaza(items) || EnterFromLedge(items) || EnterFromThieves(items));
        }

        bool EnterFromPlaza(Progression items) {
            return items.Book && World.CanEnter<LightWorldSouth>(items);
        }

        bool EnterFromLedge(Progression items) {
            return Logic.OneFrameClipOw ||
                Logic.BootsClip && items.Boots && (
                    items.MoonPearl ||
                    Logic.OwYba && items.Bottle ||
                    Logic.BunnyRevive && items.CanBunnyRevive()
                ) &&
                    World.CanEnter<LightWorldSouth>(items);
        }

        bool EnterFromThieves(Progression items) {
            return Logic.OneFrameClipUw && World.CanEnter<ThievesTown>(items) &&
                items.BigKeyTT && items.KeyTT;
        }

    }

}
