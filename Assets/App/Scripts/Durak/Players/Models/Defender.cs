using System;
using App.Scripts.Durak.Players.Base;
using Kartishki.Core;

namespace App.Scripts.Durak.Players.Models
{
    public class Defender : IPlayingCardConsumer, IEquatable<Defender>
    {
        public DurakPlayer Player { get; }
        public bool IsFailingDefense { get; set; }
        public bool CanFailDefense { get; set; }

        public Defender(DurakPlayer player)
        {
            Player = player;
        }

        public bool CheckCanFailDefense()
        {
            return CanFailDefense && !IsFailingDefense;
        }

        public bool IsOverfilled()
        {
            return Player.IsOverfilled();
        }

        public bool IsFilled()
        {
            return Player.IsFilled();
        }

        public void PushCard(PlayingCard card)
        {
            Player.PushCard(card);
        }

        public bool Equals(Defender other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Player, other.Player);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Defender defender && Equals(defender);
        }

        public override int GetHashCode()
        {
            return Player != null ? Player.GetHashCode() : 0;
        }
    }
}