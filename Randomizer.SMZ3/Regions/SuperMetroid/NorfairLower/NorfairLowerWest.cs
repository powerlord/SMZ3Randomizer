using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class NorfairLowerWest : SMRegion {

        public override string Name => "Norfair Lower West";
        public override string Area => "Norfair Lower";

        public NorfairLowerWest(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 70, 0xC78E6E, LocationType.Visible, "Missile (Gold Torizo)", Logic switch {
                    Casual => items => items.CanUsePowerBombs() && items.SpaceJump && items.Super,
                    _ => items => items.CanUsePowerBombs() && items.SpaceJump && items.Varia && (
                        items.HiJump || items.Gravity ||
                        items.CanAccessNorfairLowerPortal() && (items.CanFly() || items.CanSpringBallJump() || items.SpeedBooster) && items.Super)
                }),
                new Location(this, 71, 0xC78E74, LocationType.Hidden, "Super Missile (Gold Torizo)", Logic switch {
                    Casual => items => items.CanDestroyBombWalls() && (items.Super || items.Charge) &&
                        (items.CanAccessNorfairLowerPortal() || items.SpaceJump && items.CanUsePowerBombs()),
                    _ => items => items.CanDestroyBombWalls() && items.Varia && (items.Super || items.Charge)
                }),
                new Location(this, 79, 0xC79110, LocationType.Chozo, "Screw Attack", Logic switch {
                    Casual => items => items.CanDestroyBombWalls() && (items.SpaceJump && items.CanUsePowerBombs() || items.CanAccessNorfairLowerPortal()),
                    _ => items => items.CanDestroyBombWalls() && (items.Varia || items.CanAccessNorfairLowerPortal())
                }),
            };
        }

        public override bool CanEnter(Progression items) {
            return Logic switch {
                Casual =>
                    items.Varia && (
                        World.CanEnter<NorfairUpperEast>(items) && items.CanUsePowerBombs() && items.SpaceJump && items.Gravity ||
                        items.CanAccessNorfairLowerPortal() && items.CanDestroyBombWalls()),
                _ =>
                    World.CanEnter<NorfairUpperEast>(items) && items.CanUsePowerBombs() && items.Varia && (items.HiJump || items.Gravity) ||
                    items.CanAccessNorfairLowerPortal() && items.CanDestroyBombWalls()
            };
        }

    }

}
