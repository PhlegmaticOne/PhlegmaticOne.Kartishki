using System;
using System.Linq;
using App.Scripts.Durak.Players.Hand;
using Kartishki.Core;
using Kartishki.Core.Components;
using NUnit.Framework;

namespace App.Scripts.EditTests.Durak.Players.Hand
{
    [TestFixture]
    public class PlayerHandTests
    {
        [Test]
        public void PushCard_ShouldIncreaseCardsCountByOne()
        {
            //Arrange
            var hand = new PlayerHand();
            
            //Act
            hand.PushCard(PlayingCard.Defaults.AceDiamonds);
            
            //Assert
            Assert.AreEqual(1, hand.CardsCount);
        }

        [Test]
        public void PullCard_ShouldRemoveExpectedCardAndDecreaseCardsCountByOne()
        {
            //Arrange
            var hand = new PlayerHand();
            hand.PushCards(
                PlayingCard.Defaults.AceDiamonds,
                PlayingCard.Defaults.AceSpades,
                PlayingCard.Defaults.AceHearts);

            //Act
            var card = hand.PullCardAt(1);
            
            //Assert
            Assert.AreEqual(PlayingCard.Defaults.AceSpades, card);
            Assert.AreEqual(2, hand.CardsCount);
        }

        [Test]
        public void PullCard_ShouldThrowArgumentException_WhenCardIndexOutOfRange(
            [Values(-1, 10)] int cardIndex)
        {
            //Arrange
            var hand = new PlayerHand();
            hand.PushCards(
                PlayingCard.Defaults.AceDiamonds,
                PlayingCard.Defaults.AceSpades,
                PlayingCard.Defaults.AceHearts);

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => hand.PullCardAt(cardIndex));
        }
        
        [Test]
        public void GetCard_ShouldReturnCardAtIndexAndNotDecreaseCardsCountByOne()
        {
            //Arrange
            var hand = new PlayerHand();
            hand.PushCards(
                PlayingCard.Defaults.AceDiamonds,
                PlayingCard.Defaults.AceSpades,
                PlayingCard.Defaults.AceHearts);

            //Act
            var card = hand.GetCardAt(1);
            
            //Assert
            Assert.AreEqual(PlayingCard.Defaults.AceSpades, card);
            Assert.AreEqual(3, hand.CardsCount);
        }

        [Test]
        public void GetCard_ShouldThrowArgumentException_WhenCardIndexOutOfRange(
            [Values(-1, 10)] int cardIndex)
        {
            //Arrange
            var hand = new PlayerHand();
            hand.PushCards(
                PlayingCard.Defaults.AceDiamonds,
                PlayingCard.Defaults.AceSpades,
                PlayingCard.Defaults.AceHearts);

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => hand.GetCardAt(cardIndex));
        }
        
        [Test]
        public void GetCardsWithSuit_ShouldReturnCardsWithExpectedSuit()
        {
            //Arrange
            var expectedSuit = SuitComponent.Diamonds;
            
            var hand = new PlayerHand();
            hand.PushCards(
                PlayingCard.Defaults.AceDiamonds,
                PlayingCard.Defaults.AceSpades,
                PlayingCard.Defaults.AceHearts);

            //Act
            var cardsWithSuit = hand.GetCardsWithSuit(expectedSuit);
            
            //Assert
            Assert.IsTrue(cardsWithSuit.SequenceEqual(new [] { PlayingCard.Defaults.AceDiamonds } ));
        }
        
        [Test]
        public void GetCardsWithRank_ShouldReturnCardsWithExpectedRank()
        {
            //Arrange
            var expectedRank = RankComponent.Eight;
            
            var hand = new PlayerHand();
            hand.PushCards(
                PlayingCard.Defaults.AceDiamonds,
                PlayingCard.Defaults.EightClubs,
                PlayingCard.Defaults.AceHearts);

            //Act
            var cardsWithSuit = hand.GetCardsWithRank(expectedRank);
            
            //Assert
            Assert.IsTrue(cardsWithSuit.SequenceEqual(new [] { PlayingCard.Defaults.EightClubs } ));
        }
    }
}