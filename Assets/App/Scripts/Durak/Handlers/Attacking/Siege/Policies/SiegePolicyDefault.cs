namespace App.Scripts.Durak.Handlers.Attacking.Siege.Policies
{
    public class SiegePolicyDefault : ISiegePolicy
    {
        private readonly int _firstTurnMaxCardsCount;

        public SiegePolicyDefault(int firstTurnMaxCardsCount)
        {
            _firstTurnMaxCardsCount = firstTurnMaxCardsCount;
        }
        
        public bool CanBesiege(SiegePolicyData policyData)
        {
            var turnCards = policyData.TurnCards;
            var siegeCard = policyData.SiegeCard;

            if (policyData.TurnNumber == 1 && turnCards.AttackCardsCount >= _firstTurnMaxCardsCount)
            {
                return false;
            }
            
            return policyData.Defender.HasCards && turnCards.HasCardWithRank(siegeCard.Card.Rank);
        }
    }
}