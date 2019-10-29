﻿using System.Collections.Generic;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Regions.Zelda {

    class DarkWorldSouth : Z3Region {

        public override string Name => "Dark World South";
        public override string Area => "Dark World";

        public DarkWorldSouth(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 256+82, 0x180148, LocationType.Regular, "Digging Game"),
                new Location(this, 256+83, 0x330C7, LocationType.Regular, "Stumpy"),
                new Location(this, 256+84, 0xEB1E, LocationType.Regular, "Hype Cave - Top"),
                new Location(this, 256+85, 0xEB21, LocationType.Regular, "Hype Cave - Middle Right"),
                new Location(this, 256+86, 0xEB24, LocationType.Regular, "Hype Cave - Middle Left"),
                new Location(this, 256+87, 0xEB27, LocationType.Regular, "Hype Cave - Bottom"),
                new Location(this, 256+88, 0x180011, LocationType.Regular, "Hype Cave - NPC"),
            };
        }

        public override bool CanEnter(Progression items) {
            return items.MoonPearl && ((
                    World.CanAquire(items, Agahnim) ||
                    items.CanAccessDarkLakeHyliaPortal(Config) && items.Flippers
                ) && (items.Hammer || items.Hookshot && (items.Flippers || items.CanLiftLight())) ||
                items.Hammer && items.CanLiftLight() ||
                items.CanLiftHeavy()
            );
        }

    }

}
