using System;
using System.Collections.Generic;
using System.Linq;
using Kartishki.Core;
using Kartishki.Core.Components;

namespace App.Scripts.Durak.Turns
{
    public class TurnCardsContainer
    {
        private readonly List<TurnAttackCard> _attackSequence;
        private readonly List<PlayingCard> _defenseSequence;

        public TurnCardsContainer()
        {
            _attackSequence = new List<TurnAttackCard>();
            _defenseSequence = new List<PlayingCard>();
            TurnNumber = 1;
        }
        
        public IReadOnlyList<TurnAttackCard> AttackSequence => _attackSequence;
        public int AttackCardsCount => _attackSequence.Count;
        public int DefenseCardsCount { get; private set; }
        public int TurnNumber { get; private set; }
        
        public void AddAttackCard(TurnAttackCard card)
        {
            _attackSequence.Add(card);
            _defenseSequence.Add(null);
        }

        public void AddDefenseCard(PlayingCard card, int attackCardIndex)
        {
            if (attackCardIndex < 0 || attackCardIndex >= _attackSequence.Count)
            {
                throw new ArgumentException("Defense card must beat an existing attack card");
            }
            
            _defenseSequence[attackCardIndex] = card;
            DefenseCardsCount++;
        }

        public bool HasAttackCardWithRank(in RankComponent rank)
        {
            if (_attackSequence.Count == 0)
            {
                return false;
            }
            
            foreach (var x in _attackSequence)
            {
                if (x.AttackCard.HasRank(rank))
                {
                    return true;
                }
            }

            return false;
        }
        
        public bool HasCardWithRank(in RankComponent rank)
        {
            return CheckHasRankDefense(rank) || HasAttackCardWithRank(rank);
        }

        public TurnAttackCard GetTurnAttackCardAt(int index)
        {
            if (index < 0 || index >= _attackSequence.Count)
            {
                throw new ArgumentException("Attack index must be in range of attack cards added", nameof(index));
            }
            
            return _attackSequence[index];
        }

        public IEnumerable<PlayingCard> TakeCards()
        {
            var beatenCards = _defenseSequence.Where(x => x is not null);
            return _attackSequence.Select(x => x.AttackCard).Concat(beatenCards);
        }

        public bool IsAllCardsBeaten()
        {
            return _defenseSequence.TrueForAll(x => x is not null);
        }

        public void IncreaseTurnNumber()
        {
            TurnNumber++;
        }

        public void Clear()
        {
            _attackSequence.Clear();
            _defenseSequence.Clear();
            DefenseCardsCount = 0;
        }
        
        private bool CheckHasRankDefense(in RankComponent rank)
        {
            if (_defenseSequence.Count == 0)
            {
                return false;
            }
            
            foreach (var x in _defenseSequence)
            {
                if (x is not null && x.HasRank(rank))
                {
                    return true;
                }
            }

            return false;
        }
    }
}