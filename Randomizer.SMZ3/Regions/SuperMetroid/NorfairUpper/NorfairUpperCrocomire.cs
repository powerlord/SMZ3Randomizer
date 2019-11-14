using System.Collections.Generic;
using static Randomizer.SMZ3.Regions.SuperMetroid.NorfairUpperCrocomire.PantrySide;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class NorfairUpperCrocomire : SMRegion {

        public override string Name => "Norfair Upper Crocomire";
        public override string Area => "Norfair Upper";

        public NorfairUpperCrocomire(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 52, 0xC78BA4, LocationType.Visible, "Energy Tank, Crocomire",
                    items => CanPassCrocomire(items) && (Logic.AdditionalDamage || items.HasEnergyCapacity(1) || items.SpaceJump || items.Grapple)),
                new Location(this, 54, 0xC78BC0, LocationType.Visible, "Missile (above Crocomire)",
                    items => (Logic.MidAirIbj ? items.CanFly() : items.SpaceJump) || items.Grapple ||
                        Logic.ThreeTapCharge && items.SpeedBooster || items.HiJump && (
                            items.SpeedBooster ||
                            Logic.SpringBallGlitch && items.CanSpringBallJump() ||
                            Logic.GuidedEnemyFreeze && items.Varia && items.Ice
                        )),
                new Location(this, 57, 0xC78C04, LocationType.Visible, "Power Bomb (Crocomire)",
                    items => CanPassCrocomire(items) && (items.SpeedBooster || items.CanFly() || items.HiJump || items.Grapple ||
                        Logic.TrickyEnemyFreeze && items.Ice || Logic.SpringBallGlitch && items.CanSpringBallJump())),
                new Location(this, 58, 0xC78C14, LocationType.Visible, "Missile (below Crocomire)",
                    items => CanPassCrocomire(items) && items.Morph),
                new Location(this, 59, 0xC78C2A, LocationType.Visible, "Missile (Grapple Beam)",
                    items => CanPassCrocomire(items) && (
                        CanCrossPantryFromBottom(items, TopRight) ||
                        CanUseGrappleTutorialAndThenExit(items, TopRight)
                    )),
                new Location(this, 60, 0xC78C36, LocationType.Chozo, "Grapple Beam",
                    items => CanPassCrocomire(items) && (
                        CanCrossPantryFromBottom(items, Left) ||
                        CanUseGrappleTutorialAndThenExit(items, Left)
                    )),
            };
        }

        internal enum PantrySide { Left, TopRight }

        bool CanCrossPantryFromBottom(Progression items, PantrySide side) {
            return side == TopRight && Logic.ThreeTapCharge && items.SpeedBooster ||
                items.Morph && (
                    // Need mid air IBJ on left side since the platform sinks
                    (side == TopRight || Logic.MidAirIbj) && items.CanIbj() ||
                    items.SpaceJump ||
                    items.SpeedBooster && (
                        items.CanUsePowerBombs() ||
                        items.HiJump && (side == Left || items.Grapple) ||
                        // Get a shinespark by running through the acid
                        (Logic.ExcessiveDamage ?
                            items.HasEnergyCapacity(4) && items.Gravity :
                            items.HasEnergyCapacity(3) && items.Gravity && items.Varia)
                    )
                );
        }

        bool CanUseGrappleTutorialAndThenExit(Progression items, PantrySide side) {
            return Logic.GreenGate && items.Super && (
                items.SpaceJump ||
                // Exit through bottom part (need grapple if reaching "grapple missile")
                items.Morph && (side == Left || items.Grapple) ||
                // Softlock if reaching "grapple missile" due to missing Morph
                items.HiJump && items.Grapple && (side == Left || Logic.SoftlockRisk) ||
                side == Left && items.HiJump && (
                    // Just normal walljumps through water
                    items.Gravity ||
                    // Time a jump with the tide, with speedbooster but HiJump disabled (thanks Wild)
                    Logic.WildWallJump && items.SpeedBooster ||
                    // Spawning the Gerutas, freezing them before they scatter,
                    // then luring them to the right to freeze them again
                    Logic.GuidedEnemyFreeze && items.Ice
                )
            );
        }

        bool CanPassCrocomire(Progression items) {
            // Super is not required prior to Crocomire's door iif going through Speedway
            return items.Super && items.CanBeatCrocomire(Logic);
        }

        public override bool CanEnter(Progression items) {
            var tanks = Logic.ExcessiveDamage ? (3, 2) : (5, 2);
            return World.CanEnter<NorfairUpperWest>(items) && (
                    // Through Cathedral to Bubble Mountain
                    (items.Varia || Logic.HellRun && items.CanHellRunMaybeCf(tanks)) &&
                    // (No SpeedBooster for Cathedral since it simplifies to Speedway)
                    (items.CanFly() || items.HiJump || items.Varia && items.Ice || Logic.SpringBallGlitch && items.CanSpringBallJump()) &&
                    // Cathedral green door
                    items.Super && (
                        // Either down the mountain, or over the peak. Includes lava dive damage along reverse intended.
                        items.CanPassBombPassages() ||
                        (Logic.TrickyWallJump || items.SpaceJump || items.HiJump || items.Ice) && (Logic.SuitlessLava || items.Gravity)
                    ) &&
                        (Logic.BlueGate && items.CanBlueGateGlitch() || items.Wave) ||
                    // Through Ice Super Door to Croc Speedway, or Speedway to Bubble Mountain to Blue gate
                    (items.Varia || Logic.HellRun && items.CanHellRunMaybeCf(tanks)) && (
                        items.Super && items.SpeedBooster && items.CanUsePowerBombs() ||
                        items.SpeedBooster && (Logic.BlueGate && items.CanBlueGateGlitch() || items.Wave)
                    )
                ) ||
                // Through Mire portal, back through lava dive, to blue gate
                // (PB is implied with Springball due to CanDestroyBombWall)
                Logic.SuitlessLava && items.CanAccessNorfairLowerPortal() && items.Varia && items.HasEnergyCapacity(2) && items.Super &&
                    // Worst Room, destroy bomb blocks and get up (and dodging pirates)
                    items.CanDestroyBombWalls() && (
                        items.CanFly() || Logic.TrickyWallJump && items.HiJump || Logic.SpringBallGlitch && items.CanSpringBallJump() ||
                        Logic.GuidedEnemyFreeze && items.Charge && items.Ice
                    );
        }

    }

}
