using System;
using App.Scripts.Cards;
using App.Scripts.Durak.Players.Base;

namespace App.Scripts.Durak.Players.Models
{
    public class Attacker : IPlayingCardConsumer, IEquatable<Attacker>
    {
        public DurakPlayer Player { get; }
        public bool IsAcceptedDefense { get; set; }
        public bool CanAcceptDefense { get; set; }
        public bool CanBesiege { get; set; }
        public bool CanAttack { get; set; }
        
        public Attacker(DurakPlayer player)
        {
            Player = player;
        }

        public bool CanPerformAttack()
        {
            return CanAttack && !CanBesiege && Player.HasCards;
        }
        
        public bool CanPerformBesiege()
        {
            return !CanAttack && CanBesiege && Player.HasCards;
        }

        public void DeactivateAcceptDefense()
        {
            CanAcceptDefense = false;
            IsAcceptedDefense = false;
        }

        public void DeactivateAll()
        {
            CanBesiege = false;
            DeactivateAcceptDefense(); 
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

        public bool Equals(Attacker other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Player, other.Player);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Attacker attacker && Equals(attacker);
        }

        public override int GetHashCode()
        {
            return Player?.GetHashCode() ?? 0;
        }
    }
}