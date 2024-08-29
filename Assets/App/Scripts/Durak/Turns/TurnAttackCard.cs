using System;
using App.Scripts.Cards;
using App.Scripts.Durak.Players.Models;

namespace App.Scripts.Durak.Turns
{
    public readonly struct TurnAttackCard : IEquatable<TurnAttackCard>
    {
        internal static TurnAttackCard WithoutPlayer(PlayingCard attackCard)
        {
            return new TurnAttackCard(attackCard, null);
        }
        
        public TurnAttackCard(PlayingCard attackCard, Attacker attacker)
        {
            AttackCard = attackCard;
            Attacker = attacker;
        }

        public PlayingCard AttackCard { get; }
        public Attacker Attacker { get; }

        public bool Equals(TurnAttackCard other)
        {
            return Equals(AttackCard, other.AttackCard) && Equals(Attacker, other.Attacker);
        }

        public override bool Equals(object obj)
        {
            return obj is TurnAttackCard other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AttackCard, Attacker);
        }
    }
}