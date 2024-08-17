using System;
using App.Scripts.Durak.Decks;
using App.Scripts.Durak.Decks.DiscardPiles;
using App.Scripts.Durak.Decks.Extensions;
using App.Scripts.Durak.Game;
using App.Scripts.Durak.Handlers.AcceptDefense;
using App.Scripts.Durak.Handlers.AcceptDefense.Commit;
using App.Scripts.Durak.Handlers.Attacking;
using App.Scripts.Durak.Handlers.Attacking.Attack;
using App.Scripts.Durak.Handlers.Attacking.Siege;
using App.Scripts.Durak.Handlers.Defense;
using App.Scripts.Durak.Handlers.FailDefense;
using App.Scripts.Durak.Handlers.Transfer;
using App.Scripts.Durak.Players.Circle;
using App.Scripts.Durak.Players.Policies.FirstAttacker;
using App.Scripts.Durak.Turns;

namespace App.Scripts.Durak.Factory
{
    public static class DurakSessionFactory
    {
        public static DurakSession Create(in DurakSessionFactoryData factoryData)
        {
            var id = Guid.NewGuid();
            var c = factoryData.PoliciesConfiguration;
            var deck = CreateDeck(factoryData);
            var discardPile = new DiscardPile();
            var turnCardsContainer = new TurnCardsContainer();
            var playersCircle = CreatePlayersCircle(factoryData, deck);
            var gameResultChecker = new DurakGameResultChecker(deck, playersCircle);
            var gameResultProvider = new DurakGameResultProvider(gameResultChecker);
            var attackingHandler = new AttackActionHandler(playersCircle,
                new AttackHandler(playersCircle, c.AttackPolicy, turnCardsContainer),
                new SiegeHandler(playersCircle, c.SiegePolicy, turnCardsContainer));
            
            deck.FillCardConsumers(playersCircle.AllPlayers);
            
            return new DurakSession(id, gameResultProvider, attackingHandler,
                new DefenseHandler(playersCircle, c.DefensePolicy, turnCardsContainer, deck),
                new TransferHandler(playersCircle, c.TransferPolicy, turnCardsContainer),
                new FailDefenseHandler(playersCircle),
                new AcceptDefenseHandler(playersCircle, turnCardsContainer,
                    new AcceptDefenseCommitHandlerSuccess(gameResultProvider, turnCardsContainer, playersCircle, deck, discardPile),
                    new AcceptDefenseCommitHandlerFail(gameResultProvider, turnCardsContainer, playersCircle, deck)));
        }
        
        private static IDeck CreateDeck(in DurakSessionFactoryData factoryData)
        {
            var deckPolicy = factoryData.PoliciesConfiguration.DeckPolicy;
            var deckCards = deckPolicy.GetDeckCards();
            return Deck.Create(deckCards);
        }

        private static DurakPlayersCircle CreatePlayersCircle(in DurakSessionFactoryData factoryData, IDeck deck)
        {
            var siegePlayersPolicy = factoryData.PoliciesConfiguration.SiegePlayersPolicy;
            var firstAttackerPolicy = factoryData.PoliciesConfiguration.FirstAttackerPolicy;
            
            var firstAttacker = firstAttackerPolicy.GetFirstAttacker(new FirstAttackerPolicyData
            {
                AllPlayers = factoryData.Players,
                Deck = deck
            });
            
            return new DurakPlayersCircle(siegePlayersPolicy, factoryData.Players, firstAttacker);
        }
    }
}