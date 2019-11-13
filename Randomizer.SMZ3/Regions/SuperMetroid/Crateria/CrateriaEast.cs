using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class CrateriaEast : SMRegion {

        public override string Name => "Crateria East";
        public override string Area => "Crateria";

        public CrateriaEast(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 4, 0xC78248, LocationType.Visible, "Missile (Crateria moat)"),
                new Location(this, 1, 0xC781E8, LocationType.Visible, "Missile (outside Wrecked Ship bottom)",
                    items => items.Gravity && (
                            items.CanAccessMaridiaPortal(World) && items.CanDestroyBombWalls() ||
                            items.CanAccessNorfairUpperPortal() && items.PowerBomb
                        ) ||
                        items.CanCrossMoat(Logic)),
                new Location(this, 3, 0xC781F4, LocationType.Visible, "Missile (outside Wrecked Ship middle)",
                    items => World.Region<WreckedShip>().CanComplete(items) &&
                        (items.CanFly() || items.HiJump || items.SpeedBooster)),
                new Location(this, 2, 0xC781EE, LocationType.Hidden, "Missile (outside Wrecked Ship top)",
                    items => World.Region<WreckedShip>().CanComplete(items) &&
                        items.CanPassBombPassages()),
            };
        }

        public override bool CanEnter(Progression items) {
            return items.CanUsePowerBombs() && (
                    // From Ship
                    items.Super ||
                    // Up Red Tower
                    items.CanAccessNorfairUpperPortal() && (items.HiJump || items.Ice || items.SpaceJump || Logic.SpringBallGlitch && items.CanSpringBallJump())
                ) ||
                // Dark World access with no Flute or Lamp. Destroy bomb blocks at Turbo Shaft
                items.CanAccessMaridiaPortal(World) && items.Gravity && items.CanDestroyBombWalls() && items.Super;
        }

    }

}
