using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;
using Kartishki.Core;

namespace App.Scripts.Durak.Handlers.Transfer.Policies
{
    public struct TransferPolicyData
    {
        public DurakPlayer NextPlayer { get; set; }
        public PlayingCard Card { get; set; }
        public TurnCardsContainer TurnCards { get; set; }
    }
}