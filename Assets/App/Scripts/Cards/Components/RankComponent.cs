using System;

namespace App.Scripts.Cards.Components
{
    [Serializable]
    public readonly partial struct RankComponent : IEquatable<RankComponent>, IComparable<RankComponent>
    {
        private RankComponent(int value, string name)
        {
            Value = value;
            Name = name;
        }

        internal static RankComponent Create(int value, string name)
        {
            if (value < 0)
            {
                throw new ArgumentException("Rank value is invalid", nameof(value));
            }
            
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Rank name is invalid", nameof(name));
            }
            
            return new RankComponent(value, name);
        }

        internal static bool TryGetRankValue(string rank, out int rankValue)
        {
            return RanksLetterMap.TryGetValue(rank, out rankValue);
        }

        internal static bool IsRankValueNumeric(int rankValueNumber)
        {
            return rankValueNumber is >= 2 and <= 10;
        }

        public int Value { get; }
        public string Name { get; }

        public bool IsNumeric()
        {
            return Value is >= 2 and <= 10;
        }

        public bool IsLetter()
        {
            return Value >= RanksLetterMap[PlayingCardConsts.Jack] && 
                   Value <= RanksLetterMap[PlayingCardConsts.Ace];
        }

        public bool IsJokerRank()
        {
            return Value == RanksLetterMap[PlayingCardConsts.Joker];
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
    }
}