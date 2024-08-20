using App.Scripts.Durak.Handlers.Transfer.Policies;
using App.Scripts.Durak.Players.Extensions;
using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;
using Kartishki.Core;
using NUnit.Framework;

namespace App.Scripts.EditTests.Durak.Handlers.Transfer.Policies
{
    [TestFixture]
    public class TransferPolicyTests
    {
        [Test]
        public void Default_ShouldReturnFalse_WhenNextPlayerHasLessCardsThanCardsCountAtTablePlusOneTransferCard()
        {
            //Arrange
            var policy = new TransferPolicyDefault();
            var turnCards = new TurnCardsContainer();
            turnCards.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.AceDiamonds));

            //Act
            var result = policy.CanTransfer(new TransferPolicyData
            {
                Card = PlayingCard.Defaults.AceClubs,
                TurnCards = turnCards,
                NextPlayer = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceDiamonds)
            });
            
            //Assert
            Assert.IsFalse(result);
        }
        
        [Test]
        public void Default_ShouldReturnFalse_WhenAbyAttackCardWasBeatenByDefenseCard()
        {
            //Arrange
            var policy = new TransferPolicyDefault();
            
            var turnCards = new TurnCardsContainer();
            turnCards.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.AceDiamonds));
            turnCards.AddDefenseCard(PlayingCard.Defaults.AceHearts, 0);
            
            var player = DurakPlayer.New.PushCards(
                PlayingCard.Defaults.EightClubs,
                PlayingCard.Defaults.EightDiamonds,
                PlayingCard.Defaults.EightHearts,
                PlayingCard.Defaults.EightSpades);
            
            //Act
            var result = policy.CanTransfer(new TransferPolicyData
            {
                Card = PlayingCard.Defaults.AceClubs,
                TurnCards = turnCards,
                NextPlayer = player
            });
            
            //Assert
            Assert.IsFalse(result);
        }
        
        [Test]
        public void Default_ShouldReturnFalse_WhenTurnCardsDoesNotContainAttackCardWithRankEqualToFirstAttackCardRank()
        {
            //Arrange
            var policy = new TransferPolicyDefault();
            
            var turnCards = new TurnCardsContainer();
            turnCards.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.AceDiamonds));
            
            var player = DurakPlayer.New.PushCards(
                PlayingCard.Defaults.EightClubs,
                PlayingCard.Defaults.EightDiamonds,
                PlayingCard.Defaults.EightHearts,
                PlayingCard.Defaults.EightSpades);
            
            //Act
            var result = policy.CanTransfer(new TransferPolicyData
            {
                Card = PlayingCard.Defaults.NineClubs,
                TurnCards = turnCards,
                NextPlayer = player
            });
            
            //Assert
            Assert.IsFalse(result);
        }
    }
}