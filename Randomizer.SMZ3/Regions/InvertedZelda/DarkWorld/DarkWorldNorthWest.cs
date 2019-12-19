namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class DarkWorldNorthWest : Zelda.DarkWorldNorthWest {

        public DarkWorldNorthWest(World world, Config config) : base(world, config) {
            Location("Chest Game").CanAccess();
            Location("C-Shaped House").CanAccess();
            Location("Brewery").CanAccess();

            Location("Bumper Cave").CanAccess(items =>
                Logic.OneFrameClipOw ||
                Logic.BootsClip && items.Boots ||
                items.MoonPearl && items.CanLiftLight() && items.Cape && items.Mirror &&
                    World.CanEnter<LightWorldNorthWest>(items));
            Location("Hammer Pegs").CanAccess(items =>
                items.Hammer && (
                    items.CanLiftHeavy() ||
                    items.Mirror && World.CanEnter<LightWorldNorthWest>(items) ||
                    Logic.BootsClip && items.Boots ||
                    Logic.FakeFlipper ||
                    items.Flippers && (
                        Logic.OneFrameClipOw ||
                        Logic.SuperSpeed && items.CanSpinSpeed()
                    )
                ));
            Location("Blacksmith").CanAccess(items => (
                    items.CanLiftHeavy() ||
                    items.Mirror ||
                    Logic.OwYba && items.Bottle && (
                        Logic.OneFrameClipOw ||
                        Logic.BootsClip && items.Boots
                    )
                ) &&
                    World.CanEnter<LightWorldNorthWest>(items));
            Location("Purple Chest").CanAccess(items => (
                    items.CanLiftHeavy() ||
                    items.Mirror ||
                    Logic.OwYba && items.Bottle && (Logic.FakeFlipper || items.Flippers) && (
                        Logic.OneFrameClipOw ||
                        Logic.BootsClip && items.Boots
                    ) &&
                        World.CanEnter<LightWorldNorthWest>(items)
                ) &&
                    World.CanEnter<LightWorldSouth>(items));
        }

        public override bool CanEnter(Progression items) {
            return true;
        }

    }

}
