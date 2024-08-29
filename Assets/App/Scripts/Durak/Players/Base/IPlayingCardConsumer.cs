using App.Scripts.Cards;

namespace App.Scripts.Durak.Players.Base
{
    public interface IPlayingCardConsumer
    {
        bool IsOverfilled();
        bool IsFilled();
        void PushCard(PlayingCard card);
    }
}