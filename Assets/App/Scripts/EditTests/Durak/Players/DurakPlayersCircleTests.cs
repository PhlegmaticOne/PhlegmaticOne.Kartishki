using System.Collections.Generic;
using System.Linq;
using App.Scripts.Durak.Extensions;
using App.Scripts.Durak.Players.Circle;
using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Players.Policies.Siege;
using Moq;
using NUnit.Framework;

namespace App.Scripts.Tests.Durak.Players
{
    [TestFixture]
    public class DurakPlayersCircleTests
    {
        private static class Mocks
        {
            public static ISiegePlayersPolicy GetSiegePlayersPolicy(List<DurakPlayer> siegePlayers)
            {
                var mock = new Mock<ISiegePlayersPolicy>();
                mock.Setup(x => x.GetSiegePlayers(It.IsAny<SiegePolicyData>())).Returns(siegePlayers.ToAttackers());
                return mock.Object;
            }
        }
        
        [Test]
        public void Constructor_ShouldSetupDefenderNextToAttackerAndInitialSiegePlayers()
        {
            //Arrange
            var defender = DurakPlayer.New;
            var attacker = DurakPlayer.New;
            var siegePlayers = new List<DurakPlayer> { attacker };
            var siegePlayersPolicy = Mocks.GetSiegePlayersPolicy(siegePlayers);

            //Act
            var playersCircle = new DurakPlayersCircle(
                siegePlayersPolicy, new List<DurakPlayer> { defender, attacker }, attacker);
            
            //Assert
            Assert.AreEqual(defender, playersCircle.Defender.Player);
            Assert.AreEqual(attacker, playersCircle.Attacker.Player);
            Assert.IsTrue(playersCircle.SiegePlayers.Select(x => x.Player).SequenceEqual(siegePlayers));
        }

        [Test]
        public void ChangePlayersOnDefenceFailed_ShouldCorrectChangeAttackerAndDefender_When2Players()
        {
            //Arrange
            var defender = DurakPlayer.New;
            var attacker = DurakPlayer.New;
            var siegePlayersPolicy = new SiegePlayersPolicyNeighbors();
            var playersCircle = new DurakPlayersCircle(
                siegePlayersPolicy, new List<DurakPlayer> { defender, attacker }, attacker);
            
            //Act
            playersCircle.ChangePlayersOnDefenceFailed();

            //Assert
            Assert.AreEqual(attacker, playersCircle.Attacker.Player);
            Assert.AreEqual(defender, playersCircle.Defender.Player);
        }
        
        [Test]
        public void ChangePlayersOnDefenceFailed_ShouldCorrectChangeAttackerAndDefender_When3Players()
        {
            //Arrange
            var defender = DurakPlayer.New;
            var attacker = DurakPlayer.New;
            var player1 = DurakPlayer.New;
            var siegePlayersPolicy = new SiegePlayersPolicyNeighbors();
            var playersCircle = new DurakPlayersCircle(
                siegePlayersPolicy, new List<DurakPlayer> { defender, player1, attacker }, attacker);
            
            //Act
            playersCircle.ChangePlayersOnDefenceFailed();

            //Assert
            Assert.AreEqual(player1, playersCircle.Attacker.Player);
            Assert.AreEqual(attacker, playersCircle.Defender.Player);
        }
        
        [Test]
        public void ChangePlayersOnDefenceFailed_ShouldCorrectChangeAttackerAndDefender_When4Players()
        {
            //Arrange
            var defender = DurakPlayer.New;
            var attacker = DurakPlayer.New;
            var player1 = DurakPlayer.New;
            var player2 = DurakPlayer.New;
            var siegePlayersPolicy = new SiegePlayersPolicyNeighbors();
            var playersCircle = new DurakPlayersCircle(
                siegePlayersPolicy, new List<DurakPlayer> { player1, player2, attacker, defender }, attacker);
            
            //Act
            playersCircle.ChangePlayersOnDefenceFailed();

            //Assert
            Assert.AreEqual(player1, playersCircle.Attacker.Player);
            Assert.AreEqual(player2, playersCircle.Defender.Player);
        }
        
        [Test]
        public void ChangePlayersOnDefenceSucceed_ShouldCorrectChangeAttackerAndDefender_When2Players()
        {
            //Arrange
            var defender = DurakPlayer.New;
            var attacker = DurakPlayer.New;
            var siegePlayersPolicy = new SiegePlayersPolicyNeighbors();
            var playersCircle = new DurakPlayersCircle(
                siegePlayersPolicy, new List<DurakPlayer> { defender, attacker }, attacker);
            
            //Act
            playersCircle.ChangePlayersOnDefenceSucceed();

            //Assert
            Assert.AreEqual(defender, playersCircle.Attacker.Player);
            Assert.AreEqual(attacker, playersCircle.Defender.Player);
        }
        
        [Test]
        public void ChangePlayersOnDefenceSucceed_ShouldCorrectChangeAttackerAndDefender_When3Players()
        {
            //Arrange
            var defender = DurakPlayer.New;
            var attacker = DurakPlayer.New;
            var player1 = DurakPlayer.New;
            var siegePlayersPolicy = new SiegePlayersPolicyNeighbors();
            var playersCircle = new DurakPlayersCircle(
                siegePlayersPolicy, new List<DurakPlayer> { defender, player1, attacker }, attacker);
            
            //Act
            playersCircle.ChangePlayersOnDefenceSucceed();

            //Assert
            Assert.AreEqual(defender, playersCircle.Attacker.Player);
            Assert.AreEqual(player1, playersCircle.Defender.Player);
        }
        
        [Test]
        public void ChangePlayersOnDefenceSucceed_ShouldCorrectChangeAttackerAndDefender_When4Players()
        {
            //Arrange
            var defender = DurakPlayer.New;
            var attacker = DurakPlayer.New;
            var player1 = DurakPlayer.New;
            var player2 = DurakPlayer.New;
            var siegePlayersPolicy = new SiegePlayersPolicyNeighbors();
            var playersCircle = new DurakPlayersCircle(
                siegePlayersPolicy, new List<DurakPlayer> { player1, player2, attacker, defender }, attacker);
            
            //Act
            playersCircle.ChangePlayersOnDefenceSucceed();

            //Assert
            Assert.AreEqual(defender, playersCircle.Attacker.Player);
            Assert.AreEqual(player1, playersCircle.Defender.Player);
        }

        [Test]
        public void GetNextPlayer_ShouldReturnIndexOfNextPlayer()
        {
            //Arrange
            var player1 = DurakPlayer.New;
            var player2 = DurakPlayer.New;
            var player3 = DurakPlayer.New;
            var siegePlayersPolicy = new SiegePlayersPolicyNeighbors();
            var playersCircle = new DurakPlayersCircle(
                siegePlayersPolicy, new List<DurakPlayer> { player1, player2, player3 }, player1);

            //Act
            var nextPlayer1 = playersCircle.GetNextPlayer(player1);
            var nextPlayer2 = playersCircle.GetNextPlayer(player2);
            var nextPlayer3 = playersCircle.GetNextPlayer(player3);
            
            //Assert
            Assert.AreEqual(player2, nextPlayer1);
            Assert.AreEqual(player3, nextPlayer2);
            Assert.AreEqual(player1, nextPlayer3);
        }
    }
}