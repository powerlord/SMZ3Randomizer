using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class NorfairLowerEast : SMRegion, IReward {

        public override string Name => "Norfair Lower East";
        public override string Area => "Norfair Lower";

        public RewardType Reward { get; set; } = RewardType.GoldenFourBoss;

        public NorfairLowerEast(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 73, 0xC78F30, LocationType.Visible, "Missile (Mickey Mouse room)",
                    items => items.Morph),
                new Location(this, 74, 0xC78FCA, LocationType.Visible, "Missile (lower Norfair above fire flea room)"),
                new Location(this, 75, 0xC78FD2, LocationType.Visible, "Power Bomb (lower Norfair above fire flea room)",
                    items => items.CanPassBombPassages()),
                new Location(this, 76, 0xC790C0, LocationType.Visible, "Power Bomb (Power Bombs of shame)",
                    items => items.CanUsePowerBombs()),
                new Location(this, 77, 0xC79100, LocationType.Visible, "Missile (lower Norfair near Wave Beam)",
                    items => items.Morph),
                new Location(this, 78, 0xC79108, LocationType.Hidden, "Energy Tank, Ridley",
                    items => (!Config.Keysanity || items.RidleyKey) && items.CanUsePowerBombs() && items.Super &&
                        (items.Charge || Logic.SoftlockRisk && CanBeatRidley(items))),
                new Location(this, 80, 0xC79184, LocationType.Visible, "Energy Tank, Firefleas",
                    items => items.Morph || items.Super),
            };
        }

        bool CanBeatRidley(Progression items) {
            return items.Supers * 6 + items.PowerBombs * 2 + items.Missiles >= 36;
        }

        public override bool CanEnter(Progression items) {
            // Additional damage implies possible escape through Amphitheater
            return (Logic.AdditionalDamage || items.HasEnergyCapacity(5)) && (
                World.CanEnter<NorfairUpperEast>(items) && items.CanUsePowerBombs() &&
                    (Logic.SuitlessLava ? (items.HiJump || items.Gravity) : items.SpaceJump && items.Gravity) ||
                items.CanAccessNorfairLowerPortal() && items.CanDestroyBombWalls() && (Logic != Casual || items.CanUsePowerBombs()) &&
                    (items.CanFly() || Logic.SpringBallJump && items.CanSpringBallJump() || Logic.ShortCharge && items.SpeedBooster) && items.Super
            ) && (
                // Worst Room, destroy bomb blocks and get up (and dodging pirates)
                items.CanFly() || Logic.TrickyWallJump && items.HiJump || Logic.SpringBallJump && items.CanSpringBallJump() ||
                Logic.GuidedEnemyFreeze && items.Charge && items.Ice
            );
        }

        public bool CanComplete(Progression items) {
            return Locations.Get("Energy Tank, Ridley").Available(items);
        }

    }

}
