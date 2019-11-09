using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class BrinstarPink : SMRegion {

        public override string Name => "Brinstar Pink";
        public override string Area => "Brinstar";

        public BrinstarPink(World world, Config config) : base(world, config) {
            Weight = -4;

            Locations = new List<Location> {
                new Location(this, 14, 0xC784E4, LocationType.Chozo, "Super Missile (pink Brinstar)",
                    items => items.CanPassBombPassages() && items.Super),
                new Location(this, 21, 0xC78608, LocationType.Visible, "Missile (pink Brinstar top)"),
                new Location(this, 22, 0xC7860E, LocationType.Visible, "Missile (pink Brinstar bottom)"),
                new Location(this, 23, 0xC78614, LocationType.Chozo, "Charge Beam",
                    items => items.CanPassBombPassages()),
                new Location(this, 24, 0xC7865C, LocationType.Visible, "Power Bomb (pink Brinstar)",
                    items => items.CanUsePowerBombs() && (Logic.AdditionalDamage || items.HasEnergyCapacity(1)) && items.Super),
                new Location(this, 25, 0xC78676, LocationType.Visible, "Missile (green Brinstar pipe)",
                    items => items.Morph && (items.Super || items.PowerBomb || items.CanAccessNorfairUpperPortal())),
                new Location(this, 33, 0xC787FA, LocationType.Visible, "Energy Tank, Waterway",
                    items => items.CanUsePowerBombs() && items.CanOpenRedDoors() && items.SpeedBooster &&
                        (items.Gravity || items.HasEnergyCapacity(1))),
                new Location(this, 35, 0xC78824, LocationType.Visible, "Energy Tank, Brinstar Gate",
                    items => items.CanUsePowerBombs() && (Logic.AdditionalDamage || items.HasEnergyCapacity(1)) &&
                        (Logic.GreenGate ? items.Super : items.Wave)),
            };
        }

        public override bool CanEnter(Progression items) {
            return World.CanEnter<BrinstarGreen>(items) && items.CanOpenRedDoors() ||
                World.CanEnter<BrinstarBlue>(items) && items.CanUsePowerBombs() ||
                items.CanAccessNorfairUpperPortal() && items.Morph &&
                    (items.SpaceJump || items.HiJump || items.Ice || Logic.SpringBallGlitch && items.CanSpringBallJump()) &&
                    (Logic.GreenGate || items.Wave);
        }

    }

}
