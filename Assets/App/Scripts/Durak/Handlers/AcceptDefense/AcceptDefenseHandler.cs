using System.Linq;
using App.Scripts.Durak.Extensions;
using App.Scripts.Durak.Handlers.AcceptDefense.Commit;
using App.Scripts.Durak.Handlers.AcceptDefense.Results;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Circle;
using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;

namespace App.Scripts.Durak.Handlers.AcceptDefense
{
    public class AcceptDefenseHandler : IAcceptDefenseHandler
    {
        private readonly IDurakPlayersObserver _playersObserver;
        private readonly TurnCardsContainer _turnCardsContainer;
        private readonly IAcceptDefenseCommitHandler _commitHandlerSuccess;
        private readonly IAcceptDefenseCommitHandler _commitHandlerFail;

        public AcceptDefenseHandler(
            IDurakPlayersObserver playersObserver,
            TurnCardsContainer turnCardsContainer,
            IAcceptDefenseCommitHandler commitHandlerSuccess,
            IAcceptDefenseCommitHandler commitHandlerFail)
        {
            _playersObserver = playersObserver;
            _turnCardsContainer = turnCardsContainer;
            _commitHandlerSuccess = commitHandlerSuccess;
            _commitHandlerFail = commitHandlerFail;
        }

        public AcceptDefenseResult Handle(in AcceptDefenseHandlerData handlerData)
        {
            if (!TryAcceptDefense(handlerData.Player))
            {
                return AcceptDefenseResult.Unaccepted();
            }
            
            if (!IsAllPlayersAcceptedDefense())
            {
                return AcceptDefenseResult.Accepted();
            }
            
            return HandleAcceptResult();
        }

        private bool TryAcceptDefense(DurakPlayer player)
        {
            if (!CanHandleAcceptDefense(player, out var attackPlayer))
            {
                return false;
            }
            
            attackPlayer.IsAcceptedDefense = true;
            HandleNextSiegePlayerBesiegeCapability(attackPlayer);
            return true;
        }

        private AcceptDefenseResult HandleAcceptResult()
        {
            switch (_playersObserver.Defender.IsFailingDefense)
            {
                case false when _turnCardsContainer.IsAllCardsBeaten():
                    _commitHandlerSuccess.Commit();
                    return AcceptDefenseResult.ResultSuccessDefense();
                case true:
                    _commitHandlerFail.Commit();
                    return AcceptDefenseResult.ResultFailDefense();
                default:
                    return AcceptDefenseResult.Unaccepted();
            }
        }

        private void HandleNextSiegePlayerBesiegeCapability(Attacker currentSiegePlayer)
        {
            var currentSiegePlayerIndex = _playersObserver.SiegePlayers.IndexOf(currentSiegePlayer);
            var nextPlayerIndex = currentSiegePlayerIndex + 1;

            if (nextPlayerIndex >= _playersObserver.SiegePlayers.Count)
            {
                return;
            }

            _playersObserver.SiegePlayers[nextPlayerIndex].CanBesiege = true;
        }

        private bool CanHandleAcceptDefense(DurakPlayer player, out Attacker attackPlayer)
        {
            var isAttackPlayer = _playersObserver.TryGetAttacker(player, out attackPlayer) || 
                                 _playersObserver.TryGetSiegePlayer(player, out attackPlayer);

            return isAttackPlayer && attackPlayer.CanAcceptDefense;
        }
        
        private bool IsAllPlayersAcceptedDefense()
        {
            return _playersObserver.Attacker.IsAcceptedDefense &&
                   _playersObserver.SiegePlayers.All(x => x.IsAcceptedDefense);
        }
    }
}