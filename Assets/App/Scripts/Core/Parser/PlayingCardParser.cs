using System;
using System.Text.RegularExpressions;
using Kartishki.Core.Components;

namespace Kartishki.Core.Parser
{
    internal static class PlayingCardParser
    {
        private static readonly Regex CardRegex = new("([2-9]|10|J|Q|K|A)[♠, ♥, ♦, ♣]");
        private static readonly Regex CardInverseRegex = new("[♠, ♥, ♦, ♣]([2-9]|10|J|Q|K|A)");
        private static readonly Regex JokerRegex = new("[0|1](★)");
        private static readonly Regex JokerInverseRegex = new("(★)[0|1]");
        
        public static PlayingCard Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null or empty");
            }

            var processed = value.Replace(" ", string.Empty);
            
            if (JokerRegex.IsMatch(processed))
            {
                var jokerColor = value[0].ToString();
                return ParseJoker(jokerColor);
            }
            
            if (JokerInverseRegex.IsMatch(processed))
            {
                var jokerColor = value[1].ToString();
                return ParseJoker(jokerColor);
            }

            if (CardRegex.IsMatch(processed))
            {
                var suitValue = value[^1].ToString();
                var rankValue = value[..^1];
                return ParseCard(suitValue, rankValue);
            }

            if (CardInverseRegex.IsMatch(processed))
            {
                var suitValue = value[0].ToString();
                var rankValue = value[1..];
                return ParseCard(suitValue, rankValue);
            }

            throw new ArgumentException("Provided card string representation is not valid");
        }
        
        public static bool TryParse(string value, out PlayingCard card)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                card = null;
                return false;
            }
            
            var processed = value.Replace(" ", string.Empty);
            
            if (JokerRegex.IsMatch(processed))
            {
                var jokerColor = value[0].ToString();
                return TryParseJoker(jokerColor, out card);
            }
            
            if (JokerInverseRegex.IsMatch(processed))
            {
                var jokerColor = value[1].ToString();
                return TryParseJoker(jokerColor, out card);
            }

            if (CardRegex.IsMatch(processed))
            {
                var suitValue = value[^1].ToString();
                var rankValue = value[..^1];
                return TryParseCard(suitValue, rankValue, out card);
            }

            if (CardInverseRegex.IsMatch(processed))
            {
                var suitValue = value[0].ToString();
                var rankValue = value[1..];
                return TryParseCard(suitValue, rankValue, out card);
            }

            card = null;
            return false;
        }

        private static PlayingCard ParseCard(string suitValue, string rankValue)
        {
            var rank = RankComponent.Parse(rankValue);
            var suit = SuitComponent.Parse(suitValue);
            return PlayingCard.Create().Card().WithRank(rank).WithSuit(suit);
        }
        
        private static bool TryParseCard(string suitValue, string rankValue, out PlayingCard card)
        {
            if (RankComponent.TryParse(rankValue, out var rankComponent) &&
                SuitComponent.TryParse(suitValue, out var suitComponent))
            {
                card = PlayingCard.Create().Card().WithRank(rankComponent).WithSuit(suitComponent);
                return true;
            }

            card = null;
            return false;
        }

        private static PlayingCard ParseJoker(string jokerColor)
        {
            var joker = JokerComponent.Parse(jokerColor);
            return PlayingCard.Create().Joker().WithComponent(joker);
        }
        
        private static bool TryParseJoker(string jokerColor, out PlayingCard card)
        {
            if (JokerComponent.TryParse(jokerColor, out var joker))
            {
                card = PlayingCard.Create().Joker().WithComponent(joker);
                return true;
            }
            
            card = null;
            return false;
        }
    }
}