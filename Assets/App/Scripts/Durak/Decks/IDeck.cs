using App.Scripts.Durak.Players.Base;
using Kartishki.Core;

namespace App.Scripts.Durak.Decks
{
    public interface IDeck
    {
        PlayingCard Trump { get; }
        bool IsEmpty { get; }
        int CardsCount { get; }
        void FillCardConsumer(IPlayingCardConsumer cardConsumer);
    }
}