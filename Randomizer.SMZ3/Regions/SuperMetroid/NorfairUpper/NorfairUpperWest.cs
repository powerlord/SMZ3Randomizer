using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class NorfairUpperWest : SMRegion {

        public override string Name => "Norfair Upper West";
        public override string Area => "Norfair Upper";

        public NorfairUpperWest(World world, Config config) : base(world, config) {
            var tanks = Logic.ExcessiveDamage ? (3, 2) : (5, 2);
            Locations = new List<Location> {
                // Cathedral Missile. The safari logic is a convenience for continuing to East or Crocomire
                new Location(this, 49, 0xC78AE4, LocationType.Hidden, "Missile (lava room)",
                    items => (items.Varia || Logic.HellRun && items.CanHellRunMaybeCf(tanks)) &&
                        items.CanOpenRedDoors() && items.Morph),
                new Location(this, 50, 0xC78B24, LocationType.Chozo, "Ice Beam",
                    items => items.Super && items.CanPassBombPassages() &&
                        (Logic.MockBall || items.SpeedBooster) &&
                        (items.Varia || Logic.HellRun && items.CanHellRunWithoutCf(3))),
                new Location(this, 51, 0xC78B46, LocationType.Hidden, "Missile (below Ice Beam)",
                    items => items.Super && (
                        items.CanUsePowerBombs() &&
                            (Logic.MockBall || items.SpeedBooster) &&
                            (items.Varia || Logic.HellRun && items.CanHellRunWithoutCf(3)) ||
                        Logic.TrickyShineSpark && items.SpeedBooster && items.CanBeatCrocomire(Logic) &&
                            (items.Varia || Logic.HellRun && items.CanHellRunMaybeCf((5, 3)))
                    )),
                new Location(this, 53, 0xC78BAC, LocationType.Chozo, "Hi-Jump Boots",
                    items => items.CanOpenRedDoors() && items.CanPassBombPassages()),
                new Location(this, 55, 0xC78BE6, LocationType.Visible, "Missile (Hi-Jump Boots)",
                    items => items.CanOpenRedDoors() && items.Morph),
                new Location(this, 56, 0xC78BEC, LocationType.Visible, "Energy Tank (Hi-Jump Boots)",
                    items => items.CanOpenRedDoors()),
            };
        }

        // Todo: Maridia again
        public override bool CanEnter(Progression items) {
            return (
                // Through Brinstar Green
                items.CanDestroyBombWalls() || items.SpeedBooster ||
                // Through Ship, or Brinstar Blue
                items.CanUsePowerBombs() ||
                items.CanAccessNorfairUpperPortal()
            ) && items.Super && items.Morph;
        }

    }

}
