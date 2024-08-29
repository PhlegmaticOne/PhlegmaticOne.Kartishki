using System.Collections.Generic;
using App.Scripts.Cards;
using App.Scripts.Cards.Components;

namespace App.Scripts.Durak.Players.Hand
{
    public interface IReadOnlyPlayerHand
    {
        public int Capacity { get; }
        public int CardsCount { get; }
        IEnumerable<PlayingCard> GetCardsWithSuit(SuitComponent suit);
        IEnumerable<PlayingCard> GetCardsWithRank(RankComponent rank);
        PlayingCard GetCardAt(int cardIndex);
    }
}