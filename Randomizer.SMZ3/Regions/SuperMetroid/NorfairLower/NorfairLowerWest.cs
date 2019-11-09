using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

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
                            Logic.SpringBallGlitch && items.CanSpringBallJump() ||
                            Logic.ShortCharge && items.SpeedBooster
                        ) && items.Super
                    ) && CanBeatGT(items)),
                new Location(this, 71, 0xC78E74, LocationType.Hidden, "Super Missile (Gold Torizo)",
                    items => (items.Varia || Logic.HellRun && items.CanHellRun(5)) && (
                        items.CanAccessNorfairLowerPortal() ||
                        items.CanUsePowerBombs() && items.SpaceJump || Logic.GreenGate && items.Super
                    ) && CanBeatGT(items)),
                new Location(this, 79, 0xC79110, LocationType.Chozo, "Screw Attack",
                    items => items.CanAccessNorfairLowerPortal() ||
                        items.CanUsePowerBombs() && (items.SpaceJump && CanBeatGT(items) || Logic.GreenGate && items.Super)),
            };
        }

        bool CanBeatGT(Progression items) {
            return items.Charge && (Logic != Casual || items.Ice && items.Wave && items.Spazer) ||
                Logic.SoftlockRisk && (Logic.ExcessiveDamage ? items.Supers >= 3 && items.Grapple : items.Supers >= 6);
        }

        public override bool CanEnter(Progression items) {
            return items.Varia && World.CanEnter<NorfairUpperEast>(items) && items.CanUsePowerBombs() &&
                (Logic.SuitlessLava ? (items.HiJump || items.Gravity) : items.SpaceJump && items.Gravity) ||
            items.CanAccessNorfairLowerPortal() && items.CanDestroyBombWalls();
        }

    }

}
