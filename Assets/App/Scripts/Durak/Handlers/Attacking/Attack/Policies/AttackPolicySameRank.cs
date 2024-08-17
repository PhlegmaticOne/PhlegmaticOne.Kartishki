using System.Linq;
using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;

namespace App.Scripts.Durak.Handlers.Attacking.Attack.Policies
{
    public class AttackPolicySameRank : IAttackPolicy
    {
        public bool CanAttack(AttackPolicyData policyData)
        {
            var turnCards = policyData.TurnCards;
            var attackCard = policyData.AttackCard;

            if (!policyData.Defender.HasCards)
            {
                return false;
            }
            
            return turnCards.AttackCardsCount == 0 || turnCards.HasAttackCardWithRank(attackCard.Card.Rank);
        }

        public bool IsAttackerCanBesiege(Attacker attacker, TurnCardsContainer cardsContainer)
        {
            if (cardsContainer.AttackSequence.Count == 0)
            {
                return false;
            }

            var player = attacker.Player;
            var attackCard = cardsContainer.AttackSequence[0].AttackCard;
            return player.HasCards && !player.Hand.GetCardsWithRank(attackCard.Card.Rank).Any();
        }
    }
}