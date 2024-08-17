using Kartishki.Core;

namespace App.Scripts.Durak.Players.Base
{
    public interface IPlayingCardConsumer
    {
        bool IsOverfilled();
        bool IsFilled();
        void PushCard(PlayingCard card);
    }
}