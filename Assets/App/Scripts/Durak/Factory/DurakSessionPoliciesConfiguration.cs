using App.Scripts.Durak.Decks.Policies;
using App.Scripts.Durak.Handlers.Attacking.Attack.Policies;
using App.Scripts.Durak.Handlers.Attacking.Siege.Policies;
using App.Scripts.Durak.Handlers.Defense.Policies;
using App.Scripts.Durak.Handlers.Transfer.Policies;
using App.Scripts.Durak.Players.Policies.FirstAttacker;
using App.Scripts.Durak.Players.Policies.Siege;

namespace App.Scripts.Durak.Factory
{
    public class DurakSessionPoliciesConfiguration
    {
        public DurakSessionPoliciesConfiguration(
            IAttackPolicy attackPolicy, 
            IDefensePolicy defensePolicy, 
            ISiegePolicy siegePolicy, 
            ITransferPolicy transferPolicy, 
            ISiegePlayersPolicy siegePlayersPolicy,
            IDeckPolicy deckPolicy, IFirstAttackerPolicy firstAttackerPolicy)
        {
            AttackPolicy = attackPolicy;
            DefensePolicy = defensePolicy;
            SiegePolicy = siegePolicy;
            TransferPolicy = transferPolicy;
            SiegePlayersPolicy = siegePlayersPolicy;
            DeckPolicy = deckPolicy;
            FirstAttackerPolicy = firstAttackerPolicy;
        }

        public IAttackPolicy AttackPolicy { get; }
        public IDefensePolicy DefensePolicy { get; }
        public ISiegePolicy SiegePolicy { get; }
        public ITransferPolicy TransferPolicy { get; }
        public ISiegePlayersPolicy SiegePlayersPolicy { get; }
        public IDeckPolicy DeckPolicy { get; }
        public IFirstAttackerPolicy FirstAttackerPolicy { get; }
    }
}