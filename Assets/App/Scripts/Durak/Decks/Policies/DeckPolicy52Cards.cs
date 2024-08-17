using System.Collections.Generic;
using Kartishki.Core;
using Kartishki.Core.Extensions;

namespace App.Scripts.Durak.Decks.Policies
{
    public class DeckPolicy52Cards : IDeckPolicy
    {
        public IEnumerable<PlayingCard> GetDeckCards()
        {
            return Deck.Enumerate52Cards().Shuffle();
        }
    }
}