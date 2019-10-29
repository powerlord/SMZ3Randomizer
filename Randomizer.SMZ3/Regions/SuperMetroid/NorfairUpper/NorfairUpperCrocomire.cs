using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class NorfairUpperCrocomire : SMRegion {

        public override string Name => "Norfair Upper Crocomire";
        public override string Area => "Norfair Upper";

        public NorfairUpperCrocomire(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 52, 0xC78BA4, LocationType.Visible, "Energy Tank, Crocomire",
                    items => Logic.CanTakeAdditionalDamage || items.HasEnergyReserves(1) || items.SpaceJump || items.Grapple),
                // Todo: Maybe think of a new name for Frozen Hostile logic?
                new Location(this, 54, 0xC78BC0, LocationType.Visible, "Missile (above Crocomire)",
                    items => items.CanFly() || items.Grapple ||
                        Logic.HellRun && items.CanHellRun(4) && items.HiJump && (
                            items.SpeedBooster ||
                            Logic.SpringBallJump && items.CanSpringBallJump() ||
                            Logic.FrozenHostile && items.Varia && items.Ice)),
                new Location(this, 57, 0xC78C04, LocationType.Visible, "Power Bomb (Crocomire)",
                    items => items.CanFly() || items.HiJump || items.Grapple ||
                        Logic.FrozenEnemy && items.Ice || Logic.SpringBallJump && items.CanSpringBallJump()),
                new Location(this, 58, 0xC78C14, LocationType.Visible, "Missile (below Crocomire)",
                    items => items.Morph),
                // Todo: name for something where you might be forced to leave the room to replicate
                new Location(this, 59, 0xC78C2A, LocationType.Visible, "Missile (Grapple Beam)",
                    items => items.Morph && (items.CanFly() || items.SpeedBooster && (Logic.ThreeTapSpeed || items.CanUsePowerBombs())) ||
                        Logic.GreenGate && items.Super && (items.SpaceJump || /*Logic.Something && */items.Morph && items.Grapple)),
                new Location(this, 60, 0xC78C36, LocationType.Chozo, "Grapple Beam",
                    items => Logic.GreenGate || items.Morph && (items.CanFly() || items.SpeedBooster && items.CanUsePowerBombs())),
            };
        }

        // Todo: combine logic into one expression
        public override bool CanEnter(Progression items) {
            return Logic switch {
                _ when Logic == Casual =>
                    items.Super && items.Varia && items.Charge && (
                        // Through Cathedral and Bubble Mountain, to blue gate, to Croc (avoid lava dive damage)
                        (items.CanIbj() || items.HiJump || items.Ice || items.SpaceJump) && (items.Gravity || items.CanPassBombPassages()) ||
                        // Through Super Door to Croc speedway, or speedway to blue gate next to Croc
                        items.Wave && (items.SpeedBooster || items.CanUsePowerBombs())
                    ),
                // Lower Norfair's portal is not considered on Casual
                _ when Logic == Basic =>
                    items.Supers >= 2 && items.Missiles >= 3 && (
                        (items.HasEnergyReserves(5) || items.HasEnergyReserves(2) && items.CanCrystalFlash() || items.HasEnergyReserves(2) && items.Varia) &&
                            // Through Cathedral and Bubble Mountain
                            items.CanPassBombPassages() && (items.CanIbj() || items.HiJump || items.Ice) ||
                        // Through Croc speedway using the Ice Super Door, or speedway to Bubble Mountain
                        items.SpeedBooster && items.CanUsePowerBombs() && items.HasEnergyReserves(2) ||
                        // Through Mire portal, back through lava dive, to blue gate to Croc
                        items.CanAccessNorfairLowerPortal() && items.CanFly() && items.CanDestroyBombWalls() && items.HasEnergyReserves(2) && items.Varia
                    ),
                _ =>
                    items.Supers >= 2 && (
                        (items.HasEnergyReserves(3) || items.HasEnergyReserves(2) && items.CanCrystalFlash() || items.HasEnergyReserves(1) && items.Varia) &&
                            // Through Cathedral and Bubble Mountain
                            items.CanPassBombPassages() && (items.CanIbj() || items.HiJump || items.Ice || items.CanSpringBallJump()) ||
                        // Through Croc speedway using the Ice Super Door, or speedway to Bubble Mountain
                        items.SpeedBooster && items.CanUsePowerBombs() && items.HasEnergyReserves(2) ||
                        // Through Mire portal, back through lava dive, to blue gate to Croc
                        items.CanAccessNorfairLowerPortal() && items.CanDestroyBombWalls() && (items.CanSpringBallJump() || items.CanFly()) && items.HasEnergyReserves(2) && items.Varia
                    ),
            };
        }

    }

}
