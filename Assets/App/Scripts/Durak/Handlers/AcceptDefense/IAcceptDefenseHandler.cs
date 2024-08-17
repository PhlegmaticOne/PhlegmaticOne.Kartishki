using App.Scripts.Durak.Handlers.AcceptDefense.Results;

namespace App.Scripts.Durak.Handlers.AcceptDefense
{
    public interface IAcceptDefenseHandler
    {
        AcceptDefenseResult Handle(in AcceptDefenseHandlerData handlerData);
    }
}