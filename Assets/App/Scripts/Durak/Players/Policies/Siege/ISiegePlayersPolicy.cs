using System.Collections.Generic;
using App.Scripts.Durak.Players.Models;

namespace App.Scripts.Durak.Players.Policies.Siege
{
    public interface ISiegePlayersPolicy
    {
        IEnumerable<Attacker> GetSiegePlayers(SiegePolicyData policyData);
    }
}