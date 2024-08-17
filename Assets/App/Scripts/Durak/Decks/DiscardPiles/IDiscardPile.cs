using System.Collections.Generic;
using App.Scripts.Durak.Players.Base;
using Kartishki.Core;

namespace App.Scripts.Durak.Decks.DiscardPiles
{
    public interface IDiscardPile : IPlayingCardConsumer
    {
        IReadOnlyList<PlayingCard> GetBeatenCards();
    }
}