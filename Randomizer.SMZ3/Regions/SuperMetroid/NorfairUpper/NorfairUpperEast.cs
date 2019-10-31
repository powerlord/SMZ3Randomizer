using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class NorfairUpperEast : SMRegion {

        public override string Name => "Norfair Upper East";
        public override string Area => "Norfair Upper";

        public NorfairUpperEast(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 49, 0xC78AE4, LocationType.Hidden, "Missile (lava room)", Logic switch {
                    _ => items => items.Morph
                }),
                new Location(this, 61, 0xC78C3E, LocationType.Chozo, "Reserve Tank, Norfair", Logic switch {
                    Casual => items => items.Morph && (
                        items.CanFly() || items.Grapple && (items.SpeedBooster || items.CanPassBombPassages()) ||
                        items.HiJump || items.Ice),
                    _ => items => items.Morph && items.Super
                }),
                new Location(this, 62, 0xC78C44, LocationType.Hidden, "Missile (Norfair Reserve Tank)", Logic switch {
                    Casual => items => items.Morph && (
                        items.CanFly() || items.Grapple && (items.SpeedBooster || items.CanPassBombPassages()) ||
                        items.HiJump || items.Ice),
                    _ => items => items.Morph && items.Super
                }),
                new Location(this, 63, 0xC78C52, LocationType.Visible, "Missile (bubble Norfair green door)", Logic switch {
                    Casual => items => items.CanFly() ||
                        items.Grapple && items.Morph && (items.SpeedBooster || items.CanPassBombPassages()) ||
                        items.HiJump || items.Ice,
                    _ => items => items.Super
                }),
                new Location(this, 64, 0xC78C66, LocationType.Visible, "Missile (bubble Norfair)"),
                new Location(this, 65, 0xC78C74, LocationType.Hidden, "Missile (Speed Booster)", Logic switch {
                    Casual => items => items.CanFly() || items.Morph && (items.SpeedBooster || items.CanPassBombPassages()) ||
                        items.HiJump || items.Ice,
                    _ => items => items.Super
                }),
                // Todo: Consider scaling the mountain given Cathedral vs Speedway + intended route
                new Location(this, 66, 0xC78C82, LocationType.Chozo, "Speed Booster",
                    items => items.Super && (items.CanFly() || items.HiJump || Logic.TrickyEnemyFreeze && items.Ice)),
                new Location(this, 67, 0xC78CBC, LocationType.Visible, "Missile (Wave Beam)",
                    items => Logic == Advanced || items.CanOpenRedDoors() || (Logic.ShortCharge || items.HiJump) && items.SpeedBooster || items.CanFly()),
                new Location(this, 68, 0xC78CCA, LocationType.Chozo, "Wave Beam",
                    items => items.CanOpenRedDoors() && (!Logic.SoftlockRisk ? items.Morph : (
                            items.Grapple || items.SpaceJump ||
                            items.HiJump && (Logic.ExcessiveDamage || items.Varia) ||
                            Logic.SpringBallJump && items.CanSpringBallJump()
                        ) && (Logic.GreenGate || items.Wave))),
            };
        }

        public override bool CanEnter(Progression items) {
            // Norfair Main Street Access
            return (
                (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph ||
                items.CanAccessNorfairUpperPortal()
            ) &&
            (items.Varia || Logic.HellRun && items.CanHellRun(5)) && (
                // Cathedral route
                items.Super && (
                    items.CanFly() || items.HiJump || items.SpeedBooster ||
                    Logic.TrickyEnemyFreeze && items.Ice ||
                    Logic.SpringBallJump && items.CanSpringBallJump()
                ) ||
                // Speedway route
                items.SpeedBooster && (items.CanUsePowerBombs() || Logic.MidAirMorph && items.CanPassBombPassages())
            );
        }

    }

}
