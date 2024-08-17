using App.Scripts.Durak.Players.Models;

namespace App.Scripts.Durak.Handlers.Defense
{
    public struct DefenseHandlerData
    {
        public DurakPlayer Player { get; set; }
        public int DefenceCardIndex { get; set; }
        public int AttackCardIndex { get; set; }
    }
}