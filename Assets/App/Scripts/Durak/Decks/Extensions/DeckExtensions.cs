using System.Collections.Generic;
using App.Scripts.Cards.Components;
using App.Scripts.Durak.Players.Base;

namespace App.Scripts.Durak.Decks.Extensions
{
    public static class DeckExtensions
    {
        public static void FillCardConsumers(this IDeck deck, IEnumerable<IPlayingCardConsumer> destinations)
        {
            foreach (var destination in destinations)
            {
                deck.FillCardConsumer(destination);
            }
        }

        public static SuitComponent GetTrumpSuit(this IDeck deck)
        {
            return deck.Trump.Suit;
        }
    }
}