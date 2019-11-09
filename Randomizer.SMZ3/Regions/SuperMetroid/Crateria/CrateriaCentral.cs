using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class CrateriaCentral : SMRegion {

        public override string Name => "Crateria Central";
        public override string Area => "Crateria";

        public CrateriaCentral(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 0, 0xC781CC, LocationType.Visible, "Power Bomb (Crateria surface)",
                    items => items.CanUsePowerBombs() && (items.SpeedBooster || items.CanFly())),
                new Location(this, 12, 0xC78486, LocationType.Visible, "Missile (Crateria middle)",
                    items => items.CanPassBombPassages()),
                new Location(this, 6, 0xC783EE, LocationType.Visible, "Missile (Crateria bottom)",
                    items => items.CanDestroyBombWalls() || Logic.ThreeTapCharge && items.SpeedBooster),
                new Location(this, 11, 0xC78478, LocationType.Visible, "Super Missile (Crateria)",
                    items => items.CanUsePowerBombs() && items.HasEnergyCapacity(2) && items.SpeedBooster &&
                        (Logic.AdditionalDamage || Logic.ShortCharge || items.Ice || items.ScrewAttack || items.PowerBombs >= 2)),
                new Location(this, 7, 0xC78404, LocationType.Chozo, "Bombs",
                    items => (Logic.TrickyWallJump ? items.Morph : items.CanPassBombPassages()) && items.CanOpenRedDoors()),
            };
        }

    }

}
