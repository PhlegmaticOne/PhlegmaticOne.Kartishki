using App.Scripts.Durak.Handlers.Attacking.Results;
using App.Scripts.Durak.Handlers.Attacking.Siege.Policies;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Circle;
using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;

namespace App.Scripts.Durak.Handlers.Attacking.Siege
{
    public class SiegeHandler : IAttackHandler
    {
        private readonly IDurakPlayersObserver _playersObserver;
        private readonly ISiegePolicy _siegePolicy;
        private readonly TurnCardsContainer _turnCardsContainer;

        public SiegeHandler(
            IDurakPlayersObserver playersObserver,
            ISiegePolicy siegePolicy,
            TurnCardsContainer turnCardsContainer)
        {
            _playersObserver = playersObserver;
            _siegePolicy = siegePolicy;
            _turnCardsContainer = turnCardsContainer;
        }

        public AttackResult Handle(AttackHandlerData handlerData)
        {
            var siegePlayer = _playersObserver.GetSiegePlayer(handlerData.Player);
            var isSuccessBesiege = PerformSiege(siegePlayer, handlerData);
            return isSuccessBesiege ? AttackResult.Successful() : AttackResult.InvalidAttackState();
        }

        private bool PerformSiege(Attacker siegePlayer, in AttackHandlerData handlerData)
        {
            if (!CanBesiege(siegePlayer, in handlerData))
            {
                return false;
            }
        
            AddSiegePlayerCardToTable(siegePlayer, handlerData.CardIndex);
            HandlePlayersCapabilities(siegePlayer);
            return true;
        }

        private bool CanBesiege(Attacker siegePlayer, in AttackHandlerData handlerData)
        {
            return _siegePolicy.CanBesiege(new SiegePolicyData
            {
                TurnCards = _turnCardsContainer,
                SiegeCard = siegePlayer.Player.GetCardAt(handlerData.CardIndex),
                Defender = _playersObserver.Defender.Player,
                TurnNumber = _turnCardsContainer.TurnNumber
            });
        }
        
        private void AddSiegePlayerCardToTable(Attacker attacker, int cardIndex)
        {
            var attackCard = attacker.Player.PullCardAt(cardIndex);
            _turnCardsContainer.AddAttackCard(new TurnAttackCard(attackCard, attacker));
        }

        private void HandlePlayersCapabilities(Attacker siegePlayer)
        {
            _playersObserver.Defender.CanFailDefense = true;
            siegePlayer.DeactivateAcceptDefense();
        }
    }
}