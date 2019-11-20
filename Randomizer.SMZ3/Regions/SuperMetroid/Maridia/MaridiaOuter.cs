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
                    items => items.CanOpenRedDoors() && (
                        Logic.MidAirIbj && items.CanIbj() ||
                        items.SpaceJump || items.Grapple ||
                        items.SpeedBooster && items.Gravity ||
                        Logic.SpringBallGlitch && items.CanSpringBallJump() && (items.Gravity || items.HiJump)
                    )),
                new Location(this, 139, 0xC7C483, LocationType.Hidden, "Missile (green Maridia tatori)",
                    items => items.CanOpenRedDoors()),
            };
        }

        public override bool CanEnter(Progression items) {
            return (
                // Enter through Norfair -> Tube, or Portal -> (Tube / Crab Tunnel)
                World.CanEnter<NorfairUpperWest>(items) && items.CanUsePowerBombs() ||
                World.Region<MaridiaInner>().CanEnterMaridiaFromPortal(items) && items.Super && (Logic.GreenGate || items.CanUsePowerBombs())
            ) && (
                items.Gravity ||
                // Super needed when missing either of HiJump or SpringBall to dislodge the first crab
                Logic.SuitlessWater && (
                    items.HiJump && (items.Ice && items.Super || Logic.SpringBallGlitch && items.CanSpringBallJump()) ||
                    // SpringBall jump instead of HiJump from frozen enemies
                    items.Ice && items.Super && Logic.SpringBallGlitch && items.CanSpringBallJump()
                )
            ) ||
            // Enter through Portal -> Aquaduct
            // Freezing an enemy, with a Super to dislodge, is skipped since
            // Portal Corridor already require one of the other alternatives.
            World.Region<MaridiaInner>().CanEnterMaridiaFromPortal(items) && items.CanPassBombPassages();
        }

    }

}
