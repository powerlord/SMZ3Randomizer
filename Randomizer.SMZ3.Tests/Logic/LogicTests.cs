using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Randomizer.SMZ3.Regions.Zelda;
using static Randomizer.SMZ3.RewardType;

namespace Randomizer.SMZ3.Tests.Logic {

    [TestFixture]
    public partial class LogicTests {

        World world;

        [SetUp]
        public void Setup() {
            world = new World(new Config(new Dictionary<string, string>()), "", 0, "");
            (world.Regions.Single(x => x.Name == "Misery Mire") as MiseryMire).Medallion = ItemType.Ether;
            (world.Regions.Single(x => x.Name == "Turtle Rock") as TurtleRock).Medallion = ItemType.Quake;
            /* We cheat with non-Aga1 rewards by virtue of All() being true for an empty collection */
            foreach (var region in world.Regions.OfType<IReward>()) {
                region.Reward = region.Reward == Agahnim ? Agahnim : None;
            }
        }

        [TestCaseSource("ZeldaOverworld")]
        [TestCaseSource("ZeldaDungeons")]
        [TestCaseSource("Metroid")]
        public void CanAccessLocation(string name, IEnumerable<Instruction> instructions) {
            var location = world.Locations.Get(name);

            var inventory = Execute(instructions, world, name);
            Assert.That(location.Available(inventory), Is.True);

            // Todo: Enable this when the node based system is implemented.
            // Does not work since locations can logically shortcut CanEnter logic
            /*foreach (var index in Enumerable.Range(0, instructions.Count())) {
                inventory = Execute(instructions, world, name, index);
                var msg = instructions.ElementAt(index).DescribeBeingSkipped();
                Assert.That(location.Available(inventory), Is.False, msg);
            }*/
        }

        Progression Execute(IEnumerable<Instruction> instructions, World world, string name, int? skip = null) {
            var pool = new List<Item>();
            foreach (var (instruction, index) in instructions.Select((x, i) => (x, i))) {
                instruction.Execute(world, name, pool, skipOne: skip == index);
            }
            return new Progression(pool);
        }

        public static IEnumerable<TestCaseData> ZeldaOverworld() {
            return LogicTestCases("Logic.Normal.ZeldaOverworld.yaml");
        }

        public static IEnumerable<TestCaseData> ZeldaDungeons() {
            return LogicTestCases("Logic.Normal.ZeldaDungeons.yaml");
        }

        public static IEnumerable<TestCaseData> Metroid() {
            return LogicTestCases("Logic.Normal.Metroid.yaml");
        }

    }

}
