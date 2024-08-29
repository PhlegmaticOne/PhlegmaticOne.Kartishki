using App.Scripts.Cards;
using App.Scripts.Durak.Players.Base;

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