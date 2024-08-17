using App.Scripts.Durak.Handlers.Attacking.Results;

namespace App.Scripts.Durak.Handlers.Attacking
{
    public interface IAttackHandler
    {
        AttackResult Handle(AttackHandlerData handlerData);
    }
}