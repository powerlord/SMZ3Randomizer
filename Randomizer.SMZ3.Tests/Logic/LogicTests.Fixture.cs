using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using SharpYaml.Model;
using static System.Linq.Enumerable;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Tests.Logic {

    public partial class LogicTests {

        static IEnumerable<TestCaseData> LogicTestCases(string path) {
            using var stream = EmbeddedStream.For(path);
            using var reader = new StreamReader(stream);

            var nothing = Repeat("", 1);
            foreach (var region in ExtractData(reader)) {
                foreach (var location in region.Locations) {
                    var instructions = from x in region.Enter ?? nothing
                                       from y in location.Access ?? nothing
                                       select $"{x} {y}".Trim();
                    foreach (var instruction in instructions) {
                        var list = Normalize(ParseInstruction(instruction));
                        yield return new TestCaseData(location.Name, list)
                            .SetName($"{{m}}({{0}},\"{string.Join(" ", list)}\")");
                    }
                }
            }
        }

        static IEnumerable<RegionTestData> ExtractData(StreamReader reader) {
            var root = YamlStream.Load(reader).First().Contents;
            return from region in root as YamlMapping
                   select new RegionTestData {
                       Name = (region.Key as YamlValue).Value,
                       Enter = SeqOrNull((region.Value as YamlMapping)["Enter"]),
                       Locations = from location in (region.Value as YamlMapping)["Locations"] as YamlMapping
                                   select new LocationTestData {
                                       Name = (location.Key as YamlValue).Value,
                                       Access = SeqOrNull(location.Value)
                                   }
                   };

            static IEnumerable<string> SeqOrNull(YamlElement element) {
                return element is YamlSequence seq ? from x in seq select (x as YamlValue).Value : null;
            }
        }

        class RegionTestData {
            public string Name { get; set; }
            public IEnumerable<string> Enter { get; set; }
            public IEnumerable<LocationTestData> Locations { get; set; }
        }

        class LocationTestData {
            public string Name { get; set; }
            public IEnumerable<string> Access { get; set; }
        }

        static IEnumerable<Instruction> ParseInstruction(string text) {
            var pattern = new Regex(@"(?<has>Has(?:In ""(?<in>[^""]+)"" )?)?(?<type>\w+)(?: (?<count>\d+))? *");
            return pattern.Matches(text).Select(match => match switch {
                _ when match.Groups["has"].Success => new Instruction {
                    Has = true,
                    Type = match.Groups["type"].Value,
                    In = match.Groups["in"].Success ? match.Groups["in"].Value : null
                },
                _ => new Instruction {
                    Req = true,
                    Type = match.Groups["type"].Value,
                    Count = match.Groups["count"].Success ? int.Parse(match.Groups["count"].Value) : 1
                },
            });
        }

        static IEnumerable<Instruction> Normalize(IEnumerable<Instruction> instructions) {
            var present = instructions.Aggregate(
                (Mitt: false, MasterSword: false, TwoPowerBombs: false),
                (a, x) => (
                    a.Mitt || x.Req && x.Type == "Mitt",
                    a.MasterSword || x.Req && x.Type == "MasterSword",
                    a.TwoPowerBombs || x.Req && x.Type == "TwoPowerBombs"
                )
            );

            return instructions.Distinct().Where(x => x.Has ||
                (x.Type != "Glove" || !present.Mitt) &&
                (x.Type != "Sword" || !present.MasterSword) &&
                (x.Type != "PowerBomb" || !present.TwoPowerBombs)
            );
        }

        public class Instruction : IEquatable<Instruction> {

            public bool Req { get; set; } = false;
            public int Count { get; set; } = 1;
            public bool Has { get; set; } = false;
            public string In { get; set; } = null;
            public string Type { get; set; }

            // Need internal here since World and Item are private
            internal void Execute(World world, string location, List<Item> pool, bool skipOne = false) {
                if (Has) {
                    var type = Enum.Parse<ItemType>(Type);
                    world.Locations.Get(In ?? location).Item = skipOne ? null : new Item { Type = type, World = world };
                }
                if (Req) {
                    pool.AddRange(
                        from type in Type switch {
                            _ when Type == "Glove" => Repeat(ProgressiveGlove, skipOne ? 0 : 1),
                            _ when Type == "Mitt" => Repeat(ProgressiveGlove, skipOne ? 1 : 2),
                            _ when Type == "Sword" => Repeat(ProgressiveSword, skipOne ? 0 : 1),
                            _ when Type == "MasterSword" => Repeat(ProgressiveSword, skipOne ? 1 : 2),
                            _ when Type == "MirrorShield" => Repeat(ProgressiveShield, skipOne ? 0 : 3),
                            _ when Type == "TwoPowerBombs" => Repeat(PowerBomb, skipOne ? 1 : 2),
                            _ => Repeat(Enum.Parse<ItemType>(Type), skipOne ? Count - 1 : Count),
                        }
                        select new Item { Type = type, World = world }
                    );
                }
            }

            public string DescribeBeingSkipped() {
                return Has ? $"Without a {Type} at {(In is null ? "this location" : $"\"{In}\"")}"
                    : Type == "Mitt" ? "With only Glove"
                    : Type == "MasterSword" ? "With only Sword"
                    : Type == "TwoPowerBombs" ? "With only one PB pack"
                    : Count > 1 ? $"With only {Count - 1} {Type}"
                    : $"Without {Type}";
            }

            public bool Equals(Instruction o) {
                return o is null ? false :
                    ReferenceEquals(this, o) ||
                    Type == o.Type &&
                    Req == o.Req &&
                    Count == o.Count &&
                    Has == o.Has &&
                    In == o.In;
            }

            #region object equals and hashcode

            public override bool Equals(object obj) {
                return Equals(obj as Instruction);
            }

            public override int GetHashCode() {
                return HashCode.Combine(Type, Req, Count, Has, In);
            }

            public static bool operator==(Instruction a, Instruction b) {
                return a is null || b is null ? Equals(a, b) : a.Equals(b);
            }

            public static bool operator!=(Instruction a, Instruction b) {
                return !(a == b);
            }

            #endregion

            public override string ToString() {
                var startPattern = new Regex(@"^(Key|ETank)");
                return Req
                    ? $"{Type}{(Count > 1 || startPattern.IsMatch(Type) ? $" {Count}" : "")}"
                    : In is null ? $"Has{Type}" : $"HasIn \"{In}\" {Type}";
            }

        }

    }

}
