namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class EasternPalace : Zelda.EasternPalace {

        public EasternPalace(World world, Config config) : base(world, config) { }

        public override bool CanEnter(Progression items) {
            return items.MoonPearl && World.CanEnter("Light World North East", items);
        }

    }

}
