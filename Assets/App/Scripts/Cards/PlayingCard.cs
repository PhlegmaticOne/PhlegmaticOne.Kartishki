using System;
using App.Scripts.Cards.Builders;
using App.Scripts.Cards.Components;
using App.Scripts.Cards.Parser;

namespace App.Scripts.Cards
{
    public class PlayingCard : IEquatable<PlayingCard>
    {
        private static readonly PlayerCardBuilder Builder = new();
        
        public PlayingCard(int color, RankComponent rank, SuitComponent suit)
        {
            Color = color;
            Rank = rank;
            Suit = suit;
        }

        public static readonly PlayingCardDefaults Defaults = new();

        public static PlayerCardBuilder Create() => Builder;

        /// <summary>
        /// Parses a playing card from it's string representation
        /// </summary>
        /// <param name="value">Card string representation (rank + suit)</param>
        /// <example>6♠ 10♥ Q♦ A♣ (Card) or ★0 ★1 (Joker)</example>
        /// <returns>Playing card</returns>
        public static PlayingCard Parse(string value)
        {
            return PlayingCardParser.Parse(value);
        }

        /// <summary>
        /// Tries to parse a playing card from it's string representation
        /// </summary>
        /// <param name="value">Card string representation (rank + suit)</param>
        /// <param name="card">Playing card</param>
        /// <example>6♠ 10♥ Q♦ A♣ (Card) or ★0 ★1 (Joker)</example>
        /// <returns>true - card parsed successfully, false - card wasn't parsed</returns>
        public static bool TryParse(string value, out PlayingCard card)
        {
            return PlayingCardParser.TryParse(value, out card);
        }

        public int Color { get; }
        public SuitComponent Suit { get; }
        public RankComponent Rank { get; }

        public bool IsJoker()
        {
            return Suit.IsJokerSuit() && Rank.IsJokerRank();
        }
        
        public bool IsCard()
        {
            return !IsJoker();
        }

        public bool HasSameColor(PlayingCard card)
        {
            return Color == card.Color;
        }
        
        public bool HasSuit(in SuitComponent suit)
        {
            return IsCard() && Suit.Equals(suit);
        }

        public bool HasRank(in RankComponent rank)
        {
            return IsCard() && Rank.Equals(rank);
        }
        
        public bool Equals(PlayingCard other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Color.Equals(other.Color) && Suit.Equals(other.Suit) && Rank.Equals(other.Rank);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((PlayingCard)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Color, Rank, Suit);
        }

        public override string ToString()
        {
            return $"{Rank}{Suit}";
        }
    }
}