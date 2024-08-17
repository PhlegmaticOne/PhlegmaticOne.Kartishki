using App.Scripts.Durak.Handlers.Attacking.Attack.Policies;
using App.Scripts.Durak.Handlers.Attacking.Results;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;

namespace App.Scripts.Durak.Handlers.Attacking.Attack
{
    public class AttackHandler : IAttackHandler
    {
        private readonly IDurakPlayersObserver _playersObserver;
        private readonly IAttackPolicy _attackPolicy;
        private readonly TurnCardsContainer _turnCardsContainer;

        public AttackHandler(
            IDurakPlayersObserver playersObserver, 
            IAttackPolicy attackPolicy, 
            TurnCardsContainer turnCardsContainer)
        {
            _playersObserver = playersObserver;
            _attackPolicy = attackPolicy;
            _turnCardsContainer = turnCardsContainer;
        }

        public AttackResult Handle(AttackHandlerData handlerData)
        {
            var attacker = _playersObserver.Attacker;
            var isSuccessAttack = PerformAttack(attacker, handlerData);
            return isSuccessAttack ? AttackResult.Successful() : AttackResult.InvalidAttackState();
        }

        private bool PerformAttack(Attacker attacker, in AttackHandlerData handlerData)
        {
            if (!CanPerformAttack(attacker, in handlerData))
            {
                return false;
            }

            AddAttackerCardToTable(attacker, handlerData.CardIndex);
            HandlePlayerCapabilities(attacker);
            return true;
        }

        private bool CanPerformAttack(Attacker attacker, in AttackHandlerData handlerData)
        {
            return _attackPolicy.CanAttack(new AttackPolicyData
            {
                TurnCards = _turnCardsContainer,
                AttackCard = attacker.Player.GetCardAt(handlerData.CardIndex),
                Defender = _playersObserver.Defender.Player
            });
        }

        private void AddAttackerCardToTable(Attacker attacker, int cardIndex)
        {
            var attackCard = attacker.Player.PullCardAt(cardIndex);
            _turnCardsContainer.AddAttackCard(new TurnAttackCard(attackCard, attacker));
        }

        private void HandlePlayerCapabilities(Attacker attacker)
        {
            HandleDefenderCanFailDefense();
            HandleAttackerCanSiegeAndAcceptDefense(attacker);
        }

        private void HandleDefenderCanFailDefense()
        {
            _playersObserver.Defender.CanFailDefense = true;
        }

        private void HandleAttackerCanSiegeAndAcceptDefense(Attacker attacker)
        {
            attacker.CanBesiege = _attackPolicy.IsAttackerCanBesiege(attacker, _turnCardsContainer);
            _playersObserver.Attacker.DeactivateAcceptDefense();
        }
    }
}