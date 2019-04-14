namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class PalaceOfDarkness : Zelda.PalaceOfDarkness {

        public PalaceOfDarkness(World world, Config config) : base(world, config) { }

        public override bool CanEnter(Progression items) {
            return World.CanEnter("Dark World North East", items);
        }

    }

}
