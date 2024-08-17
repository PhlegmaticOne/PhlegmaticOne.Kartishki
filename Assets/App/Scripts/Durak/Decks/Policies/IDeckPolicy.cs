using System.Collections.Generic;
using Kartishki.Core;

namespace App.Scripts.Durak.Decks.Policies
{
    public interface IDeckPolicy
    {
        IEnumerable<PlayingCard> GetDeckCards();
    }
}