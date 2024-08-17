using System.Collections.Generic;
using App.Scripts.Durak.Decks.Extensions;
using App.Scripts.Durak.Players.Models;
using Kartishki.Core;
using Kartishki.Core.Components;

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

                if (minTrumpInHand is not null && minTrumpInHand.Card.Rank < minRank)
                {
                    minRank = minTrumpInHand.Card.Rank;
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
                if (card.Card.Rank < min)
                {
                    min = card.Card.Rank;
                    result = card;
                }
            }

            return result;
        }
    }
}