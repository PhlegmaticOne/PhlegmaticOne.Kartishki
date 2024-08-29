using System.Collections.Generic;
using App.Scripts.Cards;

namespace App.Scripts.Durak.Players.Hand
{
    public static class PlayerHandExtensions
    {
        public static void PushCards(this PlayerHand playerHand, params PlayingCard[] cards)
        {
            PushCards(playerHand, cards as IEnumerable<PlayingCard>);
        }
        
        public static void PushCards(this PlayerHand playerHand, IEnumerable<PlayingCard> cards)
        {
            foreach (var card in cards)
            {
                playerHand.PushCard(card);
            }
        }
    }
}