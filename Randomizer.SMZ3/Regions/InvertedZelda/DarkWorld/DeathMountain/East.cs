using System;

namespace Randomizer.SMZ3.Regions.InvertedZelda.DarkWorld.DeathMountain {

    class East : Zelda.DarkWorld.DeathMountain.East {

        public East(World world, Config config) : base(world, config) {
            Locations.Get("Hookshot Cave - Top Right").CanAccess(items =>
                items.Hookshot && CaveAccess(items));
            Locations.Get("Hookshot Cave - Top Left").CanAccess(items =>
                items.Hookshot && CaveAccess(items));
            Locations.Get("Hookshot Cave - Bottom Left").CanAccess(items =>
                items.Hookshot && CaveAccess(items));
            Locations.Get("Hookshot Cave - Bottom Right").CanAccess(items =>
                (items.Hookshot || items.Boots) && CaveAccess(items));
            Locations.Get("Superbunny Cave - Top").CanAccess();
            Locations.Get("Superbunny Cave - Bottom").CanAccess();
        }

        bool CaveAccess(Progression items) {
            return items.CanLiftLight() || items.Mirror && World.CanEnter("Light World Death Mountain East", items);
        }

        public override bool CanEnter(Progression items) {
            return World.CanEnter("Dark World Death Mountain West", items) ||
                World.CanEnter("Light World Death Mountain East", items) && items.Mirror;
        }

    }

}
