using System;
using System.Text.RegularExpressions;
using App.Scripts.Cards.Components;

namespace App.Scripts.Cards.Parser
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
            var rank = ParseRank(rankValue);
            var suit = ParseSuit(suitValue);
            return PlayingCard.Create().Card().WithRank(rank).WithSuit(suit);
        }
        
        private static bool TryParseCard(string suitValue, string rankValue, out PlayingCard card)
        {
            if (TryParseRank(rankValue, out var rankComponent) &&
                TryParseSuit(suitValue, out var suitComponent))
            {
                card = PlayingCard.Create().Card().WithRank(rankComponent).WithSuit(suitComponent);
                return true;
            }

            card = null;
            return false;
        }
        
        private static SuitComponent ParseSuit(string value)
        {
            if (!SuitComponent.TryGetSuitName(value, out var suitName))
            {
                throw new ArgumentException("Invalid suit value provided", nameof(value));
            }

            return SuitComponent.Create(value[0], suitName);
        }

        /// <summary>
        /// Tries to parse default suit from its string representation
        /// </summary>
        /// <param name="value">Suit string representation: ♠, ♥, ♦, ♣</param>
        /// <param name="suit">Parsed suit</param>
        /// <returns>true - suit was parsed, otherwise - not</returns>
        private static bool TryParseSuit(string value, out SuitComponent suit)
        {
            if (!SuitComponent.TryGetSuitName(value, out var suitName))
            {
                suit = SuitComponent.Invalid;
                return false;
            }

            suit = SuitComponent.Create(value[0], suitName);
            return true;
        }
        
        private static RankComponent ParseRank(string rankString)
        {
            if (int.TryParse(rankString, out var rankValue))
            {
                if (RankComponent.IsRankValueNumeric(rankValue))
                {
                    return RankComponent.Create(rankValue, rankString);
                }

                throw new ArgumentException("Rank number value must be in range: [2, 10]", nameof(rankString));
            }

            if (RankComponent.TryGetRankValue(rankString.ToUpper(), out rankValue))
            {
                return RankComponent.Create(rankValue, rankString);
            }

            throw new ArgumentException("Rank value must be in list: [J, Q, K, A]", nameof(rankString));
        }
        
        internal static bool TryParseRank(string rankString, out RankComponent rank)
        {
            if (int.TryParse(rankString, out var rankValue))
            {
                if (RankComponent.IsRankValueNumeric(rankValue))
                {
                    rank = RankComponent.Create(rankValue, rankString);
                    return true;
                }

                rank = RankComponent.Invalid;
                return false;
            }

            if (RankComponent.TryGetRankValue(rankString.ToUpper(), out rankValue))
            {
                rank = RankComponent.Create(rankValue, rankString);
                return true;
            }

            rank = RankComponent.Invalid;
            return false;
        }

        private static PlayingCard ParseJoker(string jokerColor)
        {
            var joker = int.Parse(jokerColor);
            return PlayingCard.Create().Joker().WithColor(joker);
        }
        
        private static bool TryParseJoker(string jokerColor, out PlayingCard card)
        {
            if (int.TryParse(jokerColor, out var joker))
            {
                card = PlayingCard.Create().Joker().WithColor(joker);
                return true;
            }
            
            card = null;
            return false;
        }
    }
}