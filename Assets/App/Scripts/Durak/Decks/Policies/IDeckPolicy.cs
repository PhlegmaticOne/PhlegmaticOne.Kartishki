using System.Collections.Generic;
using App.Scripts.Cards;

namespace App.Scripts.Durak.Decks.Policies
{
    public interface IDeckPolicy
    {
        IEnumerable<PlayingCard> GetDeckCards();
    }
}