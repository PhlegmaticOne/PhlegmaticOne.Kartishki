using App.Scripts.Durak.Decks.Extensions;
using Kartishki.Core;
using Kartishki.Core.Components;

namespace App.Scripts.Durak.Handlers.Defense.Policies
{
    public class DefensePolicyDefault : IDefensePolicy
    {
        public bool CanDefend(DefensePolicyData policyData)
        {
            var attackCard = policyData.AttackCard;
            var defenceCard = policyData.DefenseCard;
            var trumpSuit = policyData.Deck.GetTrumpSuit();

            return DefenseCardIsJokerWithAttackCardColor(defenceCard, attackCard) ||
                   DefenseCardRankGreaterThanAttackCardRank(defenceCard, attackCard) ||
                   DefenseCardIsTrumpAndCanBeatAttackCard(defenceCard, attackCard, trumpSuit);
        }

        private static bool DefenseCardIsJokerWithAttackCardColor(
            PlayingCard defenceCard, PlayingCard attackCard)
        {
            return defenceCard.IsJoker() && 
                   !attackCard.IsJoker() &&
                   attackCard.HasSameColor(defenceCard);
        }

        private static bool DefenseCardRankGreaterThanAttackCardRank(
            PlayingCard defenceCard, PlayingCard attackCard)
        {
            return defenceCard.HasSuit(attackCard.Card.Suit) &&
                   defenceCard.Card.Rank > attackCard.Card.Rank;
        }

        private static bool DefenseCardIsTrumpAndCanBeatAttackCard(
            PlayingCard defenceCard, PlayingCard attackCard, in SuitComponent trumpSuit)
        {
            if (attackCard.HasSuit(trumpSuit))
            {
                return defenceCard.Card.Rank > attackCard.Card.Rank;
            }
            
            return defenceCard.HasSuit(trumpSuit);
        }
    }
}