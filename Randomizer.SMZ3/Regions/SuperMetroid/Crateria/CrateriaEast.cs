using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class CrateriaEast : SMRegion {

        public override string Name => "Crateria East";
        public override string Area => "Crateria";

        public CrateriaEast(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 4, 0xC78248, LocationType.Visible, "Missile (Crateria moat)"),
                new Location(this, 1, 0xC781E8, LocationType.Visible, "Missile (outside Wrecked Ship bottom)",
                    items => EnterMoatFromWest(items) && CanCrossMoat(items) || EnterWreckedShipFromEast(items)),
                new Location(this, 3, 0xC781F4, LocationType.Visible, "Missile (outside Wrecked Ship middle)",
                    items => World.Region<WreckedShip>().CanComplete(items) &&
                        items.Super && items.CanPassBombPassages()),
                new Location(this, 2, 0xC781EE, LocationType.Hidden, "Missile (outside Wrecked Ship top)",
                    items => World.Region<WreckedShip>().CanComplete(items) &&
                        (items.CanFly() || items.HiJump || items.SpeedBooster || Logic.TrickyEnemyFreeze && items.Ice)),
            };
        }

        public bool CanCrossMoat(Progression items) {
            return items.SpaceJump || items.Grapple || items.SpeedBooster ||
                items.Gravity && (Logic.SuitlessWater || items.CanIbj() || items.HiJump) ||
                Logic.Cwj || Logic.BounceSpringBall && items.CanSpringBallJump();
        }

        public bool CanCrossOcean(Progression items) {
            return Logic.TrickyWallJump || items.SpaceJump || items.Grapple;
        }

        public override bool CanEnter(Progression items) {
            return EnterMoatFromWest(items) || EnterWreckedShipFromEast(items);
        }

        bool EnterMoatFromWest(Progression items) {
            return items.CanUsePowerBombs() && (
                // From Ship
                items.Super || (
                    // Enter from either Norfair or Maridia portal
                    items.CanAccessNorfairUpperPortal() ||
                    World.Region<MaridiaInner>().CanEnterMaridiaFromPortal(items) && items.Gravity && items.Super
                ) &&
                    // Up Red Tower
                    (items.HiJump || items.Ice || items.SpaceJump || Logic.SpringBallGlitch && items.CanSpringBallJump())
            );
        }

        bool EnterWreckedShipFromEast(Progression items) {
            // Destroy bomb blocks at Turbo Shaft
            return (
                items.CanAccessNorfairUpperPortal() && items.CanUsePowerBombs() ||
                World.Region<MaridiaInner>().CanEnterMaridiaFromPortal(items)
            ) && items.Gravity && items.CanDestroyBombWalls() && items.Super;
        }

    }

}
