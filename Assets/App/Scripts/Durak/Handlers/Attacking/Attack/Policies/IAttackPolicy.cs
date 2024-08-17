using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;

namespace App.Scripts.Durak.Handlers.Attacking.Attack.Policies
{
    public interface IAttackPolicy
    {
        bool CanAttack(AttackPolicyData policyData);
        bool IsAttackerCanBesiege(Attacker attacker, TurnCardsContainer cardsContainer);
    }
}