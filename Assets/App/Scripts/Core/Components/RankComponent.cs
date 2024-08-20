using System;
using System.Collections.Generic;

namespace Kartishki.Core.Components
{
    /// <summary>
    /// Playing card component represents its rank
    /// </summary>
    [Serializable]
    public readonly struct RankComponent : IEquatable<RankComponent>, IComparable<RankComponent>
    {
        private static readonly Dictionary<string, int> CardDefaultRanks = new()
        {
            { "J", 11 },
            { "Q", 12 },
            { "K", 13 },
            { "A", 14 },
        };

        public static RankComponent MaxPossible = new(int.MaxValue, string.Empty); 

        public static RankComponent Invalid = new(-1, string.Empty);
        public static RankComponent Two => new(2, "2");
        public static RankComponent Three => new(3, "3");
        public static RankComponent Four => new(4, "4");
        public static RankComponent Five => new(5, "5");
        public static RankComponent Six => new(6, "6");
        public static RankComponent Seven => new(7, "7");
        public static RankComponent Eight => new(8, "8");
        public static RankComponent Nine => new(9, "9");
        public static RankComponent Ten => new(10, "10");
        public static RankComponent Jack => new(CardDefaultRanks["J"], "J");
        public static RankComponent Queen => new(CardDefaultRanks["Q"], "Q");
        public static RankComponent King => new(CardDefaultRanks["K"], "K");
        public static RankComponent Ace => new(CardDefaultRanks["A"], "A");

        public static IEnumerable<RankComponent> From6ToAce()
        {
            yield return Six;
            yield return Seven;
            yield return Eight;
            yield return Nine;
            yield return Ten;
            yield return Jack;
            yield return Queen;
            yield return King;
            yield return Ace;
        }
        
        public static IEnumerable<RankComponent> From2ToAce()
        {
            yield return Two;
            yield return Three;
            yield return Four;
            yield return Five;
            yield return Six;
            yield return Seven;
            yield return Eight;
            yield return Nine;
            yield return Ten;
            yield return Jack;
            yield return Queen;
            yield return King;
            yield return Ace;
        }

        public static IEnumerable<string> EnumerateStringValues()
        {
            for (var i = 2; i <= 10; i++)
            {
                yield return i.ToString();
            }

            yield return "J";
            yield return "Q";
            yield return "K";
            yield return "A";
        }

        internal static RankComponent Parse(string rankString)
        {
            if (int.TryParse(rankString, out var rankValue))
            {
                if (IsRankNumberValid(rankValue))
                {
                    return Create(rankValue, rankString);
                }

                throw new ArgumentException("Rank number value must be in range: [2, 10]", nameof(rankString));
            }

            if (CardDefaultRanks.TryGetValue(rankString.ToUpper(), out rankValue))
            {
                return Create(rankValue, rankString);
            }

            throw new ArgumentException("Rank value must be in list: [J, Q, K, A]", nameof(rankString));
        }
        
        internal static bool TryParse(string rankString, out RankComponent rank)
        {
            if (int.TryParse(rankString, out var rankValue))
            {
                if (IsRankNumberValid(rankValue))
                {
                    rank = Create(rankValue, rankString);
                    return true;
                }

                rank = Invalid;
                return false;
            }

            if (CardDefaultRanks.TryGetValue(rankString.ToUpper(), out rankValue))
            {
                rank = Create(rankValue, rankString);
                return true;
            }

            rank = Invalid;
            return false;
        }
        
        internal static RankComponent Create(int value, string name)
        {
            if (value < 0)
            {
                throw new ArgumentException("Rank value can't be less than zero", nameof(value));
            }
            
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Rank name can't be an empty string", nameof(name));
            }
            
            return new RankComponent(value, name);
        }
        
        private RankComponent(int value, string name)
        {
            Value = value;
            Name = name;
        }

        /// <summary>
        /// Rank value in integer number
        /// </summary>
        public int Value { get; }
        
        /// <summary>
        /// Rank display name
        /// </summary>
        public string Name { get; }

        public bool IsNumeric()
        {
            return Value is >= 2 and <= 10;
        }

        public bool IsLetter()
        {
            return Value >= CardDefaultRanks["J"] && Value <= CardDefaultRanks["A"];
        }
        
        public static bool operator >(in RankComponent a, in RankComponent b)
        {
            return a.Value > b.Value;
        }

        public static bool operator <(in RankComponent a, in RankComponent b)
        {
            return a.Value < b.Value;
        }
        
        public static bool operator ==(in RankComponent a, in RankComponent b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(in RankComponent a, in RankComponent b)
        {
            return a.Value != b.Value;
        }
        
        public bool Equals(RankComponent other)
        {
            return Value == other.Value && Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            return obj is RankComponent other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Name);
        }

        public int CompareTo(RankComponent other)
        {
            return Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            return Name;
        }

        private static bool IsRankNumberValid(int rankValueNumber)
        {
            return rankValueNumber is >= 2 and <= 10;
        }
    }
}