using System;

namespace Kartishki.Core.Components
{
    /// <summary>
    /// Playing card component represents card rank and suit
    /// </summary>
    [Serializable]
    public readonly struct CardComponent : IEquatable<CardComponent>
    {
        /// <summary>
        /// Represents invalid component with invalid rank and suit components
        /// </summary>
        public static CardComponent Invalid = new(RankComponent.Invalid, SuitComponent.Invalid);
        
        internal CardComponent(in RankComponent rank, in SuitComponent suit)
        {
            Rank = rank;
            Suit = suit;
        }
        
        /// <summary>
        /// Playing card rank
        /// </summary>
        public RankComponent Rank { get; }
        /// <summary>
        /// Playing card suit
        /// </summary>
        public SuitComponent Suit { get; }
        
        public bool IsValid()
        {
            return !Equals(Invalid);
        }
        
        public bool Equals(CardComponent other)
        {
            return Rank.Equals(other.Rank) && Suit.Equals(other.Suit);
        }

        public override bool Equals(object obj)
        {
            return obj is CardComponent other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Rank, Suit);
        }
        
        public override string ToString()
        {
            return $"{Rank} {Suit}";
        }
    }
}