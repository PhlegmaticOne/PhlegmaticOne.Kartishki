using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;
using Kartishki.Core;

namespace App.Scripts.Durak.Handlers.Attacking.Siege.Policies
{
    public struct SiegePolicyData
    {
        public int TurnNumber { get; set; }
        public DurakPlayer Defender { get; set; }
        public PlayingCard SiegeCard { get; set; }
        public TurnCardsContainer TurnCards { get; set; }   
    }
}