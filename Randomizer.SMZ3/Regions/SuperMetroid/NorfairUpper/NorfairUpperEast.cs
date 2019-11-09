using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class NorfairUpperEast : SMRegion {

        public override string Name => "Norfair Upper East";
        public override string Area => "Norfair Upper";

        public NorfairUpperEast(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 61, 0xC78C3E, LocationType.Chozo, "Reserve Tank, Norfair",
                    items => Locations.Get("Missile (bubble Norfair green door)").Available(items) && items.Morph),
                new Location(this, 62, 0xC78C44, LocationType.Hidden, "Missile (Norfair Reserve Tank)",
                    items => Locations.Get("Missile (bubble Norfair green door)").Available(items) && items.Morph),
                new Location(this, 63, 0xC78C52, LocationType.Visible, "Missile (bubble Norfair green door)",
                    items => (CanScaleToBroFist(items) || CanClimbMountainSlope(items) && items.Grapple) && items.Super),
                new Location(this, 64, 0xC78C66, LocationType.Visible, "Missile (bubble Norfair)"),
                new Location(this, 65, 0xC78C74, LocationType.Hidden, "Missile (Speed Booster)",
                    items => CanClimbMountain(items) && items.Super),
                new Location(this, 66, 0xC78C82, LocationType.Chozo, "Speed Booster",
                    items => CanClimbMountain(items) && items.Super),
                new Location(this, 67, 0xC78CBC, LocationType.Visible, "Missile (Wave Beam)",
                    items => CanClimbMountain(items) && (
                        Logic == Advanced ||
                        items.CanOpenRedDoors() ||
                        (Logic.ShortCharge || items.HiJump) && items.SpeedBooster ||
                        items.CanFly())),
                new Location(this, 68, 0xC78CCA, LocationType.Chozo, "Wave Beam",
                    items => CanClimbMountain(items) && items.CanOpenRedDoors() && (!Logic.SoftlockRisk ? items.Morph : (
                            items.Grapple || items.SpaceJump ||
                            items.HiJump && (Logic.ExcessiveDamage || items.Varia) ||
                            Logic.SpringBallGlitch && items.CanSpringBallJump()
                        ) && (Logic.GreenGate || items.Wave))),
            };
        }

        bool CanClimbMountain(Progression items) {
            return CanClimbMountainWall(items) || CanClimbMountainSlope(items);
        }

        bool CanClimbMountainWall(Progression items) {
            return Logic != Casual || items.CanFly() || items.HiJump || items.Ice;
        }

        bool CanClimbMountainSlope(Progression items) {
            return items.SpeedBooster && items.Morph;
        }

        bool CanScaleToBroFist(Progression items) {
            return Logic != Casual || items.CanFly() || items.Ice;
        }

        public override bool CanEnter(Progression items) {
            return World.CanEnter<NorfairUpperWest>(items) &&
                (items.Varia || Logic.HellRun && items.CanHellRunNorfairSafari(Logic)) && (
                    // Cathedral route, Speedbooster is here iif Morph Ball is at Bubble Mountain Missile
                    items.Super && (
                        items.CanFly() || items.HiJump || items.SpeedBooster || items.Ice ||
                        Logic.SpringBallGlitch && items.CanSpringBallJump()
                    ) ||
                    // Cathedral or Speedway route with booster
                    items.SpeedBooster
                );
        }

    }

}
