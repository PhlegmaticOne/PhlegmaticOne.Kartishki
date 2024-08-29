using App.Scripts.Cards;
using App.Scripts.Durak.Handlers.Attacking;
using App.Scripts.Durak.Handlers.Attacking.Results;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Extensions;
using App.Scripts.Durak.Players.Models;
using Moq;
using NUnit.Framework;

namespace App.Scripts.EditTests.Durak.Handlers.Attacking
{
    [TestFixture]
    public class AttackActionHandlerTests
    {
        private static class Mocks
        {
            public static IAttackHandler MockHandleWithResult(AttackResult result)
            {
                var mock = new Mock<IAttackHandler>();
                mock.Setup(x => x.Handle(It.IsAny<AttackHandlerData>())).Returns(result);
                return mock.Object;
            }

            public static IDurakPlayersObserver ObserverWithAttackerAndSiegePlayer(
                Attacker attacker, Attacker siegePlayer)
            {
                var observerMock = new Mock<IDurakPlayersObserver>();
                observerMock.SetupGet(x => x.Attacker).Returns(attacker);
                observerMock.SetupGet(x => x.SiegePlayers).Returns(new[] { siegePlayer });
                return observerMock.Object;
            }
            
            public static IDurakPlayersObserver ObserverWithAttacker(Attacker attacker = null)
            {
                var observerMock = new Mock<IDurakPlayersObserver>();
                observerMock.SetupGet(x => x.Attacker).Returns(attacker);
                return observerMock.Object;
            }
            
            public static IDurakPlayersObserver ObserverWithSiegePlayer(Attacker attacker = null)
            {
                var observerMock = new Mock<IDurakPlayersObserver>();
                observerMock.SetupGet(x => x.SiegePlayers).Returns(new[] { attacker });
                return observerMock.Object;
            }
        }
        
        [Test]
        public void Handle_ShouldReturnInvalidAttackState_WhenPlayerIsNotAttackPlayer()
        {
            //Arrange
            var attacker = DurakPlayer.New.ToAttacker();
            var observer = Mocks.ObserverWithAttackerAndSiegePlayer(attacker, attacker);
            var attackHandler = Mocks.MockHandleWithResult(AttackResult.Successful());
            var siegeHandler = Mocks.MockHandleWithResult(AttackResult.Successful());

            var attackingHandler = new AttackActionHandler(observer, attackHandler, siegeHandler);

            //Act
            var result = attackingHandler.Handle(new AttackHandlerData
            {
                Player = DurakPlayer.New,
                CardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(AttackResult.InvalidAttackState(), result);
        }
        
        [Test]
        public void Handle_ShouldReturnInvalidAttackState_WhenAttackerCantAttackAndSiege()
        {
            //Arrange
            var attacker = DurakPlayer.New.ToAttacker();
            var siegePlayer = DurakPlayer.New.ToAttacker();
            var observer = Mocks.ObserverWithAttackerAndSiegePlayer(attacker, siegePlayer);
            var attackHandler = Mocks.MockHandleWithResult(AttackResult.Successful());
            var siegeHandler = Mocks.MockHandleWithResult(AttackResult.Successful());

            var attackingHandler = new AttackActionHandler(observer, attackHandler, siegeHandler);

            //Act
            var result = attackingHandler.Handle(new AttackHandlerData
            {
                Player = attacker.Player,
                CardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(AttackResult.InvalidAttackState(), result);
        }
        
        [Test]
        public void Handle_ShouldReturnSuccessfulFromAttack_WhenAttackerCanAttackAndCantSiege()
        {
            //Arrange
            var attacker = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceClubs).ToAttacker();
            var siegePlayer = DurakPlayer.New.ToAttacker();
            var observer = Mocks.ObserverWithAttackerAndSiegePlayer(attacker, siegePlayer);
            var attackHandler = Mocks.MockHandleWithResult(AttackResult.Successful());
            var siegeHandler = Mocks.MockHandleWithResult(AttackResult.InvalidGameState());

            var attackingHandler = new AttackActionHandler(observer, attackHandler, siegeHandler);

            //Act
            attacker.CanAttack = true;
            attacker.CanBesiege = false;
            var result = attackingHandler.Handle(new AttackHandlerData
            {
                Player = attacker.Player,
                CardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(AttackResult.Successful(), result);
        }
        
        [Test]
        public void Handle_ShouldReturnSuccessfulFromSiege_WhenAttackerCanSiegeAndCantAttack()
        {
            //Arrange
            var attacker = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceClubs).ToAttacker();
            var siegePlayer = DurakPlayer.New.ToAttacker();
            var observer = Mocks.ObserverWithAttackerAndSiegePlayer(attacker, siegePlayer);
            var attackHandler = Mocks.MockHandleWithResult(AttackResult.InvalidGameState());
            var siegeHandler = Mocks.MockHandleWithResult(AttackResult.Successful());

            var attackingHandler = new AttackActionHandler(observer, attackHandler, siegeHandler);

            //Act
            attacker.CanAttack = false;
            attacker.CanBesiege = true;
            var result = attackingHandler.Handle(new AttackHandlerData
            {
                Player = attacker.Player,
                CardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(AttackResult.Successful(), result);
        }

        [Test]
        public void Handle_ShouldReturnSuccessfulFromSiege_WhenSiegePlayerCanSiege()
        {
            //Arrange
            var attacker = DurakPlayer.New.ToAttacker();
            var siegePlayer = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceClubs).ToAttacker();
            var observer = Mocks.ObserverWithAttackerAndSiegePlayer(attacker, siegePlayer);
            var attackHandler = Mocks.MockHandleWithResult(AttackResult.InvalidGameState());
            var siegeHandler = Mocks.MockHandleWithResult(AttackResult.Successful());

            var attackingHandler = new AttackActionHandler(observer, attackHandler, siegeHandler);

            //Act
            siegePlayer.CanAttack = false;
            siegePlayer.CanBesiege = true;
            var result = attackingHandler.Handle(new AttackHandlerData
            {
                Player = siegePlayer.Player,
                CardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(AttackResult.Successful(), result);
        }
    }
}