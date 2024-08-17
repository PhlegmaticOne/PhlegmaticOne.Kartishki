using System;
using System.Collections.Generic;
using App.Scripts.Durak.Players.Base;
using Kartishki.Core;

namespace App.Scripts.Durak.Decks
{
    public class Deck : IDeck
    {
        private readonly Stack<PlayingCard> _deck;

        private Deck(Stack<PlayingCard> deck, PlayingCard trump)
        {
            Trump = trump;
            _deck = deck;
        }
        
        /// <summary>
        /// Enumerates default cards ordered by ranks from 6 to Ace with ranks mapped to each rank in order - Spades, Hearts, Diamonds, Clubs
        /// </summary>
        /// <returns>6♠, 6♥, 6♦, 6♣, 7♠, 7♥, 7♦, 7♣ ... A♠, A♥, A♦, A♣</returns>
        public static IEnumerable<PlayingCard> Enumerate36Cards()
        {
            return DeckEnumerator.Enumerate36Cards();
        }

        /// <summary>
        /// Enumerates default cards ordered by ranks from 2 to Ace with ranks mapped to each rank in order - Spades, Hearts, Diamonds, Clubs
        /// </summary>
        /// <returns>2♠, 2♥, 2♦, 2♣, 3♠, 3♥, 3♦, 3♣ ... A♠, A♥, A♦, A♣</returns>
        public static IEnumerable<PlayingCard> Enumerate52Cards()
        {
            return DeckEnumerator.Enumerate52Cards();
        }
        
        /// <summary>
        /// Enumerates default cards ordered by ranks from 6 to Ace with ranks mapped to each rank in order - Spades, Hearts, Diamonds, Clubs, including two Jokers
        /// </summary>
        /// <returns>6♠, 6♥, 6♦, 6♣, 7♠, 7♥, 7♦, 7♣ ... A♠, A♥, A♦, A♣, ★0, ★1</returns>
        public static IEnumerable<PlayingCard> Enumerate54Cards()
        {
            return DeckEnumerator.Enumerate54Cards();
        }

        public static Deck Create(params PlayingCard[] cards)
        {
            return Create(cards as IEnumerable<PlayingCard>);
        }
        
        public static Deck Create(IEnumerable<PlayingCard> cards)
        {
            var deckStack = new Stack<PlayingCard>();
            var trump = InitializeDeck(cards, deckStack);
            return new Deck(deckStack, trump);
        }

        public PlayingCard Trump { get; }
        public bool IsEmpty => _deck.Count == 0;
        public int CardsCount => _deck.Count;
        
        public void FillCardConsumer(IPlayingCardConsumer cardConsumer)
        {
            if (cardConsumer.IsOverfilled())
            {
                return;
            }
            
            while (!cardConsumer.IsFilled() && !IsEmpty)
            {
                var card = _deck.Pop();
                cardConsumer.PushCard(card);
            }
        }

        private static PlayingCard InitializeDeck(IEnumerable<PlayingCard> cards, Stack<PlayingCard> deckStack)
        {
            PlayingCard trump = null!;
            var isFirst = true;
            
            foreach (var card in cards)
            {
                if (isFirst)
                {
                    trump = card;
                    isFirst = false;
                }
                
                deckStack.Push(card);
            }

            if (trump is null)
            {
                throw new ArgumentException("Deck cannot be empty", nameof(cards));
            }

            return trump;
        }
    }
}