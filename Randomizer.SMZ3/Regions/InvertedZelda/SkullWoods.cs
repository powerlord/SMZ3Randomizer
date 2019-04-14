namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class SkullWoods : Zelda.SkullWoods {

        public SkullWoods(World world, Config config) : base(world, config) { }

        public override bool CanEnter(Progression items) {
            return World.CanEnter("Dark World North West", items);
        }

    }

}
