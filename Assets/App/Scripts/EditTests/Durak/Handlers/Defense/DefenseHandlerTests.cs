using App.Scripts.Durak.Decks;
using App.Scripts.Durak.Handlers.Defense;
using App.Scripts.Durak.Handlers.Defense.Policies;
using App.Scripts.Durak.Handlers.Defense.Results;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Extensions;
using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;
using Kartishki.Core;
using Moq;
using NUnit.Framework;

namespace App.Scripts.EditTests.Durak.Handlers.Defense
{
    [TestFixture]
    public class DefenseHandlerTests
    {
        private static class Mocks
        {
            public static IDurakPlayersObserver ObserverWithDefender(Defender defender)
            {
                var mock = new Mock<IDurakPlayersObserver>();
                mock.SetupGet(x => x.Defender).Returns(defender);
                return mock.Object;
            }

            public static IDefensePolicy PolicyWithResult(bool result)
            {
                var mock = new Mock<IDefensePolicy>();
                mock.Setup(x => x.CanDefend(It.IsAny<DefensePolicyData>())).Returns(result);
                return mock.Object;
            }
            
            public static IDeck DeckMock()
            {
                return new Mock<IDeck>().Object;
            }
        }

        [Test]
        public void Handle_ShouldReturnPlayerIsNotDefender_WhenPlayerIsNotDefender()
        {
            //Arrange
            var defender = DurakPlayer.New.ToDefender();
            var other = DurakPlayer.New;
            var playersObserver = Mocks.ObserverWithDefender(defender);
            var deck = Mocks.DeckMock();
            var policy = Mocks.PolicyWithResult(false);
            var cardsContainer = new TurnCardsContainer();

            var defenseHandler = new DefenseHandler(playersObserver, policy, cardsContainer, deck);

            //Act
            var result = defenseHandler.Handle(new DefenseHandlerData
            {
                Player = other,
                AttackCardIndex = 0,
                DefenceCardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(DefenseResult.PlayerNotDefender(), result);
        }
        
        [Test]
        public void Handle_ShouldReturnInvalidGameState_WhenDefenderHasNoCards()
        {
            //Arrange
            var defender = DurakPlayer.New.ToDefender();
            var playersObserver = Mocks.ObserverWithDefender(defender);
            var deck = Mocks.DeckMock();
            var policy = Mocks.PolicyWithResult(false);
            var cardsContainer = new TurnCardsContainer();

            var defenseHandler = new DefenseHandler(playersObserver, policy, cardsContainer, deck);

            //Act
            var result = defenseHandler.Handle(new DefenseHandlerData
            {
                Player = defender.Player,
                AttackCardIndex = 0,
                DefenceCardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(DefenseResult.InvalidGameState(), result);
        }
        
        [Test]
        public void Handle_ShouldReturnInvalidDefenseCard_WhenDefensePolicyReturnsFalse()
        {
            //Arrange
            var defender = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceHearts).ToDefender();
            var attacker = DurakPlayer.New.ToAttacker();
            var playersObserver = Mocks.ObserverWithDefender(defender);
            var deck = Mocks.DeckMock();
            var policy = Mocks.PolicyWithResult(false);
            var cardsContainer = new TurnCardsContainer();
            cardsContainer.AddAttackCard(new TurnAttackCard(PlayingCard.Defaults.AceHearts, attacker));

            var defenseHandler = new DefenseHandler(playersObserver, policy, cardsContainer, deck);

            //Act
            var result = defenseHandler.Handle(new DefenseHandlerData
            {
                Player = defender.Player,
                AttackCardIndex = 0,
                DefenceCardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(DefenseResult.InvalidDefenseCard(), result);
        }
        
        [Test]
        public void Handle_ShouldRemovePlayerCardAndAddItToTable_WhenPlayerCanDefend()
        {
            //Arrange
            var defender = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceHearts).ToDefender();
            var attacker = DurakPlayer.New.ToAttacker();
            
            var playersObserver = Mocks.ObserverWithDefender(defender);
            var deck = Mocks.DeckMock();
            var policy = Mocks.PolicyWithResult(true);
            var cardsContainer = new TurnCardsContainer();
            cardsContainer.AddAttackCard(new TurnAttackCard(PlayingCard.Defaults.AceHearts, attacker));

            var defenseHandler = new DefenseHandler(playersObserver, policy, cardsContainer, deck);

            //Act
            var result = defenseHandler.Handle(new DefenseHandlerData
            {
                Player = defender.Player,
                AttackCardIndex = 0,
                DefenceCardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(DefenseResult.Successful(), result);
            Assert.AreEqual(0, defender.Player.Hand.CardsCount);
            Assert.AreEqual(1, cardsContainer.DefenseCardsCount);
        }
        
        [Test]
        public void Handle_ShouldDisableDefenderCanFailDefenseAndEnableAttackerCanAcceptDefense_WhenPlayerDefendAllCardsAtTable()
        {
            //Arrange
            var defender = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceHearts).ToDefender();
            var attacker = DurakPlayer.New.ToAttacker();
            
            var playersObserver = Mocks.ObserverWithDefender(defender);
            var deck = Mocks.DeckMock();
            var policy = Mocks.PolicyWithResult(true);
            var cardsContainer = new TurnCardsContainer();
            cardsContainer.AddAttackCard(new TurnAttackCard(PlayingCard.Defaults.AceHearts, attacker));

            var defenseHandler = new DefenseHandler(playersObserver, policy, cardsContainer, deck);

            //Act
            defender.CanFailDefense = true;
            attacker.CanAcceptDefense = false;
            var result = defenseHandler.Handle(new DefenseHandlerData
            {
                Player = defender.Player,
                AttackCardIndex = 0,
                DefenceCardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(DefenseResult.Successful(), result);
            Assert.IsTrue(attacker.CanAcceptDefense);
            Assert.IsFalse(defender.CanFailDefense);
        }
    }
}