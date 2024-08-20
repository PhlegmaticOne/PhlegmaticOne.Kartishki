using System;

namespace Kartishki.Core.Components
{
    /// <summary>
    /// Playing card component represents Joker
    /// </summary>
    [Serializable]
    public readonly struct JokerComponent : IEquatable<JokerComponent>
    {
        public const int RedColor = 0;
        public const int BlackColor = 1;

        public const char JokerChar = '★';
        
        public static JokerComponent Invalid = new(-1);
        public static JokerComponent Red => new(RedColor);
        public static JokerComponent Black => new(BlackColor);

        internal static JokerComponent Parse(ReadOnlySpan<char> colorNumberValue)
        {
            if (int.TryParse(colorNumberValue, out var color) && IsJokerColorValid(color))
            {
                return Create(color);
            }

            throw new ArgumentException("Invalid default joker color provided", nameof(colorNumberValue));
        }

        internal static bool TryParse(ReadOnlySpan<char> colorNumberValue, out JokerComponent joker)
        {
            if (int.TryParse(colorNumberValue, out var color) && IsJokerColorValid(color))
            {
                joker = Create(color);
                return true;
            }

            joker = Invalid;
            return false;
        }
        
        internal static JokerComponent Create(int colorType)
        {
            if (colorType < 0)
            {
                throw new ArgumentException("Joker color type can't be less than zero", nameof(colorType));
            }

            return new JokerComponent(colorType);
        }

        private JokerComponent(int color)
        {
            Color = color;
        }

        /// <summary>
        /// Joker color
        /// </summary>
        public int Color { get; }

        public bool IsValid()
        {
            return !Equals(Invalid);
        }

        public bool Equals(JokerComponent other)
        {
            return Color == other.Color;
        }

        public override bool Equals(object obj)
        {
            return obj is JokerComponent other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Color;
        }

        public override string ToString()
        {
            return $"★{Color}";
        }

        private static bool IsJokerColorValid(int color)
        {
            return color is RedColor or BlackColor;
        }
    }
}