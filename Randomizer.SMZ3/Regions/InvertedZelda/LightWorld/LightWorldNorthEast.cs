using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class LightWorldNorthEast : Zelda.LightWorldNorthEast {

        public LightWorldNorthEast(World world, Config config) : base(world, config) {
            //King Zora
            //    canOneFrameClipOW ||
            //    (
            //        MoonPearl
            //        || canOWYBA && hasABottle
            //    )
            //    && (
            //        canLiftRocks
            //        || canFakeFlipper || Flippers
            //        || (canBootsClip || canWaterWalk) && PegasusBoots
            //        || canSuperSpeed && canSpinSpeed && LightWorldDeathMountainEast
            //    )
            Location("King Zora").CanAccess(items =>
                Logic.OneFrameClipOw || (
                    items.MoonPearl ||
                    Logic.OwYba && items.Bottle
                ) && (
                    items.CanLiftLight() ||
                    Logic.FakeFlipper || items.Flippers ||
                    (Logic.BootsClip || Logic.WaterWalk) && items.Boots ||
                    Logic.SuperSpeed && items.CanSpinSpeed() && World.CanEnter<LightWorldDeathMountainEast>(items)
                ));
            //Zora's Ledge
            //    (
            //        MoonPearl
            //        || canBunnyRevive && canBunnyRevive
            //        || canOWYBA && hasABottle
            //    ) && (
            //        Flippers ||
            //        canWaterWalk && (
            //            canFakeFlipper && canWaterWalk && MoonPearl && canOneFrameClipOW ||
            //            PegasusBoots && (
            //                canBootsClip
            //                || canOneFrameClipOW
            //                || canSuperSpeed && canSpinSpeed
            //            )
            //        )
            //    )
            Location("Zora's Ledge").CanAccess(items => (
                items.MoonPearl ||
                Logic.OwYba && items.Bottle ||
                Logic.BunnyRevive && items.CanBunnyRevive()
            ) && (
                items.Flippers ||
                Logic.WaterWalk && (
                    Logic.OneFrameClipOw && Logic.FakeFlipper && items.MoonPearl ||
                    items.Boots && (
                        Logic.OneFrameClipOw ||
                        Logic.BootsClip ||
                        Logic.SuperSpeed && items.CanSpinSpeed()
                    )
                )
            ));
            Location("Waterfall Fairy - Left").CanAccess(CanReachWaterfallFairy);
            Location("Waterfall Fairy - Right").CanAccess(CanReachWaterfallFairy);
            //Potion Shop
            //    (
            //        MoonPearl
            //        || canOWYBA && hasABottle
            //        || canBunnyRevive && canBunnyRevive
            //        || canOneFrameClipOW
            //    )
            //        && Mushroom;
            Location("Potion Shop").CanAccess(items => (
                items.MoonPearl ||
                Logic.OneFrameClipOw ||
                Logic.OwYba && items.Bottle ||
                Logic.BunnyRevive && items.CanBunnyRevive()
            ) && items.Mushroom);
            Location("Sahasrahla's Hut - Left").CanAccess(CanOpenWallCracks);
            Location("Sahasrahla's Hut - Middle").CanAccess(CanOpenWallCracks);
            Location("Sahasrahla's Hut - Right").CanAccess(CanOpenWallCracks);

            foreach (var location in Locations) {
                location.Weighted(null);
            }
        }

        //Waterfall Fairy - Left
        //Waterfall Fairy - Right
        //    (
        //        MoonPearl
        //        || canBunnyRevive && canBunnyRevive
        //        || canOWYBA && hasABottle
        //    ) && (
        //        Flippers
        //        || canWaterWalk && PegasusBoots
        //        || canFakeFlipper && MoonPearl
        //        || LightWorldDeathMountainEast && (
        //            canOneFrameClipOW
        //            || canBootsClip && PegasusBoots
        //            || canSuperSpeed && canSpinSpeed
        //        )
        //    )

        /* Assume converting FakeFlipper to WaterWalk is trivial, don't guard with a WaterWalk check */
        bool CanReachWaterfallFairy(Progression items) {
            return (
                items.MoonPearl ||
                Logic.OwYba && items.Bottle ||
                Logic.BunnyRevive && items.CanBunnyRevive()
            ) && (
                items.Flippers ||
                Logic.WaterWalk && items.Boots ||
                Logic.FakeFlipper && items.MoonPearl ||
                World.CanEnter<LightWorldDeathMountainEast>(items) && (
                    Logic.OneFrameClipOw ||
                    Logic.BootsClip && items.Boots ||
                    Logic.SuperSpeed && items.CanSpinSpeed()
                )
            );
        }

        //Sahasrahla's Hut - Left
        //Sahasrahla's Hut - Middle
        //Sahasrahla's Hut - Right
        //    MoonPearl
        //    || canOWYBA && hasABottle
        //    || canSuperBunny && PegasusBoots
        // Todo: helper method naming
        bool CanOpenWallCracks(Progression items) {
            return items.MoonPearl ||
                Logic.OwYba && items.Bottle ||
                Logic.SuperBunny && items.Boots;
        }

        //can_enter
        //    canOneFrameClipOW
        //    || DefeatAgahnim
        //    || MoonPearl && (canLiftDarkRocks || Hammer && canLiftRocks)
        //    || canOWYBA && (TwoBottles || Bottle && Lamp)
        //    || LightWorldDeathMountainWest && (MoonPearl || MagicMirror) && (
        //        canBootsClip && PegasusBoots
        //        || canSuperSpeed && canSpinSpeed
        //    )
        public override bool CanEnter(Progression items) {
            return Logic.OneFrameClipOw ||
                World.CanAquire(items, Agahnim) ||
                items.MoonPearl && (
                    items.CanLiftHeavy() ||
                    items.Hammer && items.CanLiftLight()
                ) ||
                Logic.OwYba && (
                    items.TwoBottles ||
                    items.Bottle && items.Lamp
                ) ||
                World.CanEnter<LightWorldDeathMountainWest>(items) && (items.MoonPearl || items.Mirror) && (
                    Logic.BootsClip && items.Boots ||
                    Logic.SuperSpeed && items.CanSpinSpeed()
                );
        }

    }

}
