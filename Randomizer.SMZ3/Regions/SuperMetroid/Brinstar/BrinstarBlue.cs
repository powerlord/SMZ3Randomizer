using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class BrinstarBlue : SMRegion {

        public override string Name => "Brinstar Blue";
        public override string Area => "Brinstar";

        public BrinstarBlue(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 26, 0xC786EC, LocationType.Visible, "Morphing Ball"),
                new Location(this, 27, 0xC7874C, LocationType.Visible, "Power Bomb (blue Brinstar)",
                    items => items.CanUsePowerBombs()),
                new Location(this, 28, 0xC78798, LocationType.Visible, "Missile (blue Brinstar middle)",
                    items => items.Morph),
                new Location(this, 29, 0xC7879E, LocationType.Hidden, "Energy Tank, Brinstar Ceiling",
                    items => Logic.AdditionalDamage || items.SpaceJump || items.HiJump || items.SpeedBooster || items.Ice),
                new Location(this, 34, 0xC78802, LocationType.Chozo, "Missile (blue Brinstar bottom)",
                    items => items.Morph),
                new Location(this, 36, 0xC78836, LocationType.Visible, "Missile (blue Brinstar top)",
                    items => items.CanUsePowerBombs()),
                new Location(this, 37, 0xC7883C, LocationType.Hidden, "Missile (blue Brinstar behind missile)",
                    items => items.CanUsePowerBombs()),
            };
        }

    }

}
