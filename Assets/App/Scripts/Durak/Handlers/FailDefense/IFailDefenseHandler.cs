using App.Scripts.Durak.Handlers.FailDefense.Results;

namespace App.Scripts.Durak.Handlers.FailDefense
{
    public interface IFailDefenseHandler
    {
        FailDefenseResult Handle(in FailDefenseHandlerData handlerData);
    }
}