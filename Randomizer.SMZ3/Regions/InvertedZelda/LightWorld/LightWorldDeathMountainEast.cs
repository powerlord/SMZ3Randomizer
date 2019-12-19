namespace Randomizer.SMZ3.Regions.InvertedZelda {

    class LightWorldDeathMountainEast : Zelda.LightWorldDeathMountainEast {

        public LightWorldDeathMountainEast(World world, Config config) : base(world, config) {
            // Todo: the location id and address is repeated here. can be avoided if associated with names in Patch
            Locations.AddRange(new[] {
                new Location(this, 256 + 0, 0x180016, LocationType.Ether, "Ether Tablet",
                    items =>
                        items.Book && items.MasterSword && (
                            Logic.OneFrameClipOw ||
                            items.MoonPearl && (
                                items.Hammer ||
                                Logic.BootsClip && items.Boots ||
                                Logic.SuperSpeed && items.CanSpinSpeed()
                            )
                        )),
                new Location(this, 256 + 1, 0x180140, LocationType.Regular, "Spectacle Rock",
                    items =>
                        Logic.OneFrameClipOw ||
                        Logic.OwYba && Logic.BootsClip && items.Bottle && items.Boots ||
                        items.MoonPearl && (
                            items.Hammer ||
                            Logic.BootsClip && items.Boots ||
                            Logic.SuperSpeed && items.CanSpinSpeed()
                        )),
            });

            Location("Floating Island").CanAccess();
            Location("Spiral Cave").CanAccess(items =>
                CanSomehowBeLink(items) ||
                Logic.SuperBunny && items.Mirror && items.Sword);
            Location("Paradox Cave Upper - Left").CanAccess(CanSomehowBeLink);
            Location("Paradox Cave Upper - Right").CanAccess(CanSomehowBeLink);
            Location("Paradox Cave Lower - Far Left").CanAccess(CanSomehowBeLink);
            Location("Paradox Cave Lower - Left").CanAccess(CanSomehowBeLink);
            Location("Paradox Cave Lower - Middle").CanAccess(CanSomehowBeLink);
            Location("Paradox Cave Lower - Right").CanAccess(CanSomehowBeLink);
            Location("Paradox Cave Lower - Far Right").CanAccess(CanSomehowBeLink);
            Location("Mimic Cave").CanAccess(items =>
                items.Hammer && (
                    items.MoonPearl ||
                    Logic.OwYba && items.Bottle && (
                        Logic.OneFrameClipOw ||
                        Logic.BootsClip && items.Boots
                    )
                ));
        }

        // Todo: helper method naming
        bool CanSomehowBeLink(Progression items) {
            return
                items.MoonPearl ||
                Logic.OwYba && (
                    items.Bottle && (
                        Logic.OneFrameClipOw ||
                        Logic.BootsClip && items.Boots
                    ) ||
                    items.TwoBottles && (
                        items.Hookshot ||
                        Logic.SuperSpeed && items.CanSpinSpeed()
                    )
                );
        }

        public override bool CanEnter(Progression items) {
            return
                items.CanLiftHeavy() && World.CanEnter<DarkWorldDeathMountainEast>(items) ||
                World.CanEnter<LightWorldDeathMountainWest>(items) && (
                    Logic.OneFrameClipOw ||
                    Logic.MirrorWrap && items.Mirror || (
                        items.MoonPearl ||
                        Logic.OwYba && items.TwoBottles
                    ) && (
                        items.Hookshot ||
                        Logic.BootsClip && items.Boots ||
                        Logic.SuperSpeed && items.CanSpinSpeed()
                    )
                );
        }

    }

}
