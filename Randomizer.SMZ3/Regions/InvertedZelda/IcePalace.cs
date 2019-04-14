namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class IcePalace : Zelda.IcePalace {

        public IcePalace(World world, Config config) : base(world, config) { }

        public override bool CanEnter(Progression items) {
            return items.CanMeltFreezors() && items.Flippers;
        }

    }

}
