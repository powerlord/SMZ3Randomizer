using System;
using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class NorfairUpperCrocomire : SMRegion {

        public override string Name => "Norfair Upper Crocomire";
        public override string Area => "Norfair Upper";

        public NorfairUpperCrocomire(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 52, 0xC78BA4, LocationType.Visible, "Energy Tank, Crocomire",
                    items => Logic.AdditionalDamage || items.HasEnergyCapacity(1) || items.SpaceJump || items.Grapple),
                // Todo: Maybe think of a new name for the FrozenHostile setting?
                new Location(this, 54, 0xC78BC0, LocationType.Visible, "Missile (above Crocomire)",
                    items => items.CanFly() || items.Grapple ||
                        (items.Varia || Logic.HellRun && items.CanHellRun(4)) && items.HiJump && (
                            items.SpeedBooster ||
                            Logic.SpringBallJump && items.CanSpringBallJump() ||
                            Logic.GuidedEnemyFreeze && items.Varia && items.Ice)),
                new Location(this, 57, 0xC78C04, LocationType.Visible, "Power Bomb (Crocomire)",
                    items => items.CanFly() || items.HiJump || items.Grapple ||
                        Logic.TrickyEnemyFreeze && items.Ice || Logic.SpringBallJump && items.CanSpringBallJump()),
                new Location(this, 58, 0xC78C14, LocationType.Visible, "Missile (below Crocomire)",
                    items => items.Morph),
                // Todo: name for something where you might be forced to leave the room to replicate. is it "SoftlockRisk"?
                new Location(this, 59, 0xC78C2A, LocationType.Visible, "Missile (Grapple Beam)",
                    items => items.Morph && (items.CanFly() || items.SpeedBooster && (Logic.ThreeTapCharge || items.CanUsePowerBombs())) ||
                        Logic.GreenGate && items.Super && (items.SpaceJump || Logic.SoftlockRisk && items.Morph && items.Grapple)),
                new Location(this, 60, 0xC78C36, LocationType.Chozo, "Grapple Beam",
                    items => Logic.GreenGate || items.Morph && (items.CanFly() || items.SpeedBooster && items.CanUsePowerBombs())),
            };
        }

        // Todo: combine logic into one expression
        public override bool CanEnter(Progression items) {
            var (hellrun, crystalFlash, varia) = Logic.ExcessiveDamage ? (3, 2, 1) : (5, 2, 1);
            return Logic switch {
                _ when Logic == Casual =>
                    items.Super &&
                    items.Charge &&
                    items.Varia && (
                        // Through Cathedral and Bubble Mountain to blue gate to Croc (avoid lava dive damage)
                        (items.CanFly() || items.HiJump || items.Ice) && (items.Gravity || items.CanPassBombPassages()) ||
                        // Through Ice Super Door to Croc Speedway, or Speedway to Bubble Mountain to blue gate
                        (Logic.MockBall || items.SpeedBooster) && items.CanUsePowerBombs() || items.SpeedBooster && (Logic.GreenGate || items.Wave)
                    ),
                // Lower Norfair's portal is not considered on Casual
                _ =>
                    items.Super &&
                    (items.Charge || Logic.SoftlockRisk && items.HasEnoughAmmo(Logic == Advanced ? 6 : 9)) && (
                        // Through Cathedral and Bubble Mountain
                        (items.Varia || Logic.HellRun && (
                            items.CanHellRun(hellrun) ||
                            items.CanHellRun(crystalFlash) && items.CanCrystalFlash() ||
                            items.CanHellRun(varia) && items.Varia
                        ))
                        && (items.CanFly() || items.HiJump || items.Ice || Logic.SpringBallJump && items.CanSpringBallJump()) &&
                            (Logic.LavaDive || items.CanPassBombPassages()) ||
                        // Through Ice Super Door to Croc Speedway, or Speedway to Bubble Mountain to blue gate
                        (items.Varia || Logic.HellRun && items.CanHellRun(2)) && (
                            (Logic.MockBall || items.SpeedBooster) && items.CanUsePowerBombs() ||
                            items.SpeedBooster && (Logic.GreenGate || items.Wave)
                        ) ||
                        // Through Mire portal, back through lava dive, to blue gate to Croc
                        Logic.LavaDive && items.Varia && items.CanHellRun(2) &&
                            items.CanAccessNorfairLowerPortal() && items.CanDestroyBombWalls() &&
                            (items.CanFly() || Logic.SpringBallJump && items.CanSpringBallJump())
                    ),
            };
        }

    }

}
