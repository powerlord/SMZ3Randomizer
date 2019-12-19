namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class SkullWoods : Zelda.SkullWoods {

        public SkullWoods(World world, Config config) : base(world, config) {
            Location("Skull Woods - Bridge Room").CanAccess(items =>
                items.Firerod);
            Location("Skull Woods - Mothula").CanAccess(items =>
                items.Firerod && items.Sword && items.KeySW >= 3);
        }

        public override bool CanEnter(Progression items) {
            return World.CanEnter<DarkWorldNorthWest>(items);
        }

    }

}
