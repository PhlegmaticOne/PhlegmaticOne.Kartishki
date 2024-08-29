using System.Collections.Generic;
using App.Scripts.Cards;
using App.Scripts.Durak.Players.Base;

namespace App.Scripts.Durak.Decks.DiscardPiles
{
    public interface IDiscardPile : IPlayingCardConsumer
    {
        IReadOnlyList<PlayingCard> GetBeatenCards();
    }
}