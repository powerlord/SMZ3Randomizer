using System.Linq;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class TurtleRock : Zelda.TurtleRock {

        public TurtleRock(World world, Config config) : base(world, config) {
            Locations.Get("Turtle Rock - Compass Chest").CanAccess(items =>
                items.Somaria && ReachLobbyTransit(items));
            Locations.Get("Turtle Rock - Roller Room - Left").CanAccess(items =>
                items.Firerod && items.Somaria && ReachLobbyTransit(items));
            Locations.Get("Turtle Rock - Roller Room - Right").CanAccess(items =>
                items.Firerod && items.Somaria && ReachLobbyTransit(items));
            Locations.Get("Turtle Rock - Chain Chomps").CanAccess(items =>
                EnterTop(items) && items.KeyTR >= 1 ||
                EnterCaves(items));
            Locations.Get("Turtle Rock - Big Key Chest").CanAccess(items =>
                items.KeyTR >= 2);
            Locations.Get("Turtle Rock - Big Chest").CanAccess(items =>
                items.BigKeyTR && (
                    EnterTop(items) && items.KeyTR >= 2 ||
                    EnterCaves(items) && (items.Hookshot || items.Somaria)));
            Locations.Get("Turtle Rock - Crystaroller Room").CanAccess(items =>
                items.BigKeyTR && (EnterCaves(items) || EnterTop(items) && items.KeyTR >= 2) ||
                EnterCaves(items) && items.Lamp && items.Somaria);
            Locations.Get("Turtle Rock - Trinexx").CanAccess(items =>
                items.Somaria && items.BigKeyTR && items.KeyTR >= 4 && items.Lamp && CanBeatBoss(items));
        }

        bool ReachLobbyTransit(Progression items) {
            return EnterTop(items) || EnterCaves(items) && (
                items.KeyTR >= (new[] {
                    Locations.Get("Turtle Rock - Roller Room - Left"),
                    Locations.Get("Turtle Rock - Roller Room - Right")
                }.Any(l => l.ItemType == BigKeyTR) ? 2 : 4) ||
                items.Lamp && items.KeyTR >= 4);
        }

        protected override bool LaserBridge(Progression items) {
            return (
                EnterCaves(items) ||
                EnterTop(items) && items.Lamp && items.BigKeyTR && items.KeyTR >= 3
            ) && (items.Cape || items.Byrna || items.CanBlockLasers);
        }

        public override bool CanEnter(Progression items) {
            return EnterTop(items) || EnterCaves(items);
        }

        bool EnterTop(Progression items) {
            return Medallion switch {
                Bombos => items.Bombos,
                Ether => items.Ether,
                _ => items.Quake
            } && items.Sword &&
                items.Somaria && World.CanEnter("Dark World Death Mountain East", items);
        }

        bool EnterCaves(Progression items) {
            return items.Mirror && World.CanEnter("Light World Death Mountain East", items);
        }

    }

}
