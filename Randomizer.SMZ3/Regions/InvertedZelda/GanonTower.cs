using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class GanonTower : Zelda.GanonTower {

        public GanonTower(World world, Config config) : base(world, config) { }

        public override bool CanEnter(Progression items) {
            return items.MoonPearl && World.CanEnter("Light World North East", items) &&
                World.CanAquireAll(items, CrystalBlue, CrystalRed, GoldenFourBoss);
        }

    }

}
