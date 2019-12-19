using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class MiseryMire : Zelda.MiseryMire {

        public MiseryMire(World world, Config config) : base(world, config) { }

        /* Need "CanKillManyEnemies" if implementing swordless */
        public override bool CanEnter(Progression items) {
            return Medallion switch {
                    Bombos => items.Bombos,
                    Ether => items.Ether,
                    _ => items.Quake
                } && items.Sword &&
                    (items.Boots || items.Hookshot) &&
                    /*items.CanKillManyEnemies() &&*/
                    World.CanEnter<DarkWorldMire>(items);
        }

    }

}
