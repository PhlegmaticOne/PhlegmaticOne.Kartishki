using App.Scripts.Durak.Handlers.Defense.Results;

namespace App.Scripts.Durak.Handlers.Defense
{
    public interface IDefenseHandler
    {
        DefenseResult Handle(in DefenseHandlerData handlerData);
    }
}