namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class SwampPalace : Zelda.SwampPalace {

        public SwampPalace(World world, Config config) : base(world, config) { }

        public override bool CanEnter(Progression items) {
            return items.MoonPearl && items.Mirror && items.Flippers && World.CanEnter("Light World South", items);
        }

    }

}
