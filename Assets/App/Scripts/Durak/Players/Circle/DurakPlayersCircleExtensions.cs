using System.Linq;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Models;

namespace App.Scripts.Durak.Players.Circle
{
    public static class DurakPlayersCircleExtensions
    {
        public static DurakPlayer DefenderPlayer(this IDurakPlayersObserver playersObserver)
        {
            return playersObserver.Defender.Player;
        }

        public static DurakPlayer AttackerPlayer(this IDurakPlayersObserver playersObserver)
        {
            return playersObserver.Attacker.Player;
        }
        
        public static bool TryGetDefender(this IDurakPlayersObserver playersObserver, DurakPlayer player, out Defender defender)
        {
            if (IsDefender(playersObserver, player))
            {
                defender = playersObserver.Defender;
                return true;
            }

            defender = null;
            return false;
        }
        
        public static bool TryGetAttacker(this IDurakPlayersObserver playersObserver, DurakPlayer player, out Attacker attacker)
        {
            if (IsAttacker(playersObserver, player))
            {
                attacker = playersObserver.Attacker;
                return true;
            }

            attacker = null;
            return false;
        }
        
        public static bool TryGetSiegePlayer(this IDurakPlayersObserver playersObserver, DurakPlayer player, out Attacker siegePlayer)
        {
            siegePlayer = playersObserver.SiegePlayers.FirstOrDefault(x => x.Player.Equals(player));
            return siegePlayer is not null;
        }
        
        public static Attacker GetSiegePlayer(this IDurakPlayersObserver playersObserver, DurakPlayer player)
        {
            return playersObserver.SiegePlayers.FirstOrDefault(x => x.Player.Equals(player));
        }
        
        public static bool IsDefender(this IDurakPlayersObserver playersCircle, DurakPlayer player)
        {
            return playersCircle.Defender.Player.Equals(player);
        }

        public static bool IsAttacker(this IDurakPlayersObserver playersCircle, DurakPlayer player)
        {
            return playersCircle.Attacker.Player.Equals(player);
        }

        public static bool IsSiegePlayer(this IDurakPlayersObserver playersCircle, DurakPlayer player)
        {
            return playersCircle.SiegePlayers.Any(siegePlayer => siegePlayer.Player.Equals(player));
        }
    }
}