namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class HyruleCastle : Zelda.HyruleCastle {

        public HyruleCastle(World world, Config config) : base(world, config) { }

        public override bool CanEnter(Progression items) {
            return items.MoonPearl && World.CanEnter("Light World North East", items);
        }

    }

}
