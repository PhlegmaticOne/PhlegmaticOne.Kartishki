using App.Scripts.Durak.Handlers.FailDefense;
using App.Scripts.Durak.Handlers.FailDefense.Results;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Models;
using Moq;
using NUnit.Framework;

namespace App.Scripts.Tests.Durak.Handlers.FailDefense
{
    [TestFixture]
    public class FailDefenseHandlerTests
    {
        private static class Mocks
        {
            public static IDurakPlayersObserver ObserverWithDefender(Defender defender)
            {
                var mock = new Mock<IDurakPlayersObserver>();
                mock.SetupGet(x => x.Defender).Returns(defender);
                return mock.Object;
            }

            public static IDurakPlayersObserver ObserverWithPlayers(
                Defender defender, Attacker attacker, Attacker siegePlayer)
            {
                var mock = new Mock<IDurakPlayersObserver>();
                mock.SetupGet(x => x.Defender).Returns(defender);
                mock.SetupGet(x => x.Attacker).Returns(attacker);
                mock.SetupGet(x => x.SiegePlayers).Returns(new [] { attacker, siegePlayer });
                return mock.Object;
            }

            public static Attacker SiegePlayer()
            {
                var player = DurakPlayer.New.ToAttacker();
                player.IsAcceptedDefense = true;
                player.CanBesiege = true;
                player.CanAcceptDefense = false;
                return player;
            }
        }

        [Test]
        public void Handle_ShouldReturnPlayerIsNotDefender_WhenPlayerIsNotDefender()
        {
            //Arrange
            var defender = DurakPlayer.New.ToDefender();
            var other = DurakPlayer.New;
            var playersObserver = Mocks.ObserverWithDefender(defender);

            var failDefenseHandler = new FailDefenseHandler(playersObserver);

            //Act
            var result = failDefenseHandler.Handle(new FailDefenseHandlerData
            {
                Player = other
            });
            
            //Assert
            Assert.AreEqual(FailDefenseResult.PlayerNotDefender(), result);
        }
        
        [Test]
        public void Handle_ShouldReturnDefenderCantAccept_WhenPlayerIsFailingDefenseAlready()
        {
            //Arrange
            var defender = DurakPlayer.New.ToDefender();
            var playersObserver = Mocks.ObserverWithDefender(defender);

            var failDefenseHandler = new FailDefenseHandler(playersObserver);

            //Act
            defender.IsFailingDefense = true;
            var result = failDefenseHandler.Handle(new FailDefenseHandlerData
            {
                Player = defender.Player
            });
            
            //Assert
            Assert.AreEqual(FailDefenseResult.DefenderCantAccept(), result);
        }
        
        [Test]
        public void Handle_ShouldReturnDefenderCantAccept_WhenPlayerCantFailDefense()
        {
            //Arrange
            var defender = DurakPlayer.New.ToDefender();
            var playersObserver = Mocks.ObserverWithDefender(defender);

            var failDefenseHandler = new FailDefenseHandler(playersObserver);

            //Act
            defender.CanFailDefense = false;
            var result = failDefenseHandler.Handle(new FailDefenseHandlerData
            {
                Player = defender.Player
            });
            
            //Assert
            Assert.AreEqual(FailDefenseResult.DefenderCantAccept(), result);
        }
        
        [Test]
        public void Handle_ShouldStartDefenderFailingDefense_WhenPlayerCanFailDefense()
        {
            //Arrange
            var defender = DurakPlayer.New.ToDefender();
            var attacker = Mocks.SiegePlayer();
            var siegePlayer = Mocks.SiegePlayer();
            var playersObserver = Mocks.ObserverWithPlayers(defender, attacker, siegePlayer);

            var failDefenseHandler = new FailDefenseHandler(playersObserver);

            //Act
            defender.CanFailDefense = true;
            var result = failDefenseHandler.Handle(new FailDefenseHandlerData
            {
                Player = defender.Player
            });
            
            //Assert
            Assert.AreEqual(FailDefenseResult.Successful(), result);
            Assert.AreEqual(defender.IsFailingDefense, true);
        }
        
        [Test]
        public void Handle_ShouldDisableSiegePlayersIsAcceptedDefenseCanAcceptDefenseAndCanBesiege_WhenPlayerCanFailDefense()
        {
            //Arrange
            var defender = DurakPlayer.New.ToDefender();
            var attacker = Mocks.SiegePlayer();
            var siegePlayer = Mocks.SiegePlayer();
            var playersObserver = Mocks.ObserverWithPlayers(defender, attacker, siegePlayer);

            var failDefenseHandler = new FailDefenseHandler(playersObserver);

            //Act
            defender.CanFailDefense = true;
            var result = failDefenseHandler.Handle(new FailDefenseHandlerData
            {
                Player = defender.Player
            });
            
            //Assert
            Assert.AreEqual(FailDefenseResult.Successful(), result);
            Assert.AreEqual(false, siegePlayer.IsAcceptedDefense);
            Assert.AreEqual(false, siegePlayer.CanBesiege);
            Assert.AreEqual(false, siegePlayer.CanAcceptDefense);
        }
        
        [Test]
        public void Handle_ShouldDisableAttackerIsAcceptedDefenseButKeepCanAcceptDefenseAndCanBesiege_WhenPlayerCanFailDefense()
        {
            //Arrange
            var defender = DurakPlayer.New.ToDefender();
            var attacker = Mocks.SiegePlayer();
            var siegePlayer = Mocks.SiegePlayer();
            var playersObserver = Mocks.ObserverWithPlayers(defender, attacker, siegePlayer);

            var failDefenseHandler = new FailDefenseHandler(playersObserver);

            //Act
            defender.CanFailDefense = true;
            var result = failDefenseHandler.Handle(new FailDefenseHandlerData
            {
                Player = defender.Player
            });
            
            //Assert
            Assert.AreEqual(FailDefenseResult.Successful(), result);
            Assert.AreEqual(false, attacker.IsAcceptedDefense);
            Assert.AreEqual(true, attacker.CanBesiege);
            Assert.AreEqual(true, attacker.CanAcceptDefense);
        }
    }
}