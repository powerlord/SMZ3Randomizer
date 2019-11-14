using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class BrinstarKraid : SMRegion, IReward {

        public override string Name => "Brinstar Kraid";
        public override string Area => "Brinstar";

        public RewardType Reward { get; set; } = RewardType.GoldenFourBoss;

        public BrinstarKraid(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 43, 0xC7899C, LocationType.Hidden, "Energy Tank, Kraid",
                    items => !Config.Keysanity || items.KraidKey),
                new Location(this, 48, 0xC78ACA, LocationType.Chozo, "Varia Suit",
                    items => !Config.Keysanity || items.KraidKey),
                new Location(this, 44, 0xC789EC, LocationType.Hidden, "Missile (Kraid)",
                    items => items.CanUsePowerBombs()),
            };
        }

        // Todo: Add in a route from Inner Maridia
        public override bool CanEnter(Progression items) {
            return (
                    // Through Brinstar Green
                    items.CanDestroyBombWalls() || items.SpeedBooster ||
                    // Through Ship, or Brinstar Blue
                    items.CanUsePowerBombs() ||
                    items.CanAccessNorfairUpperPortal()
                ) && items.Super && items.CanPassBombPassages();
        }

        public bool CanComplete(Progression items) {
            return Locations.Get("Varia Suit").Available(items);
        }

    }

}
