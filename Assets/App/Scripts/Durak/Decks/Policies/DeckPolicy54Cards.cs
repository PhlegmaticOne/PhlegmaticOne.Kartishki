using System.Collections.Generic;
using Kartishki.Core;
using Kartishki.Core.Extensions;

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