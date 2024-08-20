using App.Scripts.Durak.Handlers.Attacking;
using App.Scripts.Durak.Handlers.Attacking.Attack;
using App.Scripts.Durak.Handlers.Attacking.Attack.Policies;
using App.Scripts.Durak.Handlers.Attacking.Results;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Extensions;
using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;
using Kartishki.Core;
using Moq;
using NUnit.Framework;

namespace App.Scripts.EditTests.Durak.Handlers.Attacking.Attack
{
    [TestFixture]
    public class AttackHandlerTests
    {
        private static class Mocks
        {
            public static IDurakPlayersObserver ObserverWithAttackerAndDefender(
                Attacker attacker = null, Defender defender = null)
            {
                var observerMock = new Mock<IDurakPlayersObserver>();
                observerMock.SetupGet(x => x.Attacker).Returns(attacker);
                observerMock.SetupGet(x => x.Defender).Returns(defender);
                return observerMock.Object;
            }

            public static IAttackPolicy AttackPolicyWithResults(
                bool canAttackResult, bool canSiegeResult)
            {
                var attackPolicyMock = new Mock<IAttackPolicy>();
                
                attackPolicyMock
                    .Setup(x => x.CanAttack(It.IsAny<AttackPolicyData>()))
                    .Returns(canAttackResult);
                
                attackPolicyMock
                    .Setup(x => x.IsAttackerCanBesiege(It.IsAny<Attacker>(), It.IsAny<TurnCardsContainer>()))
                    .Returns(canSiegeResult);
                
                return attackPolicyMock.Object;
            }
        }
        
        [Test]
        public void Handle_ShouldResultInvalidAttackState_WhenPlayerCantAttackDueToAttackPolicy()
        {
            //Arrange
            var attacker = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceClubs).ToAttacker();
            var defender = DurakPlayer.New.ToDefender();

            var playersObserver = Mocks.ObserverWithAttackerAndDefender(attacker, defender);
            var attackPolicy = Mocks.AttackPolicyWithResults(false, false);
            var cardsContainer = new TurnCardsContainer();
            
            var handler = new AttackHandler(playersObserver, attackPolicy, cardsContainer);
            
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
        public void Handle_ShouldAddAttackCardToTable_WhenPlayerCanAttack()
        {
            //Arrange
            var attacker = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceClubs).ToAttacker();
            var defender = DurakPlayer.New.ToDefender();

            var playersObserver = Mocks.ObserverWithAttackerAndDefender(attacker, defender);
            var attackPolicy = Mocks.AttackPolicyWithResults(true, false);
            var cardsContainer = new TurnCardsContainer();
            
            var handler = new AttackHandler(playersObserver, attackPolicy, cardsContainer);
            
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
        public void Handle_ShouldActivateDefenderFailDefenseAvailability_WhenPlayerCanAttack()
        {
            //Arrange
            var attacker = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceClubs).ToAttacker();
            var defender = DurakPlayer.New.ToDefender();

            var playersObserver = Mocks.ObserverWithAttackerAndDefender(attacker, defender);
            var attackPolicy = Mocks.AttackPolicyWithResults(true, false);
            var cardsContainer = new TurnCardsContainer();
            
            var handler = new AttackHandler(playersObserver, attackPolicy, cardsContainer);
            
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
        public void Handle_ShouldDeactivateAttackerAcceptDefenseAndSetExpectedSiege_WhenPlayerCanAttack(
            [Values(true, false)] bool canBesiegeExpected)
        {
            //Arrange
            var attacker = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceClubs).ToAttacker();
            var defender = DurakPlayer.New.ToDefender();

            var playersObserver = Mocks.ObserverWithAttackerAndDefender(attacker, defender);
            var attackPolicy = Mocks.AttackPolicyWithResults(true, canBesiegeExpected);
            var cardsContainer = new TurnCardsContainer();
            
            var handler = new AttackHandler(playersObserver, attackPolicy, cardsContainer);
            
            //Act
            _ = handler.Handle(new AttackHandlerData
            {
                Player = attacker.Player,
                CardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(canBesiegeExpected, attacker.CanBesiege);
            Assert.AreEqual(false, attacker.CanAcceptDefense);
        }
    }
}