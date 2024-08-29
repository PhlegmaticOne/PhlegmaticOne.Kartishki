using System.Collections.Generic;
using App.Scripts.Cards;
using App.Scripts.Cards.Extensions;

namespace App.Scripts.Durak.Decks.Policies
{
    public class DeckPolicy54Cards : IDeckPolicy
    {
        public IEnumerable<PlayingCard> GetDeckCards()
        {
            return Deck.Enumerate54Cards().Shuffle();
        }
    }
}