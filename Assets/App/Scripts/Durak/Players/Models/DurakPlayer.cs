using System;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Hand;
using Kartishki.Core;

namespace App.Scripts.Durak.Players.Models
{
    public class DurakPlayer : IEquatable<DurakPlayer>, IPlayingCardConsumer
    {
        private const int DurakPlayerHandCapacityDefault = 6;
        
        private readonly PlayerHand _hand;
        
        internal static DurakPlayer New => new(Guid.NewGuid());
        
        public Guid Id { get; }
        public IReadOnlyPlayerHand Hand => _hand;
        public bool HasCards => _hand.CardsCount > 0;
        
        public DurakPlayer(Guid id, int handCapacity = DurakPlayerHandCapacityDefault)
        {
            Id = id;
            _hand = new PlayerHand(handCapacity);
        }

        public PlayingCard GetCardAt(int index)
        {
            return Hand.GetCardAt(index);
        }

        public PlayingCard PullCardAt(int index)
        {
            return _hand.PullCardAt(index);
        }

        public bool IsOverfilled()
        {
            return Hand.CardsCount >= Hand.Capacity;
        }

        public bool IsFilled()
        {
            return Hand.CardsCount == Hand.Capacity;
        }

        public void PushCard(PlayingCard card)
        {
            _hand.PushCard(card);
        }

        public Attacker ToAttacker(bool initialCanAttack = true)
        {
            return new Attacker(this)
            {
                CanAttack = initialCanAttack
            };
        }

        public Defender ToDefender()
        {
            return new Defender(this);
        }

        public bool Equals(DurakPlayer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is DurakPlayer player && Equals(player);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}