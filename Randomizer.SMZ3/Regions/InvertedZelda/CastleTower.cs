namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class CastleTower : Zelda.GanonTower {

        public CastleTower(World world, Config config) : base(world, config) { }

        public override bool CanEnter(Progression items) {
            return items.CanKillManyEnemies() && World.CanEnter<DarkWorldDeathMountainWest>(items);
        }

    }

}
