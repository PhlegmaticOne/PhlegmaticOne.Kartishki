using System.Collections.Generic;
using App.Scripts.Durak.Players.Models;

namespace App.Scripts.Durak.Players.Policies.Siege
{
    public struct SiegePolicyData
    {
        public DurakPlayer Defender { get; set; }
        public List<DurakPlayer> AllPlayers { get; set; }
    }
}