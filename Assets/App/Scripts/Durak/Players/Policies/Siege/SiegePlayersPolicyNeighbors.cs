using System.Collections.Generic;
using App.Scripts.Durak.Extensions;
using App.Scripts.Durak.Players.Models;

namespace App.Scripts.Durak.Players.Policies.Siege
{
    public class SiegePlayersPolicyNeighbors : ISiegePlayersPolicy
    {
        public IEnumerable<Attacker> GetSiegePlayers(SiegePolicyData policyData)
        {
            var allPlayers = policyData.AllPlayers;
            var defencePlayer = policyData.Defender;
            var index = allPlayers.IndexOf(defencePlayer);
            var siegePlayerNext = allPlayers.AtIndexCyclical(index + 1);
            var siegePlayerPrevious = allPlayers.AtIndexCyclical(index - 1);

            yield return siegePlayerNext.ToAttacker(false);

            if (!siegePlayerNext.Equals(siegePlayerPrevious))
            {
                yield return siegePlayerPrevious.ToAttacker(false);
            }
        }
    }
}