using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class BrinstarRed : SMRegion {

        public override string Name => "Brinstar Red";
        public override string Area => "Brinstar";

        public BrinstarRed(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 38, 0xC78876, LocationType.Chozo, "X-Ray Scope",
                    items => items.CanUsePowerBombs() && items.CanOpenRedDoors() && (
                        items.SpaceJump || items.Grapple ||
                        Logic.AdditionalDamage && (items.Varia && items.HasEnergyCapacity(3) || items.HasEnergyCapacity(5)) &&
                            (items.CanIbj() || items.HiJump && items.SpeedBooster || Logic.SpringBallGlitch && items.CanSpringBallJump())
                    )),
                new Location(this, 39, 0xC788CA, LocationType.Visible, "Power Bomb (red Brinstar sidehopper room)",
                    items => items.Super && items.CanUsePowerBombs()),
                new Location(this, 40, 0xC7890E, LocationType.Chozo, "Power Bomb (red Brinstar spike room)",
                    items => items.Super),
                new Location(this, 41, 0xC78914, LocationType.Visible, "Missile (red Brinstar spike room)",
                    items => items.Super && items.CanUsePowerBombs()),
                new Location(this, 42, 0xC7896E, LocationType.Chozo, "Spazer",
                    items => items.CanPassBombPassages() && items.Super),
            };
        }

        // Todo: consider Maridia portal
        public override bool CanEnter(Progression items) {
            return
                // Through Brinstar Green
                (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph ||
                // Through Ship, or Brinstar Blue
                items.CanUsePowerBombs() && items.Super ||
                items.CanAccessNorfairUpperPortal() && (
                    items.SpaceJump || items.HiJump || items.Ice ||
                    Logic.SpringBallGlitch && items.CanSpringBallJump()
                );
        }

    }

}
