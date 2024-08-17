using App.Scripts.Durak.Handlers.FailDefense.Results;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Circle;
using App.Scripts.Durak.Players.Models;

namespace App.Scripts.Durak.Handlers.FailDefense
{
    public class FailDefenseHandler : IFailDefenseHandler
    {
        private readonly IDurakPlayersObserver _playersObserver;

        public FailDefenseHandler(IDurakPlayersObserver playersObserver)
        {
            _playersObserver = playersObserver;
        }
        
        public FailDefenseResult Handle(in FailDefenseHandlerData handlerData)
        {
            if (!_playersObserver.TryGetDefender(handlerData.Player, out var defender))
            {
                return FailDefenseResult.PlayerNotDefender();
            }

            if (!defender.CheckCanFailDefense())
            {
                return FailDefenseResult.DefenderCantAccept();
            }
            
            HandlePlayersCapabilities(defender);
            return FailDefenseResult.Successful();
        }

        private void HandlePlayersCapabilities(Defender defender)
        {
            HandleDefenderIsFailingDefense(defender);
            HandleSiegePlayersCantAcceptDefenseAndCantSiege();
            HandleAttackerCanSiegeAndMustAcceptDefense();
        }

        private void HandleSiegePlayersCantAcceptDefenseAndCantSiege()
        {
            foreach (var siegePlayer in _playersObserver.SiegePlayers)
            {
                siegePlayer.DeactivateAll();
            }
        }

        private void HandleAttackerCanSiegeAndMustAcceptDefense()
        {
            var attacker = _playersObserver.Attacker;
            attacker.CanBesiege = true;
            attacker.CanAcceptDefense = true;
            attacker.IsAcceptedDefense = false;
        }

        private static void HandleDefenderIsFailingDefense(Defender defender)
        {
            defender.IsFailingDefense = true;
        }
    }
}