using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;
using Kartishki.Core;

namespace App.Scripts.Durak.Handlers.Attacking.Attack.Policies
{
    public struct AttackPolicyData
    {
        public DurakPlayer Defender { get; set; }
        public PlayingCard AttackCard { get; set; }
        public TurnCardsContainer TurnCards { get; set; }
    }
}