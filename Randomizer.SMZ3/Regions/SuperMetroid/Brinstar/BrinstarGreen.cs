using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class BrinstarGreen : SMRegion {

        public override string Name => "Brinstar Green";
        public override string Area => "Brinstar";

        public BrinstarGreen(World world, Config config) : base(world, config) {
            Weight = -6;

            Locations = new List<Location> {
                new Location(this, 13, 0xC784AC, LocationType.Chozo, "Power Bomb (green Brinstar bottom)",
                    items => items.CanUsePowerBombs()),
                new Location(this, 15, 0xC78518, LocationType.Visible, "Missile (green Brinstar below super missile)",
                    items => items.CanOpenRedDoors() && items.CanPassBombPassages()),
                new Location(this, 16, 0xC7851E, LocationType.Visible, "Super Missile (green Brinstar top)",
                    items => items.CanOpenRedDoors() && (items.SpeedBooster || Logic.MockBall && items.Morph)),
                new Location(this, 17, 0xC7852C, LocationType.Chozo, "Reserve Tank, Brinstar",
                    items => items.CanOpenRedDoors() && (items.SpeedBooster || Logic.MockBall && items.Morph)),
                new Location(this, 18, 0xC78532, LocationType.Hidden, "Missile (green Brinstar behind missile)",
                    items => items.CanOpenRedDoors() && (Logic.MockBall || items.SpeedBooster) && items.Morph &&
                        (items.CanPassBombPassages() || Logic.WildJump && items.ScrewAttack)),
                new Location(this, 19, 0xC78538, LocationType.Visible, "Missile (green Brinstar behind reserve tank)",
                    items => items.CanOpenRedDoors() && (Logic.MockBall || items.SpeedBooster) && items.Morph),
                new Location(this, 30, 0xC787C2, LocationType.Visible, "Energy Tank, Etecoons",
                    items => items.CanUsePowerBombs()),
                new Location(this, 31, 0xC787D0, LocationType.Visible, "Super Missile (green Brinstar bottom)",
                    items => items.CanUsePowerBombs() && items.Super),
            };
        }

        public override bool CanEnter(Progression items) {
            return items.CanDestroyBombWalls() || items.SpeedBooster;
        }

    }

}
