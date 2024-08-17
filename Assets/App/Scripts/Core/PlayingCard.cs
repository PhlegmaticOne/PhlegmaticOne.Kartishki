using System;
using System.Runtime.CompilerServices;
using Kartishki.Core.Builders;
using Kartishki.Core.Components;
using Kartishki.Core.Parser;

[assembly : InternalsVisibleTo("Kartishki.Tests")]

namespace Kartishki.Core
{
    /// <summary>
    /// Represents a generic playing card used in card games
    /// </summary>
    public class PlayingCard : IEquatable<PlayingCard>
    {
        private static readonly PlayerCardBuilder Builder = new();
        
        private PlayingCard(in CardComponent card, in JokerComponent joker)
        {
            Card = card;
            Joker = joker;
        }

        /// <summary>
        /// Provides functionality to create default playing cards
        /// </summary>
        public static readonly PlayingCardDefaults Defaults = new();

        /// <summary>
        /// Provides functionality to create custom playing cards
        /// </summary>
        /// <returns>Builder to create custom playing card</returns>
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
        
        /// <summary>
        /// Creates plating card with provided components
        /// </summary>
        /// <param name="card">Card component</param>
        /// <param name="joker">Joker component</param>
        /// <returns>Playing card with Card and Joker components</returns>
        /// <exception cref="ArgumentException">Throwing when Card and Joker components are both invalid</exception>
        internal static PlayingCard Create(in CardComponent card, in JokerComponent joker)
        {
            if (!card.IsValid() && !joker.IsValid())
            {
                throw new ArgumentException("Playing card must be Joker or Card");
            }
            
            return new PlayingCard(card, joker);
        }

        /// <summary>
        /// Joker component. If playing card is joker then Joker is valid component
        /// </summary>
        public JokerComponent Joker { get; }
        /// <summary>
        /// Card. If playing card has default card value - rank + value - then Card is valid component
        /// </summary>
        public CardComponent Card { get; }

        /// <summary>
        /// Checks if current playing card represents default card - rank + value
        /// </summary>
        /// <returns>true - card is default, false - it is a Joker</returns>
        public bool IsJoker()
        {
            return Joker.IsValid();
        }
        
        /// <summary>
        /// Checks if current playing card represents Joker
        /// </summary>
        /// <returns>true - card is Joker, false - it is a default card</returns>
        public bool IsCard()
        {
            return Card.IsValid();
        }

        /// <summary>
        /// Checks if current playing card has same color with other card
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool HasSameColor(PlayingCard card)
        {
            return GetCardColor() == card.GetCardColor();
        }
        
        /// <summary>
        /// Checks if current playing card has specified suit
        /// </summary>
        /// <returns>true - card has specified suit, false - card is Joker or it has different suit</returns>
        public bool HasSuit(in SuitComponent suit)
        {
            return IsCard() && Card.Suit.Equals(suit);
        }

        /// <summary>
        /// Checks if current playing card has specified rank
        /// </summary>
        /// <returns>true - card has specified rank, false - card is Joker or it has different rank</returns>
        public bool HasRank(in RankComponent rank)
        {
            return IsCard() && Card.Rank.Equals(rank);
        }
        
        /// <summary>
        /// Checks if current card is equal to other by comparing theirs Card and Joker components
        /// </summary>
        /// <param name="other">Other playing card to compare</param>
        /// <returns>true - cards are equal, false - they are different</returns>
        public bool Equals(PlayingCard other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Card.Equals(other.Card) && Joker.Equals(other.Joker);
        }

        /// <summary>
        /// Checks if current card is equal to other object by comparing theirs Card and Joker components
        /// </summary>
        /// <param name="obj">Other object to compare</param>
        /// <returns>true - cards are equal, false - they are different</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((PlayingCard)obj);
        }

        /// <summary>
        /// Calculates playing card hash code from its Card and Joker components
        /// </summary>
        /// <returns>Playing card hash code</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Card, Joker);
        }

        /// <summary>
        /// Returns card string representation. Default card: 6♠ 10♥ Q♦ A♣; Joker: ★0 ★1
        /// </summary>
        /// <returns>Playing card string representation</returns>
        public override string ToString()
        {
            return IsJoker() ? Joker.ToString() : Card.ToString();
        }

        private int GetCardColor()
        {
            return IsJoker() ? Joker.ColorType : Card.Suit.Color;
        }
    }
}