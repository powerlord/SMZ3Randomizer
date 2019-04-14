namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class DesertPalace : Zelda.DesertPalace {

        public DesertPalace(World world, Config config) : base(world, config) { }

        public override bool CanEnter(Progression items) {
            return items.MoonPearl && items.Book && World.CanEnter("Light World South", items);
        }

    }

}
