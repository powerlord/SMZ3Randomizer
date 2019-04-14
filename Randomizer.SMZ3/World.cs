using System;
using System.Collections.Generic;
using System.Linq;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3 {

    class World {

        public List<Location> Locations { get; }
        public List<Region> Regions { get; }
        public Config Config { get; set; }
        public string Player { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }

        public IEnumerable<Item> Items => Locations.Select(l => l.Item).Where(i => i != null);

        /* Let Regions be a list with predictable order, use this lookup internally */
        readonly IDictionary<Type, Region> regionsOfType;

        public World(Config config, string player, int id, string guid) {
            Config = config;
            Player = player;
            Id = id;
            Guid = guid;

            Regions = new List<Region> {
                new Regions.Zelda.CastleTower(this, Config),
                new Regions.Zelda.EasternPalace(this, Config),
                new Regions.Zelda.DesertPalace(this, Config),
                new Regions.Zelda.TowerOfHera(this, Config),
                new Regions.Zelda.PalaceOfDarkness(this, Config),
                new Regions.Zelda.SwampPalace(this, Config),
                new Regions.Zelda.SkullWoods(this, Config),
                new Regions.Zelda.ThievesTown(this, Config),
                new Regions.Zelda.IcePalace(this, Config),
                new Regions.Zelda.MiseryMire(this, Config),
                new Regions.Zelda.TurtleRock(this, Config),
                new Regions.Zelda.GanonTower(this, Config),
                new Regions.Zelda.LightWorldDeathMountainWest(this, Config),
                new Regions.Zelda.LightWorldDeathMountainEast(this, Config),
                new Regions.Zelda.LightWorldNorthWest(this, Config),
                new Regions.Zelda.LightWorldNorthEast(this, Config),
                new Regions.Zelda.LightWorldSouth(this, Config),
                new Regions.Zelda.HyruleCastle(this, Config),
                new Regions.Zelda.DarkWorldDeathMountainWest(this, Config),
                new Regions.Zelda.DarkWorldDeathMountainEast(this, Config),
                new Regions.Zelda.DarkWorldNorthWest(this, Config),
                new Regions.Zelda.DarkWorldNorthEast(this, Config),
                new Regions.Zelda.DarkWorldSouth(this, Config),
                new Regions.Zelda.DarkWorldMire(this, Config),
                new Regions.SuperMetroid.CrateriaCentral(this, Config),
                new Regions.SuperMetroid.CrateriaWest(this, Config),
                new Regions.SuperMetroid.CrateriaEast(this, Config),
                new Regions.SuperMetroid.BrinstarBlue(this, Config),
                new Regions.SuperMetroid.BrinstarGreen(this, Config),
                new Regions.SuperMetroid.BrinstarKraid(this, Config),
                new Regions.SuperMetroid.BrinstarPink(this, Config),
                new Regions.SuperMetroid.BrinstarRed(this, Config),
                new Regions.SuperMetroid.MaridiaOuter(this, Config),
                new Regions.SuperMetroid.MaridiaInner(this, Config),
                new Regions.SuperMetroid.NorfairUpperWest(this, Config),
                new Regions.SuperMetroid.NorfairUpperEast(this, Config),
                new Regions.SuperMetroid.NorfairUpperCrocomire(this, Config),
                new Regions.SuperMetroid.NorfairLowerWest(this, Config),
                new Regions.SuperMetroid.NorfairLowerEast(this, Config),
                new Regions.SuperMetroid.WreckedShip(this, Config)
            };

            Locations = Regions.SelectMany(x => x.Locations).ToList();
            regionsOfType = Regions.ToDictionary(k => k.GetType(), v => v);
        }

        public TRegion Region<TRegion>() where TRegion : Region {
            if (regionsOfType.TryGetValue(typeof(TRegion), out var region))
                return region as TRegion;
            throw new ArgumentException($"Invalid region type {typeof(TRegion).Name}");
        }

        public bool CanEnter<TRegion>(Progression items) where TRegion : Region {
            return Region<TRegion>().CanEnter(items);
        }

        public bool CanAquire(Progression items, RewardType reward) {
            return Regions.OfType<IReward>().First(x => reward == x.Reward).CanComplete(items);
        }

        public bool CanAquireAll(Progression items, params RewardType[] rewards) {
            return Regions.OfType<IReward>().Where(x => rewards.Contains(x.Reward)).All(x => x.CanComplete(items));
        }

        public void Setup(Random rnd) {
            SetMedallions(rnd);
            SetRewards(rnd);
        }

        private void SetMedallions(Random rnd) {
            foreach (var region in Regions.OfType<IMedallionAccess>()) {
                region.Medallion = rnd.Next(3) switch {
                    0 => ItemType.Bombos,
                    1 => ItemType.Ether,
                    _ => ItemType.Quake,
                };
            }
        }

        private void SetRewards(Random rnd) {
            var rewards = new[] {
                PendantGreen, PendantNonGreen, PendantNonGreen, CrystalRed, CrystalRed,
                CrystalBlue, CrystalBlue, CrystalBlue, CrystalBlue, CrystalBlue }.Shuffle(rnd);
            foreach (var region in Regions.OfType<IReward>().Where(x => x.Reward == None)) {
                region.Reward = rewards.First();
                rewards.Remove(region.Reward);
            }
        }

    }

}
