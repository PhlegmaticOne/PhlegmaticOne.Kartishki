using System.Collections.Generic;
using App.Scripts.Durak.Extensions;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Players.Policies.Siege;

namespace App.Scripts.Durak.Players.Circle
{
    public class DurakPlayersCircle : IDurakPlayersChanger
    {
        private readonly ISiegePlayersPolicy _siegePlayersPolicy;
        private readonly List<DurakPlayer> _allPlayers;
        private readonly List<Attacker> _siegePlayers;

        public DurakPlayersCircle(
            ISiegePlayersPolicy siegePlayersPolicy,
            List<DurakPlayer> allPlayers,
            DurakPlayer firstAttacker)
        {
            _allPlayers = allPlayers;
            _siegePlayersPolicy = siegePlayersPolicy;
            _siegePlayers = new List<Attacker>();
            Attacker = firstAttacker.ToAttacker();
            Defender = GetNextPlayer(firstAttacker).ToDefender();
            UpdateSiegePlayers();
        }
        
        public Defender Defender { get; private set; }
        public Attacker Attacker { get; private set; }
        public IReadOnlyList<DurakPlayer> AllPlayers => _allPlayers;
        public IReadOnlyList<Attacker> SiegePlayers => _siegePlayers;

        public DurakPlayer GetNextPlayer(DurakPlayer player)
        {
            var index = _allPlayers.IndexOf(player);
            return _allPlayers.AtIndexCyclical(index + 1);
        }

        public void ChangePlayersOnDefenceFailed()
        {
            MovePlayersNext(1);
        }

        public void ChangePlayersOnDefenceSucceed()
        {
            MovePlayersNext(0);
        }

        private void MovePlayersNext(int deltaMove)
        {
            var newAttackPlayerIndex = _allPlayers.IndexOf(Defender.Player) + deltaMove;

            Attacker = _allPlayers.AtIndexCyclical(newAttackPlayerIndex).ToAttacker();
            Defender = _allPlayers.AtIndexCyclical(newAttackPlayerIndex + 1).ToDefender();
            
            UpdateSiegePlayers();
        }

        private void UpdateSiegePlayers()
        {
            var newAttackPlayers = _siegePlayersPolicy.GetSiegePlayers(new SiegePolicyData
            {
                AllPlayers = _allPlayers,
                Defender = Defender.Player
            });
            
            _siegePlayers.UpdateRange(newAttackPlayers);
        }
    }
}