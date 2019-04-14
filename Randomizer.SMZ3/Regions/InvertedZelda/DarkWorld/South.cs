namespace Randomizer.SMZ3.Regions.InvertedZelda.DarkWorld {

    class South : Zelda.DarkWorld.South {

        public South(World world, Config config) : base(world, config) { }

        public override bool CanEnter(Progression items) {
            return true;
        }

    }

}
