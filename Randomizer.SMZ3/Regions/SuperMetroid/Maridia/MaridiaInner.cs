using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class MaridiaInner : SMRegion, IReward {

        public override string Name => "Maridia Inner";
        public override string Area => "Maridia";

        public RewardType Reward { get; set; } = RewardType.GoldenFourBoss;

        public MaridiaInner(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 140, 0xC7C4AF, LocationType.Visible, "Super Missile (yellow Maridia)",
                    items => CanAccessAndScaleCrabShaft(items) && (items.CanPassBombPassages() || items.CanSpringBallJump())),
                new Location(this, 141, 0xC7C4B5, LocationType.Visible, "Missile (yellow Maridia)",
                    items => CanAccessAndScaleCrabShaft(items) && (items.CanPassBombPassages() || items.CanSpringBallJump())),
                new Location(this, 142, 0xC7C533, LocationType.Visible, "Missile (yellow Maridia behind false wall)",
                    items => CanAccessAndScaleCrabShaft(items)),
                new Location(this, 143, 0xC7C559, LocationType.Chozo, "Plasma Beam",
                    items => Locations.Get("Space Jump").Available(items) && (
                        items.ScrewAttack || items.Plasma ||
                        items.HasEnergyCapacity(3) && (
                            Logic.ThreeTapCharge && Logic.AdditionalDamage && items.SpeedBooster ||
                            Logic.PseudoScrew && items.Charge
                        )
                    ) && (
                        items.CanFly() ||
                        Logic.TrickyWallJump && items.HiJump || 
                        Logic.ThreeTapCharge && items.SpeedBooster ||
                        Logic.SpringBallGlitch && items.CanSpringBallJump()
                    )),
                new Location(this, 144, 0xC7C5DD, LocationType.Visible, "Missile (left Maridia sand pit room)",
                    items => CanAccessAquaduct(items) && LeftSandPit(items)),
                new Location(this, 145, 0xC7C5E3, LocationType.Chozo, "Reserve Tank, Maridia",
                    items => CanAccessAquaduct(items) && LeftSandPit(items)),
                new Location(this, 146, 0xC7C5EB, LocationType.Visible, "Missile (right Maridia sand pit room)",
                    items => CanAccessAquaduct(items) &&
                        (items.Gravity || Logic.SuitlessWater && (Logic.SpringBallGlitch && items.CanSpringBallJump() || items.HiJump))),
                new Location(this, 147, 0xC7C5F1, LocationType.Visible, "Power Bomb (right Maridia sand pit room)",
                    items => CanAccessAquaduct(items) && items.Morph &&
                        (items.Gravity || Logic.SuitlessWater && Logic.SpringBallGlitch && items.CanSpringBallJump() && items.HiJump)),
                new Location(this, 148, 0xC7C603, LocationType.Visible, "Missile (pink Maridia)",
                    items => CanAccessAquaduct(items) && items.Gravity && (Logic.SnailClip || items.SpeedBooster)),
                new Location(this, 149, 0xC7C609, LocationType.Visible, "Super Missile (pink Maridia)",
                    items => CanAccessAquaduct(items) && items.Gravity && (Logic.SnailClip || items.SpeedBooster)),
                new Location(this, 150, 0xC7C6E5, LocationType.Chozo, "Spring Ball",
                    items => items.CanUsePowerBombs() && (
                        items.Grapple && (
                            items.Gravity && (items.SpaceJump || items.HiJump) ||
                            Logic.SuitlessWater && Logic.Cwj && items.SpaceJump && items.HiJump
                        ) ||
                        Logic.IceClip && items.Ice && items.Gravity
                    ) &&
                        items.Gravity || Logic.SuitlessWater && Logic.SpringBallGlitch && items.CanSpringBallJump() && items.HiJump),
                new Location(this, 151, 0xC7C74D, LocationType.Hidden, "Missile (Draygon)",
                    items => (CanAccessAquaduct(items) && CanDefeatBotwoon(items) || items.CanAccessMaridiaPortal(World)) &&
                        items.Super && CanCrossColosseum(items)),
                new Location(this, 152, 0xC7C755, LocationType.Visible, "Energy Tank, Botwoon",
                    items => CanAccessAquaduct(items) && CanDefeatBotwoon(items) || items.CanAccessMaridiaPortal(World)),
                new Location(this, 154, 0xC7C7A7, LocationType.Chozo, "Space Jump",
                    items => (CanAccessAquaduct(items) && CanDefeatBotwoon(items) || items.CanAccessMaridiaPortal(World)) &&
                        items.Super && CanCrossColosseum(items) && CanDefeatDraygon(items)),
            };
        }

        bool CanAccessAndScaleCrabShaft(Progression items) {
            // (Norfair / Portal -> Green Gate glitch at Crab Tunnel) -> Crab Shaft
            var fromEverest =
                World.CanEnter<NorfairUpperWest>(items) && items.CanUsePowerBombs() ||
                items.CanAccessMaridiaPortal(World) && items.Super && (Logic.GreenGate || items.CanUsePowerBombs());
            return (
                fromEverest ||
                // Portal -> Aquaduct -> Crab Shaft
                items.CanAccessMaridiaPortal(World) && items.CanPassBombPassages()
            ) && (
                items.Gravity || Logic.SuitlessWater && (
                    // HiJump from Aquaduct, plus Tricky freeze for the top
                    Logic.TrickyEnemyFreeze && items.Ice && items.HiJump ||
                    // Freeze enemy, Super for dislodge at bottom, Space Jump to break the surface at Pseudo Plasma Spark Room <-- Todo: Rename this room
                    // Todo: Move Space Jump out on Third Pass. Consider it for Watering Hole Missile + Super checks
                    Logic.GuidedEnemyFreeze && items.Ice && (fromEverest || items.Super) && items.SpaceJump ||
                    // SpringBall with either HiJump reach, or from frozen enemies
                    Logic.SpringBallGlitch && items.CanSpringBallJump() && (items.HiJump || Logic.GuidedEnemyFreeze && items.Ice)
                )
            );
        }

        bool CanAccessAquaduct(Progression items) {
            return items.Super && items.CanUsePowerBombs() || items.CanAccessMaridiaPortal(World);
        }

        bool LeftSandPit(Progression items) {
            return (
                // SuitlessWater with Gravity implies fighting the sand and a wall jump into mid air morph
                items.Gravity && (Logic.SuitlessWater || items.SpaceJump || items.HiJump) ||
                Logic.SuitlessWater && items.HiJump && (
                    Logic.TimedWallJump ||
                    items.SpaceJump ||
                    Logic.SpringBallGlitch && items.CanSpringBallJump()
                )
            ) &&
                // Access the items once reaching the "tunnel maze"
                (Logic.MidAirMorph && items.Morph || items.CanIbj() || items.CanSpringBallJump());
        }

        bool CanDefeatBotwoon(Progression items) {
            return (
                items.Charge && (Logic.WeakBeam || items.Ice && items.Wave && items.Spazer) ||
                Logic.SoftlockRisk && items.HasEnoughAmmo(6)
            ) &&
                // Access Botwoon's room from the left
                (items.SpeedBooster && items.Gravity || Logic.SuitlessWater && items.Ice);
        }

        bool CanCrossColosseum(Progression items) {
            return items.Gravity && (Logic.TrickyWallJump || items.SpaceJump || items.Grapple) ||
            Logic.SuitlessWater && items.HiJump && (
                Logic.TrickyEnemyFreeze && items.Ice ||
                items.Grapple
            );
        }

        bool CanDefeatDraygon(Progression items) {
            return (
                items.Charge && (Logic.WeakBeam || items.Ice && items.Wave && items.Spazer) ||
                Logic.SoftlockRisk && items.HasEnoughAmmo(12) ||
                items.Grapple && items.HasEnergyCapacity(5) ||
                Logic.ShortCharge && items.SpeedBooster && items.Gravity
            ) && (
                // Escape Draygon's room
                items.Gravity && (
                    // SuitlessWater implies Gravity Jump
                    Logic.SuitlessWater ||
                    items.CanFly() || items.SpeedBooster && items.HiJump
                ) ||
                    Logic.SpringBallGlitch && items.CanSpringBallJump() && items.Grapple
            );
        }

        public override bool CanEnter(Progression items) {
            return
                World.CanEnter<NorfairUpperWest>(items) && items.CanUsePowerBombs() && (
                    items.Gravity && (
                        // SuitlessWater implies Gravity Jump
                        Logic.SuitlessWater ||
                        items.CanFly() || items.SpeedBooster || items.Grapple
                    ) ||
                    // Super needed when missing either of HiJump or SpringBall to dislodge the first crab
                    Logic.SuitlessWater && (
                        items.HiJump && (
                            Logic.TrickyEnemyFreeze && items.Ice && items.Super ||
                            Logic.SpringBallGlitch && items.CanSpringBallJump()
                        ) && items.Grapple ||
                        // SpringBall jump instead of HiJump from frozen enemies
                        Logic.SpringBallGlitch && Logic.TrickyEnemyFreeze &&
                            items.CanSpringBallJump() && items.Ice && items.Super && items.Grapple
                    )
                ) ||
                CanEnterMaridiaFromPortal(items);
        }

        public bool CanEnterMaridiaFromPortal(Progression items) {
            return items.CanAccessMaridiaPortal(World) && items.Morph && (
                items.Gravity ||
                Logic.SuitlessWater && (
                    items.HiJump ||
                    Logic.SpringBallGlitch && items.CanSpringBallJump()
                )
            );
        }

        public bool CanComplete(Progression items) {
            return Locations.Get("Space Jump").Available(items);
        }

    }

}
