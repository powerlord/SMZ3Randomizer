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
                            Logic.SpringBallGlitch && items.CanSpringBallJump() ||
                            Logic.GuidedEnemyFreeze && items.Varia && items.Ice)),
                new Location(this, 57, 0xC78C04, LocationType.Visible, "Power Bomb (Crocomire)",
                    items => items.CanFly() || items.HiJump || items.Grapple ||
                        Logic.TrickyEnemyFreeze && items.Ice || Logic.SpringBallGlitch && items.CanSpringBallJump()),
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

        public override bool CanEnter(Progression items) {
            return items.Super &&
                // Todo: check this per location except escape missile?
                (items.Charge || Logic.SoftlockRisk && items.HasEnoughAmmo(Logic == Advanced ? 6 : 9)) && (
                    World.CanEnter<NorfairUpperWest>(items) && (
                        // Through Cathedral to Bubble Mountain
                        (items.Varia || Logic.HellRun && items.CanHellRunNorfairSafari(Logic)) &&
                        // (No SpeedBooster for Cathedral since it simplifies to Speedway)
                        (items.CanFly() || items.HiJump || items.Ice || Logic.SpringBallGlitch && items.CanSpringBallJump()) && (
                            // Either down the mountain, or over the peak. Includes lava dive damage along reverse intended.
                            items.CanPassBombPassages() ||
                            (Logic != Casual || items.SpaceJump || items.HiJump || items.Ice) && (Logic.SuitlessLava || items.Gravity)
                        ) &&
                            (Logic.GreenGate || items.Wave) ||
                        // Through Ice Super Door to Croc Speedway, or Speedway to Bubble Mountain to Blue gate
                        (items.Varia || Logic.HellRun && items.CanHellRunNorfairSafari(Logic)) && (
                            (Logic.MockBall || items.SpeedBooster) && items.CanUsePowerBombs() ||
                            items.SpeedBooster && (Logic.GreenGate || items.Wave)
                        )
                    ) ||
                    // Through Mire portal, back through lava dive, to blue gate
                    // (PB is implied with Springball due to CanDestroyBombWall)
                    Logic.SuitlessLava && items.CanAccessNorfairLowerPortal() && items.Varia && items.CanHellRun(2) &&
                        // Worst Room, destroy bomb blocks and get up (and dodging pirates)
                        items.CanDestroyBombWalls() && (
                            items.CanFly() || Logic.TrickyWallJump && items.HiJump || Logic.SpringBallGlitch && items.CanSpringBallJump() ||
                            Logic.GuidedEnemyFreeze && items.Charge && items.Ice
                        )
                );
        }

    }

}
