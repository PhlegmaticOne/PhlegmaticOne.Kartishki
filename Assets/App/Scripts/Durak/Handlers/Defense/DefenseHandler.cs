using App.Scripts.Durak.Decks;
using App.Scripts.Durak.Handlers.Defense.Policies;
using App.Scripts.Durak.Handlers.Defense.Results;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Circle;
using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;

namespace App.Scripts.Durak.Handlers.Defense
{
    public class DefenseHandler : IDefenseHandler
    {
        private readonly IDurakPlayersObserver _playersObserver;
        private readonly IDefensePolicy _defensePolicy;
        private readonly TurnCardsContainer _turnCardsContainer;
        private readonly IDeck _deck;

        public DefenseHandler(
            IDurakPlayersObserver playersObserver, 
            IDefensePolicy defensePolicy,
            TurnCardsContainer turnCardsContainer,
            IDeck deck)
        {
            _playersObserver = playersObserver;
            _defensePolicy = defensePolicy;
            _turnCardsContainer = turnCardsContainer;
            _deck = deck;
        }

        public DefenseResult Handle(in DefenseHandlerData handlerData)
        {
            if (!_playersObserver.TryGetDefender(handlerData.Player, out var defender))
            {
                return DefenseResult.PlayerNotDefender();
            }

            if (!defender.Player.HasCards)
            {
                return DefenseResult.InvalidGameState();
            }

            var isSuccessDefend = PerformDefense(defender, handlerData);
            return isSuccessDefend ? DefenseResult.Successful() : DefenseResult.InvalidDefenseCard();
        }

        private bool PerformDefense(Defender defender, in DefenseHandlerData handlerData)
        {
            if (!CanDefense(defender.Player, handlerData))
            {
                return false;
            }

            var attacker = DefendWithCard(defender.Player, handlerData);
            HandlePlayerCapabilities(defender, attacker);
            return true;
        }

        private bool CanDefense(DurakPlayer defender, in DefenseHandlerData handlerData)
        {
            return _defensePolicy.CanDefend(new DefensePolicyData
            {
                Deck = _deck,
                AttackCard = _turnCardsContainer.GetTurnAttackCardAt(handlerData.AttackCardIndex).AttackCard,
                DefenseCard = defender.GetCardAt(handlerData.DefenceCardIndex)
            });
        }

        private Attacker DefendWithCard(DurakPlayer defender, DefenseHandlerData handlerData)
        {
            var defenceCard = defender.PullCardAt(handlerData.DefenceCardIndex);
            _turnCardsContainer.AddDefenseCard(defenceCard, handlerData.AttackCardIndex);
            return _turnCardsContainer.GetTurnAttackCardAt(handlerData.AttackCardIndex).Attacker;
        }

        private void HandlePlayerCapabilities(Defender defender, Attacker attacker)
        {
            if (_turnCardsContainer.IsAllCardsBeaten())
            {
                defender.CanFailDefense = false;
                attacker.CanAcceptDefense = true;
            }
        }
    }
}