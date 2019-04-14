namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class TowerOfHera : Zelda.TowerOfHera {

        public TowerOfHera(World world, Config config) : base(world, config) { }

        public override bool CanEnter(Progression items) {
            return items.MoonPearl && items.Hammer && World.CanEnter("Light World Death Mountain East", items);
        }
    }

}
