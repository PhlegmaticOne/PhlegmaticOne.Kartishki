using System.Collections.Generic;
using App.Scripts.Cards;
using App.Scripts.Cards.Components;
using App.Scripts.Durak.Decks.Extensions;
using App.Scripts.Durak.Players.Models;

namespace App.Scripts.Durak.Players.Policies.FirstAttacker
{
    public class FirstAttackerPolicyLowestTrump : IFirstAttackerPolicy
    {
        public DurakPlayer GetFirstAttacker(FirstAttackerPolicyData policyData)
        {
            var trumpSuit = policyData.Deck.GetTrumpSuit();
            var minRank = RankComponent.MaxPossible;
            DurakPlayer firstAttacker = null;

            foreach (var player in policyData.AllPlayers)
            {
                var trumpCardsInHand = player.Hand.GetCardsWithSuit(trumpSuit);
                var minTrumpInHand = GetMinTrumpInHand(trumpCardsInHand);

                if (minTrumpInHand is not null && minTrumpInHand.Rank < minRank)
                {
                    minRank = minTrumpInHand.Rank;
                    firstAttacker = player;
                }
            }

            return firstAttacker ?? policyData.AllPlayers[0];
        }

        private static PlayingCard GetMinTrumpInHand(IEnumerable<PlayingCard> cards)
        {
            PlayingCard result = null!;
            var min = RankComponent.MaxPossible;

            foreach (var card in cards)
            {
                if (card.Rank < min)
                {
                    min = card.Rank;
                    result = card;
                }
            }

            return result;
        }
    }
}