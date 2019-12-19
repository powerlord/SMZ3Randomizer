using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class LightWorldNorthWest : Zelda.LightWorldNorthWest {

        public LightWorldNorthWest(World world, Config config) : base(world, config) {
            Location("Mushroom").CanAccess(CanBeLinkInLightWorld);
            Location("Lost Woods Hideout").CanAccess(CanBeLinkInLightWorld);
            //Lumberjack Tree
            //    Agahnim && (
            //        canOneFrameClipOW
            //        || canMirrorWrap && MagicMirror
            //        || PegasusBoots && (
            //            MoonPearl
            //            || canBunnyRevive && canBunnyRevive
            //            || canOWYBA && hasABottle
            //        )
            //    )
            Location("Lumberjack Tree").CanAccess(items =>
                World.CanAquire(items, Agahnim) && (
                    Logic.OneFrameClipOw ||
                    Logic.MirrorWrap && items.Mirror ||
                    CanBeLinkInLightWorld(items) && items.Boots
                ));
            //Pegasus Rocks
            //    PegasusBoots && (
            //        MoonPearl
            //        || canOWYBA && hasABottle
            //        || canBunnyRevive && canBunnyRevive
            //    )
            Location("Pegasus Rocks").CanAccess(items =>
                CanBeLinkInLightWorld(items) && items.Boots);
            Location("Graveyard Ledge").CanAccess(CanBeLinkInLightWorld);
            //King's Tomb
            //    PegasusBoots && (
            //        MoonPearl
            //        || canBunnyRevive && canBunnyRevive
            //        || canOWYBA && Bottle
            //    ) && (
            //        canLiftDarkRocks
            //        || canMirrorClip && MagicMirror && MoonPearl && canBootsClip && PegasusBoots
            //        || canOneFrameClipOW
            //        || LightWorldDeathMountainEast &&
            //            canSuperSpeed && canSpinSpeed &&
            //            (MoonPearl || canOWYBA && TwoBottles)
            //    )
            Location("King's Tomb").CanAccess(items =>
                items.Boots && CanBeLinkInLightWorld(items) && (
                    items.CanLiftHeavy() ||
                    Logic.OneFrameClipOw ||
                    Logic.BootsClip && Logic.MirrorClip && items.Mirror && items.MoonPearl ||
                    Logic.SuperSpeed && items.CanSpinSpeed() && World.CanEnter<LightWorldDeathMountainEast>(items) && (
                        items.MoonPearl ||
                        Logic.OwYba && items.TwoBottles
                    )
                ));
            // Todo: helper method naming
            //Kakariko Well - Top
            //    CanBeLinkInLightWorld
            //Kakariko Well - Left
            //Kakariko Well - Middle
            //Kakariko Well - Right
            //Kakariko Well - Bottom
            //    canSuperBunny
            //    || CanBeLinkInLightWorld
            Location("Kakariko Well - Top").CanAccess(CanBeLinkInLightWorld);
            Location("Kakariko Well - Left").CanAccess(CanBeLinkOrSuperBunnyInLightWorld);
            Location("Kakariko Well - Middle").CanAccess(CanBeLinkOrSuperBunnyInLightWorld);
            Location("Kakariko Well - Right").CanAccess(CanBeLinkOrSuperBunnyInLightWorld);
            Location("Kakariko Well - Bottom").CanAccess(CanBeLinkOrSuperBunnyInLightWorld);
            //Blind's Hideout - Top
            //    CanBeLinkInLightWorld
            //Blind's Hideout - Left
            //Blind's Hideout - Right
            //Blind's Hideout - Far Left
            //Blind's Hideout - Far Right
            //    canSuperBunny && MagicMirror
            //    || CanBeLinkInLightWorld
            Location("Blind's Hideout - Top").CanAccess(CanBeLinkInLightWorld);
            Location("Blind's Hideout - Far Left").CanAccess(CanSolveBlockPushPuzzle);
            Location("Blind's Hideout - Left").CanAccess(CanSolveBlockPushPuzzle);
            Location("Blind's Hideout - Right").CanAccess(CanSolveBlockPushPuzzle);
            Location("Blind's Hideout - Far Right").CanAccess(CanSolveBlockPushPuzzle);
            Location("Chicken House").CanAccess(CanBeLinkInLightWorld);
            //Kakariko Tavern
            //    canSuperBunny
            //    || CanBeLinkInLightWorld
            Location("Kakariko Tavern").CanAccess(CanBeLinkOrSuperBunnyInLightWorld);
            //Magic Bat
            //    Powder && (
            //        MoonPearl
            //        || canOWYBA && hasABottle
            //        || canBunnyRevive && canBunnyRevive
            //    ) && (
            //        Hammer ||
            //        (canFakeFlipper || Flippers) && (
            //            canOneFrameClipOW ||
            //            canBootsClip && PegasusBoots
            //        )
            //    )
            Location("Magic Bat").CanAccess(items =>
                 CanBeLinkInLightWorld(items) && items.Powder && (
                    items.Hammer ||
                    (Logic.FakeFlipper || items.Flippers) && (
                        Logic.OneFrameClipOw ||
                        Logic.BootsClip && items.Boots
                    )
                ));

            foreach (var location in Locations) {
                location.Weighted(null);
            }
        }

        bool CanSolveBlockPushPuzzle(Progression items) {
            return CanBeLinkInLightWorld(items) ||
                Logic.SuperBunny && items.Mirror;
        }

        bool CanBeLinkOrSuperBunnyInLightWorld(Progression items) {
            return CanBeLinkInLightWorld(items) ||
                Logic.SuperBunny;
        }

        bool CanBeLinkInLightWorld(Progression items) {
            return
                items.MoonPearl ||
                Logic.OwYba && items.Bottle ||
                Logic.BunnyRevive && items.CanBunnyRevive();
        }

        public override bool CanEnter(Progression items) {
            return World.CanEnter<LightWorldNorthEast>(items);
        }

    }

}
