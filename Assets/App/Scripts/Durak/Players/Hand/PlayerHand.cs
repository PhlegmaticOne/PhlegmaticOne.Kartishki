using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Cards;
using App.Scripts.Cards.Components;

namespace App.Scripts.Durak.Players.Hand
{
    public class PlayerHand : IReadOnlyPlayerHand
    {
        private readonly List<PlayingCard> _cards = new();

        public PlayerHand(int capacity = int.MaxValue)
        {
            Capacity = capacity;
        }
        
        public int Capacity { get; }
        public int CardsCount => _cards.Count;

        public IEnumerable<PlayingCard> GetCardsWithSuit(SuitComponent suit)
        {
            return _cards.Where(card => card.HasSuit(suit));
        }

        public IEnumerable<PlayingCard> GetCardsWithRank(RankComponent rank)
        {
            return _cards.Where(x => x.HasRank(rank));
        }

        public void PushCard(PlayingCard playingCard)
        {
            _cards.Add(playingCard);
        }

        public PlayingCard PullCardAt(int index)
        {
            if (index < 0 || index >= _cards.Count)
            {
                throw new ArgumentException("Invalid card index");
            }

            var card = _cards[index];
            _cards.Remove(card);
            return card;
        }

        public PlayingCard GetCardAt(int index)
        {
            if (index < 0 || index >= _cards.Count)
            {
                throw new ArgumentException("Invalid card index");
            }
            
            return _cards[index];
        }
    }
}