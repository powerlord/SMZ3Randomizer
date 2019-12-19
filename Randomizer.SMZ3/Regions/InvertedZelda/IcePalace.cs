namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class IcePalace : Zelda.IcePalace {

        public IcePalace(World world, Config config) : base(world, config) { }

        public override bool CanEnter(Progression items) {
            // Todo: missing LW + Mirror
            return (
                Logic.OneFrameClipUw ||
                items.CanMeltIceEnemies()
            ) && (
                items.Flippers ||
                Logic.OneFrameClipOw ||
                Logic.BootsClip && items.Boots ||
                Logic.SuperSpeed && items.CanSpinSpeed() ||
                Logic.FakeFlipper && (
                    Logic.BunnyRevive ||
                    items.Flute && World.CanEnter<LightWorldNorthWest>(items) || // Todo: glitch flute activation
                    World.CanEnter<DarkWorldNorthWest>(items)
                )
            );
        }

    }

}
