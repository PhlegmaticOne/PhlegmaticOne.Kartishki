using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;

namespace App.Scripts.Durak.Handlers.Transfer.Policies
{
    public class TransferPolicyDefault : ITransferPolicy
    {
        public bool CanTransfer(TransferPolicyData policyData)
        {
            var card = policyData.Card;
            var turnCards = policyData.TurnCards;
            var nextPlayer = policyData.NextPlayer;
            
            return NextPlayerHasEnoughCards(nextPlayer, turnCards) && 
                   turnCards.DefenseCardsCount == 0 &&
                   turnCards.HasAttackCardWithRank(card.Card.Rank);
        }

        private static bool NextPlayerHasEnoughCards(DurakPlayer nextPlayer, TurnCardsContainer turnCards)
        {
            return nextPlayer.Hand.CardsCount > turnCards.AttackCardsCount + 1;
        }
    }
}