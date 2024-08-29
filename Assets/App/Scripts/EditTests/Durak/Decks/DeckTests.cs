using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Cards;
using App.Scripts.Durak.Decks;
using App.Scripts.Durak.Players.Extensions;
using App.Scripts.Durak.Players.Models;
using NUnit.Framework;

namespace App.Scripts.EditTests.Durak.Decks
{
    [TestFixture]
    public class DeckTests
    {
        [Test]
        public void Deck_ShouldThrowArgumentException_WhenCardsCollectionIsEmpty()
        {
            //Arrange
            var cards = Enumerable.Empty<PlayingCard>();

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => Deck.Create(cards));
        }

        [Test]
        public void Deck_ShouldBeCreated_WhenCardsCollectionIsNotEmpty()
        {
            //Arrange
            var cards = Deck.Enumerate36Cards();
            
            //Act
            var deck = Deck.Create(cards);
            
            //Assert
            Assert.NotNull(deck);
            Assert.IsFalse(deck.IsEmpty);
        }

        [Test]
        public void Deck_ShouldHaveFirstCardAsTrump()
        {
            //Arrange
            var deck = Deck.Create(
                PlayingCard.Defaults.SixSpades,
                PlayingCard.Defaults.AceDiamonds,
                PlayingCard.Defaults.EightClubs);
            
            var trump = deck.Trump;
            
            //Act
            //Assert
            Assert.AreEqual(trump, PlayingCard.Defaults.SixSpades);
        }

        [Test]
        public void Deck_ShouldFillPlayerWith6Cards_WhenPlayerHandIsEmpty()
        {
            //Arrange
            const int handCapacity = 6;

            var deck = Deck.Create(
                PlayingCard.Defaults.AceDiamonds,
                PlayingCard.Defaults.EightClubs,
                PlayingCard.Defaults.AceClubs,
                PlayingCard.Defaults.EightHearts,
                PlayingCard.Defaults.FourSpades,
                PlayingCard.Defaults.FiveSpades);

            var player = new DurakPlayer(Guid.Empty, handCapacity);
            
            //Act
            deck.FillCardConsumer(player);
            
            //Assert
            Assert.IsTrue(deck.IsEmpty);
            Assert.AreEqual(handCapacity, player.Hand.CardsCount);
        }
        
        [Test]
        public void Deck_ShouldFillPlayerWith3Cards_WhenPlayerHandHas3Cards()
        {
            //Arrange
            const int handCapacity = 6;
            const int expectedDeckCardsCount = 3;

            var deck = Deck.Create(
                PlayingCard.Defaults.AceDiamonds,
                PlayingCard.Defaults.EightClubs,
                PlayingCard.Defaults.AceClubs,
                PlayingCard.Defaults.EightHearts,
                PlayingCard.Defaults.FourSpades,
                PlayingCard.Defaults.FiveSpades);

            var player = new DurakPlayer(Guid.Empty, handCapacity);
            
            player.PushCards(
                PlayingCard.Defaults.AceDiamonds,
                PlayingCard.Defaults.AceClubs,
                PlayingCard.Defaults.EightClubs);
            
            //Act
            deck.FillCardConsumer(player);
            
            //Assert
            Assert.AreEqual(expectedDeckCardsCount, deck.CardsCount);
            Assert.AreEqual(handCapacity, player.Hand.CardsCount);
        }

        [Test]
        public void Deck_ShouldNotFillPlayer_WhenPlayerIsOverfilled()
        {
            //Arrange
            var deck = Deck.Create(
                PlayingCard.Defaults.AceDiamonds);

            var player = new DurakPlayer(Guid.Empty, 1);
            player.PushCards(
                PlayingCard.Defaults.AceDiamonds,
                PlayingCard.Defaults.EightHearts);
            
            //Act
            deck.FillCardConsumer(player);
            
            //Assert
            Assert.AreEqual(1, deck.CardsCount);
            Assert.AreEqual(2, player.Hand.CardsCount);
        }

        [Test]
        public void Deck_ShouldFillPlayerWithRemainCards_WhenCardsCountInDeckLessThanPlayerNeededCardsCount()
        {
            //Arrange
            var deck = Deck.Create(
                PlayingCard.Defaults.AceDiamonds,
                PlayingCard.Defaults.EightHearts);
            
            var player = new DurakPlayer(Guid.Empty, 4);
            
            //Act
            deck.FillCardConsumer(player);
            
            //Assert
            Assert.IsTrue(deck.IsEmpty);
            Assert.AreEqual(2, player.Hand.CardsCount);
        }

        [TestCaseSource(typeof(DeckCountTestEnumerations), nameof(DeckCountTestEnumerations.Get))]
        public int Deck_ShouldHaveExpectedCardsCount_WhenConstructedFromDefaultCardEnumerations(
            IEnumerable<PlayingCard> cards)
        {
            //Act
            var deck = Deck.Create(cards);
            
            //Assert
            return deck.CardsCount;
        }
    }

    internal class DeckCountTestEnumerations
    {
        public static IEnumerable Get()
        {
            yield return new TestCaseData(Deck.Enumerate36Cards()).Returns(36).SetName("36 cards");
            yield return new TestCaseData(Deck.Enumerate52Cards()).Returns(52).SetName("52 cards");
            yield return new TestCaseData(Deck.Enumerate54Cards()).Returns(54).SetName("54 cards");
        }
    }
}