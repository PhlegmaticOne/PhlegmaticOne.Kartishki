using System;
using System.Collections.Generic;

namespace Kartishki.Core.Components
{
    [Serializable]
    public readonly partial struct SuitComponent : IEquatable<SuitComponent>
    {
        private static readonly Dictionary<char, SuitData> DefaultSuits = new()
        {
            { PlayingCardConsts.SpadesSuitChar, new SuitData(PlayingCardConsts.SpadesSuitName, PlayingCardConsts.BlackColor) },
            { PlayingCardConsts.HeartsSuitChar, new SuitData(PlayingCardConsts.HeartsSuitName, PlayingCardConsts.RedColor) },
            { PlayingCardConsts.DiamondsSuitChar, new SuitData(PlayingCardConsts.DiamondsSuitName, PlayingCardConsts.RedColor) },
            { PlayingCardConsts.ClubsSuitChar, new SuitData(PlayingCardConsts.ClubsSuitName, PlayingCardConsts.BlackColor) },
            { PlayingCardConsts.JokerSuitChar, new SuitData(PlayingCardConsts.JokerSuitName, PlayingCardConsts.InvalidColor) },
        };

        private SuitComponent(char value, string name)
        {
            Value = value;
            Name = name;
        }

        internal static SuitComponent Create(char value, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Suit name can't be an empty string", nameof(name));
            }

            if (!ValidateValueChar(value))
            {
                throw new ArgumentException("Suit value is invalid", nameof(value));
            }

            return new SuitComponent(value, name);
        }

        internal static bool TryGetSuitName(string value, out string name)
        {
            if (value.Length == 1 && DefaultSuits.TryGetValue(value[0], out var suitData))
            {
                name = suitData.Name;
                return true;
            }
            
            name = null;
            return false;
        }

        public char Value { get; }

        public string Name { get; }

        public int Color => DefaultSuits[Value].Color;

        public bool IsValid()
        {
            return !Equals(Invalid);
        }

        public bool IsJokerSuit()
        {
            return Value == PlayingCardConsts.JokerSuitChar;
        }

        public bool Equals(SuitComponent other)
        {
            return Value == other.Value && Name == other.Name && Color == other.Color;
        }

        public override bool Equals(object obj)
        {
            return obj is SuitComponent other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Name, Color);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        private static bool ValidateValueChar(in char value)
        {
            return char.IsLetter(value) || char.IsSymbol(value);
        }
    }
}