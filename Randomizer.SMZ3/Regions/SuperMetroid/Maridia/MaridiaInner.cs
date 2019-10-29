using System.Collections.Generic;

namespace Randomizer.SMZ3.Regions.SuperMetroid {

    class MaridiaInner : SMRegion, IReward {

        public override string Name => "Maridia Inner";
        public override string Area => "Maridia";

        public RewardType Reward { get; set; } = RewardType.GoldenFourBoss;

        public MaridiaInner(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 140, 0xC7C4AF, LocationType.Visible, "Super Missile (yellow Maridia)",
                    items => (items.CanPassBombPassages() || items.CanSpringBallJump()) && (items.Gravity ||
                        Logic.Suitless && items.HiJump && (items.Ice || Logic.SpringBallJump && items.CanSpringBallJump()))),
                new Location(this, 141, 0xC7C4B5, LocationType.Visible, "Missile (yellow Maridia super missile)",
                    items => (items.CanPassBombPassages() || items.CanSpringBallJump()) && (items.Gravity ||
                        Logic.Suitless && items.HiJump && (items.Ice || Logic.SpringBallJump && items.CanSpringBallJump()))),
                new Location(this, 142, 0xC7C533, LocationType.Visible, "Missile (yellow Maridia false wall)",
                    items => items.Gravity || Logic.Suitless && items.HiJump && (items.Ice || Logic.SpringBallJump && items.CanSpringBallJump())),
                new Location(this, 143, 0xC7C559, LocationType.Chozo, "Plasma Beam",
                    items => CanDefeatDraygon(items) && (
                        items.ScrewAttack || items.Plasma ||
                        /*Logic.Something && */items.SpeedBooster ||
                        Logic.PseudoScrew && items.Charge && items.HasEnergyReserves(3)
                    ) && (
                        items.HiJump || items.CanFly() ||
                        /*Logic.Something && */(items.SpeedBooster || items.CanSpringBallJump())
                    )),
                // Todo: Define logic for entry into left sand pit on Advanced
                new Location(this, 144, 0xC7C5DD, LocationType.Visible, "Missile (left Maridia sand pit room)",
                    LeftSandPit),
                new Location(this, 145, 0xC7C5E3, LocationType.Chozo, "Reserve Tank, Maridia",
                    LeftSandPit),
                new Location(this, 146, 0xC7C5EB, LocationType.Visible, "Missile (right Maridia sand pit room)",
                    items => items.Gravity || Logic.Suitless && items.HiJump),
                new Location(this, 147, 0xC7C5F1, LocationType.Visible, "Power Bomb (right Maridia sand pit room)",
                    items => items.Gravity || Logic.SpringBallJump && items.CanSpringBallJump() && items.HiJump),
                new Location(this, 148, 0xC7C603, LocationType.Visible, "Missile (pink Maridia)",
                    items => items.Gravity && (Logic.SnailClip || items.SpeedBooster)),
                new Location(this, 149, 0xC7C609, LocationType.Visible, "Super Missile (pink Maridia)",
                    items => items.Gravity && (Logic.SnailClip || items.SpeedBooster)),
                new Location(this, 150, 0xC7C6E5, LocationType.Chozo, "Spring Ball",
                items => items.CanUsePowerBombs() && (
                    items.Grapple && (Logic.ClimbCwj ?
                        items.SpaceJump && items.HiJump :
                        items.Gravity && (items.SpaceJump || items.HiJump)
                    ) ||
                    Logic.IceClip && items.Gravity && items.Ice
                ) &&
                Logic.SpringBallJump && items.CanSpringBallJump() && items.HiJump),
                new Location(this, 151, 0xC7C74D, LocationType.Hidden, "Missile (Draygon)",
                    CanDefeatBotwoon),
                new Location(this, 152, 0xC7C755, LocationType.Visible, "Energy Tank, Botwoon", 
                    CanDefeatBotwoon),
                // Todo: logic for getting there?
                new Location(this, 154, 0xC7C7A7, LocationType.Chozo, "Space Jump",
                    items => CanDefeatDraygon(items)),
            };
        }

        bool LeftSandPit(Progression items) {
            return (
                    items.Gravity && (Logic.Suitless || items.HiJump) ||
                    /*Logic.Something && */items.HiJump && (items.SpaceJump || items.CanSpringBallJump())
                ) &&
                (items.CanPassBombPassages() || items.CanSpringBallJump() && items.HiJump);
        }

        bool CanDefeatBotwoon(Progression items) {
            return Logic.Suitless && items.Ice || items.SpeedBooster || items.CanAccessMaridiaPortal(World);
        }

        bool CanDefeatDraygon(Progression items) {
            return items.Gravity &&
                (Logic.Suitless || items.SpeedBooster && items.HiJump || items.CanFly()) ||
                Logic.SpringBallJump && items.CanSpringBallJump() && items.Grapple;
        }

        // Todo: figure out Springball Jumps category names based on logic
        public override bool CanEnter(Progression items) {
            return (Logic.Suitless || items.Gravity) && (
                World.CanEnter<NorfairUpperWest>(items) && items.CanUsePowerBombs() && (!Logic.Suitless ?
                    (items.CanFly() || items.SpeedBooster || items.Grapple) :
                    (items.Gravity || items.HiJump && (items.Ice || Logic.SpringBallJump && items.CanSpringBallJump()) && items.Grapple)
                ) ||
                items.CanAccessMaridiaPortal(World)
            );

            //(items.CanFly() || items.SpeedBooster || items.Grapple)
            //(items.Gravity || items.HiJump && items.Ice && items.Grapple)
            //(items.Gravity || items.HiJump && (items.Ice || items.CanSpringBallJump()) && items.Grapple)
        }

        public bool CanComplete(Progression items) {
            return Locations.Get("Space Jump").Available(items);
        }

    }

}
