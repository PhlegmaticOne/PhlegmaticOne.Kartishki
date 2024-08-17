using System;
using System.Collections.Generic;

namespace Kartishki.Core.Components
{
    /// <summary>
    /// Playing card component represents its suit
    /// </summary>
    [Serializable]
    public readonly struct SuitComponent : IEquatable<SuitComponent>
    {
        private class SuitData
        {
            public readonly string Name;
            public readonly int Color;

            public SuitData(string name, int color)
            {
                Name = name;
                Color = color;
            }
        }
        
        private const char SpadesSuitChar = '♠';
        private const char HeartsSuitChar = '♥';
        private const char DiamondsSuitChar = '♦';
        private const char ClubsSuitChar = '♣';

        public const int RedColor = 0;
        public const int BlackColor = 1;
        
        private static readonly Dictionary<char, SuitData> DefaultSuits = new()
        {
            { SpadesSuitChar, new SuitData("Spades", BlackColor) },
            { HeartsSuitChar, new SuitData("Hearts", RedColor) },
            { DiamondsSuitChar, new SuitData("Diamonds", RedColor) },
            { ClubsSuitChar, new SuitData("Clubs", BlackColor) },
        };
        
        /// <summary>
        /// Invalid clubs that cannot be created
        /// </summary>
        public static SuitComponent Invalid = new('\0', string.Empty, -1);
        
        /// <summary>
        /// Spades ♠
        /// </summary>
        public static SuitComponent Spades => new(SpadesSuitChar, DefaultSuits[SpadesSuitChar].Name, DefaultSuits[SpadesSuitChar].Color);
        
        /// <summary>
        /// Hearts ♥
        /// </summary>
        public static SuitComponent Hearts => new(HeartsSuitChar, DefaultSuits[HeartsSuitChar].Name, DefaultSuits[HeartsSuitChar].Color);
        
        /// <summary>
        /// Diamonds ♦
        /// </summary>
        public static SuitComponent Diamonds => new(DiamondsSuitChar, DefaultSuits[DiamondsSuitChar].Name, DefaultSuits[DiamondsSuitChar].Color);
        
        /// <summary>
        /// Clubs ♣
        /// </summary>
        public static SuitComponent Clubs => new(ClubsSuitChar, DefaultSuits[ClubsSuitChar].Name, DefaultSuits[ClubsSuitChar].Color);

        /// <summary>
        /// Enumerates default suits: ♠ (Spades), ♥ (Hearts), ♦ (Diamonds), ♣ (Clubs)
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SuitComponent> Enumerate()
        {
            yield return Spades;
            yield return Hearts;
            yield return Diamonds;
            yield return Clubs;
        }

        public static IEnumerable<string> EnumerateStringValues()
        {
            yield return SpadesSuitChar.ToString();
            yield return HeartsSuitChar.ToString();
            yield return DiamondsSuitChar.ToString();
            yield return ClubsSuitChar.ToString();
        }

        /// <summary>
        /// Parses default suit from its string representation
        /// </summary>
        /// <param name="value">Suit string representation: ♠, ♥, ♦, ♣</param>
        /// <returns>Parsed suit: Spades, Hearts, Diamonds, Clubs</returns>
        /// <exception cref="ArgumentException">Throwing when input suit string is not one of: ♠, ♥, ♦, ♣</exception>
        internal static SuitComponent Parse(string value)
        {
            if (!ValidateParseValue(value, out var suitData))
            {
                throw new ArgumentException("Invalid suit value provided", nameof(value));
            }

            return Create(value[0], suitData.Name, suitData.Color);
        }
        
        /// <summary>
        /// Tries to parse default suit from its string representation
        /// </summary>
        /// <param name="value">Suit string representation: ♠, ♥, ♦, ♣</param>
        /// <param name="suit">Parsed suit</param>
        /// <returns>true - suit was parsed, otherwise - not</returns>
        internal static bool TryParse(string value, out SuitComponent suit)
        {
            if (!ValidateParseValue(value, out var suitData))
            {
                suit = Invalid;
                return false;
            }

            suit = Create(value[0], suitData.Name, suitData.Color);
            return true;
        }

        /// <summary>
        /// Creates suit from one char value and name
        /// </summary>
        /// <param name="value">Single character suit view</param>
        /// <param name="name">Suit name</param>
        /// <param name="color">Suit color</param>
        /// <returns>Created suit</returns>
        /// <exception cref="ArgumentException">Throwing when name is empty or value is not letter or symbol</exception>
        internal static SuitComponent Create(char value, string name, int color)
        {
            if (color < 0)
            {
                throw new ArgumentException("Suit color can't be less than zero", nameof(color));
            }
            
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Suit name can't be an empty string", nameof(name));
            }

            if (!ValidateValueChar(value))
            {
                throw new ArgumentException("Suit value is invalid", nameof(value));
            }

            return new SuitComponent(value, name, color);
        }

        private SuitComponent(char value, string name, int color)
        {
            Value = value;
            Name = name;
            Color = color;
        }

        /// <summary>
        /// Suit single character value
        /// </summary>
        public char Value { get; }

        /// <summary>
        /// Suit display name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Suit color
        /// </summary>
        public int Color { get; }

        /// <summary>
        /// Checks if current suit is not equal to <code>SuitComponent.Invalid</code>
        /// </summary>
        /// <returns>true - suit is valid, otherwise - not</returns>
        public bool IsValid()
        {
            return !Equals(Invalid);
        }

        /// <summary>
        /// Checks current suit equality with other by equality of their values and names
        /// </summary>
        /// <param name="other">Other suit</param>
        /// <returns>true - suits are equal, otherwise - not</returns>
        public bool Equals(SuitComponent other)
        {
            return Value == other.Value && Name == other.Name && Color == other.Color;
        }

        /// <summary>
        /// Checks current suit equality with other by equality of their values and names
        /// </summary>
        /// <param name="obj">Other suit</param>
        /// <returns>true - suits are equal, otherwise - not</returns>
        public override bool Equals(object obj)
        {
            return obj is SuitComponent other && Equals(other);
        }

        /// <summary>
        /// Calculates hash code of suit using its value and name
        /// </summary>
        /// <returns>Suit hash code</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Name, Color);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        private static bool ValidateParseValue(string value, out SuitData suitData)
        {
            suitData = null;
            return value.Length == 1 && DefaultSuits.TryGetValue(value[0], out suitData);
        }

        private static bool ValidateValueChar(in char value)
        {
            return char.IsLetter(value) || char.IsSymbol(value);
        }
    }
}