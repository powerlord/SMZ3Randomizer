using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class WreckedShip : SMRegion, IReward {

        public override string Name => "Wrecked Ship";
        public override string Area => "Wrecked Ship";

        public RewardType Reward { get; set; } = RewardType.GoldenFourBoss;

        public WreckedShip(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 128, 0xC7C265, LocationType.Visible, "Missile (Wrecked Ship middle)",
                    items => items.CanPassBombPassages()),
                new Location(this, 129, 0xC7C2E9, LocationType.Chozo, "Reserve Tank, Wrecked Ship",
                    items => CanUnlockShip(items) && CanCrossBowling(items, excessive: 2)),
                new Location(this, 130, 0xC7C2EF, LocationType.Visible, "Missile (Gravity Suit)",
                    items => Locations.Get("Gravity Suit").Available(items)),
                new Location(this, 131, 0xC7C319, LocationType.Visible, "Missile (Wrecked Ship top)",
                    items => CanUnlockShip(items)),
                new Location(this, 132, 0xC7C337, LocationType.Visible, "Energy Tank, Wrecked Ship",
                    items => CanUnlockShip(items) && (items.HiJump || items.SpaceJump || items.SpeedBooster || items.Gravity ||
                        Logic.SpringBallGlitch && (items.CanFly() || items.CanSpringBallJump()))),
                new Location(this, 133, 0xC7C357, LocationType.Visible, "Super Missile (Wrecked Ship left)",
                    items => CanUnlockShip(items)),
                new Location(this, 134, 0xC7C365, LocationType.Visible, "Right Super, Wrecked Ship",
                    items => CanUnlockShip(items)),
                new Location(this, 135, 0xC7C36D, LocationType.Chozo, "Gravity Suit",
                    items => CanUnlockShip(items) && CanCrossBowling(items, excessive: 1)),
            };
        }

        bool CanUnlockShip(Progression items) {
            return items.CanPassBombPassages() && (!Config.Keysanity || items.PhantoonKey);
        }

        bool CanCrossBowling(Progression items, int excessive) {
            return items.Grapple || items.SpaceJump ||
                Logic.AdditionalDamage && (
                    Logic.ExcessiveDamage ?
                    items.Varia || items.HasEnergyCapacity(excessive) :
                    items.Varia && items.HasEnergyCapacity(2) || items.HasEnergyCapacity(3)
                );
        }

        public override bool CanEnter(Progression items) {
            return items.Super && (
                items.CanUsePowerBombs() && (
                    // Through Turbo Shaft, Forgotten Highway
                    items.CanAccessNorfairUpperPortal() && items.Gravity ||
                    items.CanCrossMoat(Logic)
                ) ||
                // Dark World access with no Flute or Lamp. Destroy bomb blocks at Turbo Shaft
                items.CanAccessMaridiaPortal(World) && items.Gravity && items.CanDestroyBombWalls()
            );
        }

        public bool CanComplete(Progression items) {
            return CanEnter(items) && CanUnlockShip(items);
        }

    }

}
