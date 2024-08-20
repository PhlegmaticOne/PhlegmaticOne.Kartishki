using System;
using Kartishki.Core;
using Kartishki.Core.Components;

namespace App.Scripts.DurakGame.PlayingCards.Views.ViewModel
{
    public class PlayingCardViewModel : IPlayingCardViewModel
    {
        private PlayingCardViewModel(string rankView, string suitView, RankComponent rank, PlayingCardViewModelType cardType, int color)
        {
            RankView = rankView;
            SuitView = suitView;
            Rank = rank;
            CardType = cardType;
            Color = color;
        }

        public static IPlayingCardViewModel FromCard(PlayingCard card)
        {
            return new PlayingCardViewModel(
                card.Card.Rank.Name,
                card.Card.Suit.GetValueString(),
                card.Card.Rank,
                GetCardType(card),
                GetCardColor(card));
        }
        
        public string RankView { get; }
        public string SuitView { get; }
        public RankComponent Rank { get; }
        public int Color { get; }
        public PlayingCardViewModelType CardType { get; }

        private static int GetCardColor(PlayingCard card)
        {
            return card.IsJoker() ? card.Joker.Color : card.Card.Suit.Color;
        }
        
        private static PlayingCardViewModelType GetCardType(PlayingCard card)
        {
            if (card.IsJoker())
            {
                return PlayingCardViewModelType.Joker;
            }

            if (card.Card.Rank.IsNumeric())
            {
                return PlayingCardViewModelType.Numeric;
            }

            if (card.Card.Rank.IsLetter())
            {
                return PlayingCardViewModelType.Letter;
            }

            throw new ArgumentException("Unknown card card", nameof(card));
        }
    }
}