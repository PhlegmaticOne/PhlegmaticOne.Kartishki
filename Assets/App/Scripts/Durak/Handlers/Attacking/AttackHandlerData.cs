using App.Scripts.Durak.Players.Models;

namespace App.Scripts.Durak.Handlers.Attacking
{
    public struct AttackHandlerData
    {
        public DurakPlayer Player { get; set; }
        public int CardIndex { get; set; }
    }
}