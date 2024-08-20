using App.Scripts.Durak.Handlers.Attacking;
using App.Scripts.Durak.Handlers.Attacking.Results;
using App.Scripts.Durak.Handlers.Attacking.Siege;
using App.Scripts.Durak.Handlers.Attacking.Siege.Policies;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Extensions;
using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;
using Kartishki.Core;
using Moq;
using NUnit.Framework;

namespace App.Scripts.EditTests.Durak.Handlers.Attacking.Siege
{
    [TestFixture]
    public class SiegeHandlerTests
    {
        private static class Mocks
        {
            public static IDurakPlayersObserver ObserverWithSiegePlayerAndDefender(
                Attacker attacker = null, Defender defender = null)
            {
                var observerMock = new Mock<IDurakPlayersObserver>();
                observerMock.SetupGet(x => x.SiegePlayers).Returns(new[] { attacker });
                observerMock.SetupGet(x => x.Defender).Returns(defender);
                return observerMock.Object;
            }

            public static ISiegePolicy SiegePolicyWithResult(bool canSiege)
            {
                var policyMock = new Mock<ISiegePolicy>();
                
                policyMock
                    .Setup(x => x.CanBesiege(It.IsAny<SiegePolicyData>()))
                    .Returns(canSiege);
                
                return policyMock.Object;
            }
        }
        
        [Test]
        public void Handle_ShouldResultInvalidAttackState_WhenPlayerCantSiegeDueToSiegePolicy()
        {
            //Arrange
            var attacker = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceClubs).ToAttacker();
            var defender = DurakPlayer.New.ToDefender();

            var playersObserver = Mocks.ObserverWithSiegePlayerAndDefender(attacker, defender);
            var attackPolicy = Mocks.SiegePolicyWithResult(false);
            var cardsContainer = new TurnCardsContainer();
            
            var handler = new SiegeHandler(playersObserver, attackPolicy, cardsContainer);
            
            //Act
            var result = handler.Handle(new AttackHandlerData
            {
                Player = attacker.Player,
                CardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(AttackResult.InvalidAttackState(), result);
        }

        [Test]
        public void Handle_ShouldAddAttackCardToTable_WhenPlayerCanSiege()
        {
            //Arrange
            var attacker = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceClubs).ToAttacker();
            var defender = DurakPlayer.New.ToDefender();

            var playersObserver = Mocks.ObserverWithSiegePlayerAndDefender(attacker, defender);
            var attackPolicy = Mocks.SiegePolicyWithResult(true);
            var cardsContainer = new TurnCardsContainer();
            
            var handler = new SiegeHandler(playersObserver, attackPolicy, cardsContainer);
            
            //Act
            var result = handler.Handle(new AttackHandlerData
            {
                Player = attacker.Player,
                CardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(1, cardsContainer.AttackCardsCount);
            Assert.AreEqual(AttackResult.Successful(), result);
        }
        
        [Test]
        public void Handle_ShouldActivateDefenderFailDefenseAvailability_WhenPlayerCanSiege()
        {
            //Arrange
            var attacker = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceClubs).ToAttacker();
            var defender = DurakPlayer.New.ToDefender();

            var playersObserver = Mocks.ObserverWithSiegePlayerAndDefender(attacker, defender);
            var attackPolicy = Mocks.SiegePolicyWithResult(true);
            var cardsContainer = new TurnCardsContainer();
            
            var handler = new SiegeHandler(playersObserver, attackPolicy, cardsContainer);
            
            //Act
            _ = handler.Handle(new AttackHandlerData
            {
                Player = attacker.Player,
                CardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(true, defender.CheckCanFailDefense());
        }
        
        [Test]
        public void Handle_ShouldDeactivateSiegePlayerAcceptDefense_WhenPlayerCanSiege()
        {
            //Arrange
            var attacker = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceClubs).ToAttacker();
            var defender = DurakPlayer.New.ToDefender();

            var playersObserver = Mocks.ObserverWithSiegePlayerAndDefender(attacker, defender);
            var attackPolicy = Mocks.SiegePolicyWithResult(true);
            var cardsContainer = new TurnCardsContainer();
            
            var handler = new SiegeHandler(playersObserver, attackPolicy, cardsContainer);
            
            //Act
            _ = handler.Handle(new AttackHandlerData
            {
                Player = attacker.Player,
                CardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(false, attacker.CanAcceptDefense);
        }
    }
}