using App.Scripts.Durak.Decks;
using App.Scripts.Durak.Game;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Extensions;
using App.Scripts.Durak.Players.Models;
using Kartishki.Core;
using Moq;
using NUnit.Framework;

namespace App.Scripts.Tests.Durak.Game
{
    [TestFixture]
    public class DurakGameResultCheckerTests
    {
        private static class Mocks
        {
            public static IDeck CreateEmptyDeck()
            {
                var mock = new Mock<IDeck>();
                mock.SetupGet(x => x.IsEmpty).Returns(true);
                return mock.Object;
            }
        
            public static IDeck CreateNotEmptyDeck()
            {
                var mock = new Mock<IDeck>();
                mock.SetupGet(x => x.IsEmpty).Returns(false);
                return mock.Object;
            }

            public static IDurakPlayersObserver CreateObserverWithDefenderAndAttacker(
                DurakPlayer defender, DurakPlayer attacker)
            {
                var mock = new Mock<IDurakPlayersObserver>();
                mock.SetupGet(x => x.Defender).Returns(new Defender(defender));
                mock.SetupGet(x => x.AllPlayers).Returns(new[] { defender, attacker });
                return mock.Object;
            }
        }
        
        [Test]
        public void CheckResult_ShouldBeActive_WhenDeckIsNotEmpty()
        {
            //Arrange
            var defender = DurakPlayer.New;
            var attacker = DurakPlayer.New;
            var mock = Mocks.CreateObserverWithDefenderAndAttacker(defender, attacker);
            var deck = Mocks.CreateNotEmptyDeck();
            var resultChecker = new DurakGameResultChecker(deck, mock);

            //Act
            var result = resultChecker.CheckResult();
            
            //Assert
            Assert.AreEqual(DurakGameResult.Active(), result);
        }

        [Test]
        public void CheckResult_ShouldBeLostWithDefender_BecauseAllPlayerHandsAreEmptyAndLastTurnWasMadeByDefender()
        {
            //Arrange
            var defender = DurakPlayer.New;
            var attacker = DurakPlayer.New;
            var mock = Mocks.CreateObserverWithDefenderAndAttacker(defender, attacker);
            var deck = Mocks.CreateEmptyDeck();
            var resultChecker = new DurakGameResultChecker(deck, mock);
            
            //Act
            var result = resultChecker.CheckResult();
            
            //Assert
            Assert.AreEqual(DurakGameResult.Lost(defender), result);
        }

        [Test]
        public void CheckResult_ShouldBeActive_WhenMoreThanOnePlayerWithCardsLeft()
        {
            //Arrange
            var defender = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceDiamonds);
            var attacker = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceClubs);
            var mock = Mocks.CreateObserverWithDefenderAndAttacker(defender, attacker);
            var deck = Mocks.CreateEmptyDeck();
            var resultChecker = new DurakGameResultChecker(deck, mock);
            
            //Act
            var result = resultChecker.CheckResult();
            
            //Assert
            Assert.AreEqual(DurakGameResult.Active(), result);
        }

        [Test]
        public void CheckResult_ShouldBeLostWithPlayerWithNotEmptyHand_WhenAllOtherPlayersHaveEmptyHands()
        {
            //Arrange
            var defender = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceDiamonds);
            var attacker = DurakPlayer.New.PushCards();
            var mock = Mocks.CreateObserverWithDefenderAndAttacker(defender, attacker);
            var deck = Mocks.CreateEmptyDeck();
            var resultChecker = new DurakGameResultChecker(deck, mock);
            
            //Act
            var result = resultChecker.CheckResult();
            
            //Assert
            Assert.AreEqual(DurakGameResult.Lost(defender), result);
        }
    }
}