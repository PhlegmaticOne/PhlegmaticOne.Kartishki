using App.Scripts.Durak.Handlers.Attacking.Results;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Circle;

namespace App.Scripts.Durak.Handlers.Attacking
{
    public class AttackActionHandler : IAttackHandler
    {
        private readonly IDurakPlayersObserver _playersObserver;
        private readonly IAttackHandler _attackHandler;
        private readonly IAttackHandler _siegeHandler;

        public AttackActionHandler(
            IDurakPlayersObserver playersObserver,
            IAttackHandler attackHandler,
            IAttackHandler siegeHandler)
        {
            _playersObserver = playersObserver;
            _attackHandler = attackHandler;
            _siegeHandler = siegeHandler;
        }
        
        public AttackResult Handle(AttackHandlerData handlerData)
        {
            if (TryAttackAsAttacker(handlerData, out var result))
            {
                return result;
            }

            if (TryAttackAsSiegePlayer(handlerData, out result))
            {
                return result;
            }
            
            return AttackResult.InvalidAttackState();
        }

        private bool TryAttackAsAttacker(AttackHandlerData handlerData, out AttackResult attackResult)
        {
            var isAttacker = _playersObserver.TryGetAttacker(handlerData.Player, out var attacker);
                
            switch (isAttacker)
            {
                case true when attacker.CanPerformAttack():
                {
                    attackResult = _attackHandler.Handle(handlerData);
                    return true;
                }
                case true when attacker.CanPerformBesiege():
                {
                    attackResult = _siegeHandler.Handle(handlerData);
                    return true;
                }
                default:
                {
                    attackResult = AttackResult.InvalidAttackState();
                    return false;
                }
            }
        }

        private bool TryAttackAsSiegePlayer(AttackHandlerData handlerData, out AttackResult attackResult)
        {
            var isSiegePlayer = _playersObserver.TryGetSiegePlayer(handlerData.Player, out var siegePlayer);
                
            switch (isSiegePlayer)
            {
                case true when siegePlayer.CanPerformBesiege():
                {
                    attackResult = _siegeHandler.Handle(handlerData);
                    return true;
                }
                default:
                {
                    attackResult = AttackResult.InvalidAttackState();
                    return false;
                }
            }
        }
    }
}