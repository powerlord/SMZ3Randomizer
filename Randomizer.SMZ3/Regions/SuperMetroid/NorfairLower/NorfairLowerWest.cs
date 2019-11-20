using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class NorfairLowerWest : SMRegion {

        public override string Name => "Norfair Lower West";
        public override string Area => "Norfair Lower";

        public NorfairLowerWest(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 70, 0xC78E6E, LocationType.Visible, "Missile (Gold Torizo)",
                    items => items.Varia && items.CanUsePowerBombs() && items.SpaceJump && (
                        World.CanEnter<NorfairUpperEast>(items) || (
                            items.CanFly() ||
                            Logic.ShortCharge && items.SpeedBooster ||
                            Logic.SpringBallGlitch && items.CanSpringBallJump()
                        ) && items.Super
                    ) && items.CanBeatGoldenTorizo(Logic)),
                new Location(this, 71, 0xC78E74, LocationType.Hidden, "Super Missile (Gold Torizo)",
                    items => (items.Varia || Logic.HellRun && items.CanHellRunWithoutCf(5)) && (
                        items.CanAccessNorfairLowerPortal() ||
                        items.SpaceJump || Logic.GreenGate && items.Super
                    ) && items.CanBeatGoldenTorizo(Logic)),
                new Location(this, 79, 0xC79110, LocationType.Chozo, "Screw Attack",
                    items => items.CanAccessNorfairLowerPortal() ||
                        items.CanUsePowerBombs() && (items.SpaceJump && items.CanBeatGoldenTorizo(Logic) || Logic.GreenGate && items.Super)),
            };
        }

        public override bool CanEnter(Progression items) {
            return items.Varia && World.CanEnter<NorfairUpperEast>(items) && items.CanUsePowerBombs() &&
                    (Logic.SuitlessLava ? items.HiJump || items.Gravity : items.SpaceJump && items.Gravity) ||
                (Logic.HellRun || items.Varia) && items.CanAccessNorfairLowerPortal() && items.CanDestroyBombWalls();
        }

    }

}
