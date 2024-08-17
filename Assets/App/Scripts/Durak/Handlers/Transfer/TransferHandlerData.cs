using App.Scripts.Durak.Players.Models;

namespace App.Scripts.Durak.Handlers.Transfer
{
    public struct TransferHandlerData
    {
        public DurakPlayer Player { get; set; }
        public int CardIndex { get; set; }
    }
}