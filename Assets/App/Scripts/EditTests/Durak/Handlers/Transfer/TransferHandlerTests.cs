using App.Scripts.Cards;
using App.Scripts.Durak.Handlers.Transfer;
using App.Scripts.Durak.Handlers.Transfer.Policies;
using App.Scripts.Durak.Handlers.Transfer.Results;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Extensions;
using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;
using Moq;
using NUnit.Framework;

namespace App.Scripts.EditTests.Durak.Handlers.Transfer
{
    [TestFixture]
    public class TransferHandlerTests
    {
        private static class Mocks
        {
            public static IDurakPlayersChanger ChangerWithDefenderAndAttacker(
                Defender defender, Attacker attacker = null)
            {
                return ChangerWithDefenderAndAttackerMock(defender, attacker).Object;
            }
            
            public static Mock<IDurakPlayersChanger> ChangerWithDefenderAndAttackerMock(
                Defender defender, Attacker attacker = null)
            {
                var mock = new Mock<IDurakPlayersChanger>();
                mock.SetupGet(x => x.Defender).Returns(defender);
                mock.SetupGet(x => x.Attacker).Returns(attacker);
                return mock;
            }

            public static ITransferPolicy TransferPolicyWithResult(bool result)
            {
                var mock = new Mock<ITransferPolicy>();
                mock.Setup(x => x.CanTransfer(It.IsAny<TransferPolicyData>())).Returns(result);
                return mock.Object;
            }
        }
        
        [Test]
        public void Handle_ShouldReturnPlayerNotDefender_WhenPlayerNotDefender()
        {
            //Arrange
            var defender = DurakPlayer.New.ToDefender();
            var playersChanger = Mocks.ChangerWithDefenderAndAttacker(defender);
            var policy = Mocks.TransferPolicyWithResult(true);
            var turnCards = new TurnCardsContainer();
            var handler = new TransferHandler(playersChanger, policy, turnCards);

            //Act
            var result = handler.Handle(new TransferHandlerData
            {
                Player = DurakPlayer.New,
                CardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(TransferResult.PlayerNotDefender(), result);
        }
        
        [Test]
        public void Handle_ShouldReturnInvalidTransferCard_WhenPlayerCannotTransferDueToPolicy()
        {
            //Arrange
            var defender = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceClubs).ToDefender();
            var playersChanger = Mocks.ChangerWithDefenderAndAttacker(defender);
            var policy = Mocks.TransferPolicyWithResult(false);
            var turnCards = new TurnCardsContainer();
            var handler = new TransferHandler(playersChanger, policy, turnCards);

            //Act
            var result = handler.Handle(new TransferHandlerData
            {
                Player = defender.Player,
                CardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(TransferResult.InvalidTransferCard(), result);
        }
        
        [Test]
        public void Handle_ShouldChangePlayerSameAsWhenTurnWasSucceed_WhenPlayerTransferred()
        {
            //Arrange
            var player = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceClubs);
            var defender = player.ToDefender();
            var attacker = player.ToAttacker();
            
            var playersChanger = Mocks.ChangerWithDefenderAndAttackerMock(defender, attacker);
            var policy = Mocks.TransferPolicyWithResult(true);
            var turnCards = new TurnCardsContainer();
            var handler = new TransferHandler(playersChanger.Object, policy, turnCards);

            //Act
            var result = handler.Handle(new TransferHandlerData
            {
                Player = defender.Player,
                CardIndex = 0
            });
            
            //Assert
            Assert.AreEqual(TransferResult.Successful(), result);
            playersChanger.Verify(x => x.ChangePlayersOnDefenceSucceed(), Times.Once);
        }
        
        [Test]
        public void Handle_ShouldAddAttackCardToTable_WhenPlayerTransferred()
        {
            //Arrange
            var transferCard = PlayingCard.Defaults.AceClubs;
            var player = DurakPlayer.New.PushCards(transferCard);
            var defender = player.ToDefender();
            var attacker = player.ToAttacker();
            
            var playersChanger = Mocks.ChangerWithDefenderAndAttackerMock(defender, attacker);
            var policy = Mocks.TransferPolicyWithResult(true);
            var turnCards = new TurnCardsContainer();
            var handler = new TransferHandler(playersChanger.Object, policy, turnCards);

            //Act
            var result = handler.Handle(new TransferHandlerData
            {
                Player = defender.Player,
                CardIndex = 0
            });

            //Assert
            var attackCard = turnCards.GetTurnAttackCardAt(0);
            Assert.AreEqual(TransferResult.Successful(), result);
            Assert.AreEqual(1, turnCards.AttackCardsCount);
            Assert.AreEqual(attackCard.AttackCard, transferCard);
        }
    }
}