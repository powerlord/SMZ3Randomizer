using System.Collections.Generic;

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
                    // Having SpeedBooster implies you can reach both sides,
                    // so grapple only becomes a hard requirement in non-casual
                    items => (CanScaleToBroFist(items) || CanClimbMountainSlope(items) && items.Grapple) && items.Super),
                new Location(this, 64, 0xC78C66, LocationType.Visible, "Missile (bubble Norfair)"),
                new Location(this, 65, 0xC78C74, LocationType.Hidden, "Missile (Speed Booster)",
                    items => CanClimbMountain(items) && items.Super),
                new Location(this, 66, 0xC78C82, LocationType.Chozo, "Speed Booster",
                    items => CanClimbMountain(items) && items.Super),
                new Location(this, 67, 0xC78CBC, LocationType.Visible, "Missile (Wave Beam)",
                    // Todo: test if tricky walljump is Basic or Advanced
                    items => CanClimbMountain(items) && (
                        //Logic.[TimedWallJump/WildWallJump] ||
                        Logic.TrickyWallJump ||
                        items.CanOpenRedDoors() || items.CanFly() || items.HiJump ||
                        Logic.ShortCharge && items.SpeedBooster
                    )),
                new Location(this, 68, 0xC78CCA, LocationType.Chozo, "Wave Beam",
                    items => CanClimbMountain(items) && items.CanOpenRedDoors() && (
                        !Logic.SoftlockRisk ? items.Morph : (
                                items.Grapple || items.SpaceJump ||
                                Logic.AdditionalDamage && items.HiJump
                            ) &&
                            (Logic.BlueGate && items.CanBlueGateGlitch() || items.Wave)
                        )),
            };
        }

        bool CanClimbMountain(Progression items) {
            return CanClimbMountainWall(items) || CanClimbMountainSlope(items);
        }

        bool CanClimbMountainWall(Progression items) {
            return Logic.TrickyWallJump || items.CanFly() || items.HiJump || items.Ice;
        }

        bool CanClimbMountainSlope(Progression items) {
            return items.SpeedBooster && items.Morph;
        }

        bool CanScaleToBroFist(Progression items) {
            // AdditionalDamage implies damage boost over the peaks
            return Logic.TrickyWallJump && (Logic.AdditionalDamage || items.HiJump) || items.CanFly() || items.Ice;
        }

        public override bool CanEnter(Progression items) {
            var tanks = Logic.ExcessiveDamage ? (3, 2) : (5, 2);
            return World.CanEnter<NorfairUpperWest>(items) &&
                (items.Varia || Logic.HellRun && items.CanHellRunMaybeCf(tanks)) && (
                    // Cathedral route, Speedbooster is here for the case of Morph at Bubble Mountain Missile
                    items.Super && (
                        items.CanFly() || items.HiJump || items.SpeedBooster || items.Varia && items.Ice ||
                        Logic.SpringBallGlitch && items.CanSpringBallJump()
                    ) ||
                    // Cathedral or Speedway route with booster
                    items.SpeedBooster
                );
        }

    }

}
