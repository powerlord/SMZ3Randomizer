using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class MaridiaOuter : SMRegion {

        public override string Name => "Maridia Outer";
        public override string Area => "Maridia";

        public MaridiaOuter(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 136, 0xC7C437, LocationType.Visible, "Missile (green Maridia shinespark)",
                    items => items.Gravity && items.SpeedBooster),
                new Location(this, 137, 0xC7C43D, LocationType.Visible, "Super Missile (green Maridia)"),
                new Location(this, 138, 0xC7C47D, LocationType.Visible, "Energy Tank, Mama turtle",
                    // Todo: speedbooster here, how, why?
                    // Todo: Test grapple's reach without Hi-Jump boots
                    items => items.CanOpenRedDoors() && (items.CanFly() || /*items.HiJump && */items.Grapple) ||
                        Logic.SpringBallJump && items.CanSpringBallJump() && (items.Gravity || items.HiJump)),
                new Location(this, 139, 0xC7C483, LocationType.Hidden, "Missile (green Maridia tatori)",
                    items => items.CanOpenRedDoors()),
            };
        }

        public override bool CanEnter(Progression items) {
            return (Logic.Suitless || items.Gravity) && (
                World.CanEnter<NorfairUpperWest>(items) && items.CanUsePowerBombs() && (items.Gravity ||
                    Logic.Suitless && items.HiJump && (items.Ice || Logic.SpringBallJump && items.CanSpringBallJump()))
                || items.CanAccessMaridiaPortal(World)
            );
        }

    }

}
