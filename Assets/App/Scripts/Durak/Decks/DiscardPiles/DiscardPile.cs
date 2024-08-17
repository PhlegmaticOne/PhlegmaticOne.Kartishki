using System.Collections.Generic;
using Kartishki.Core;

namespace App.Scripts.Durak.Decks.DiscardPiles
{
    public class DiscardPile : IDiscardPile
    {
        private readonly List<PlayingCard> _beatenCards = new();

        public void PushCards(IEnumerable<PlayingCard> beatenCards)
        {
            _beatenCards.AddRange(beatenCards);
        }

        public IReadOnlyList<PlayingCard> GetBeatenCards()
        {
            return _beatenCards;
        }

        public bool IsOverfilled() => false;

        public bool IsFilled() => false;

        public void PushCard(PlayingCard card)
        {
            _beatenCards.Add(card);
        }
    }
}