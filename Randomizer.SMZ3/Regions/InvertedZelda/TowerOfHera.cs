namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class TowerOfHera : Zelda.TowerOfHera {

        public TowerOfHera(World world, Config config) : base(world, config) {
            Location("Tower of Hera - Big Key Chest").CanAccess(items =>
                items.CanLightTorches() && (
                    items.KeyTH && (
                        items.MoonPearl ||
                        Logic.OwYba && items.Bottle ||
                        Logic.BunnyRevive && items.CanBunnyRevive()
                    ) ||
                    EnterFromMire(items)
                ));
            Location("Tower of Hera - Compass Chest").CanAccess(items =>
                EnterFromTower(items) && items.BigKeyTH || EnterFromMire(items));
            Location("Tower of Hera - Big Chest").CanAccess(items =>
                EnterFromTower(items) && items.BigKeyTH || EnterFromMire(items) && (items.BigKeyTH || items.BigKeyMM));
            Location("Tower of Hera - Moldorm").CanAccess(items =>
                EnterFromTower(items) && (items.BigKeyTH || EnterFromMire(items) && items.BigKeyMM) && CanBeatBoss(items));
        }

        protected override bool EnterFromTower(Progression items) {
            return
                World.CanEnter<LightWorldDeathMountainEast>(items) && items.MoonPearl && items.Hammer ||
                World.CanEnter<LightWorldDeathMountainWest>(items) && (
                    Logic.OneFrameClipOw && Logic.SuperBunny && items.Sword && items.Mirror || (
                        items.MoonPearl ||
                        Logic.OwYba && items.TwoBottles || (
                            Logic.OwYba && items.Bottle ||
                            Logic.BunnyRevive && items.CanBunnyRevive()
                        ) && (
                            Logic.OneFrameClipOw ||
                            Logic.BootsClip && items.Boots
                        )
                    ) && (
                        Logic.OneFrameClipOw ||
                        Logic.BootsClip && items.Boots ||
                        Logic.SuperSpeed && items.CanSpinSpeed()
                    )
                );
        }

    }

}
