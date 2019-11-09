using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class CrateriaWest : SMRegion {

        public override string Name => "Crateria West";
        public override string Area => "Crateria";

        public CrateriaWest(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 8, 0xC78432, LocationType.Visible, "Energy Tank, Terminator"),
                new Location(this, 5, 0xC78264, LocationType.Visible, "Energy Tank, Gauntlet",
                    items => CanEnterAndLeaveGauntlet(items) && (Logic.AdditionalDamage || items.HasEnergyCapacity(1))),
                new Location(this, 9, 0xC78464, LocationType.Visible, "Missile (Crateria gauntlet right)",
                    items => CanEnterAndLeaveGauntlet(items) && items.CanPassBombPassages() && (Logic.AdditionalDamage || items.HasEnergyCapacity(2))),
                new Location(this, 10, 0xC7846A, LocationType.Visible, "Missile (Crateria gauntlet left)",
                    items => CanEnterAndLeaveGauntlet(items) && items.CanPassBombPassages() && (Logic.AdditionalDamage || items.HasEnergyCapacity(2))),
            };
        }

        bool CanEnterAndLeaveGauntlet(Progression items) {
            return (Logic.TrickyWallJump || items.CanFly() || items.SpeedBooster) && (
                items.Morph && (
                    items.Bombs ||
                    items.PowerBombs >= 2 ||
                    items.PowerBombs >= 1 && items.SpeedBooster && items.HasEnergyCapacity(2)
                ) ||
                items.ScrewAttack
            ) &&
                (Logic.SoftlockRisk || items.Morph);
        }

        public override bool CanEnter(Progression items) {
            return items.CanDestroyBombWalls() || items.SpeedBooster;
        }

    }

}
