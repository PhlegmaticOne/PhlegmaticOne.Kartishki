using System;
using Kartishki.Core;
using Kartishki.Core.Components;
using NUnit.Framework;

namespace App.Scripts.EditTests.Core
{
    [TestFixture]
    public class PlayingCardTests
    {
        [Test]
        public void Equals_ShouldReturnTrue_WhenComparingDefaultCardWithSameCreatedCard()
        {
            //Arrange
            var predefined = PlayingCard.Defaults.AceClubs;
            var created = PlayingCard.Create().Card().WithRank(RankComponent.Ace).WithSuit(SuitComponent.Clubs);
            
            //Act
            //Assert
            Assert.AreEqual(predefined, created);
        }
        
        [Test]
        public void IsJoker_ShouldReturnTrue_WhenCardHasJokerComponent()
        {
            //Arrange
            var jokerRed = PlayingCard.Defaults.JokerRed;
            
            //Act
            //Assert
            Assert.IsTrue(jokerRed.IsJoker());
            Assert.IsFalse(jokerRed.IsCard());
        }
        
        [Test]
        public void IsCard_ShouldReturnTrue_WhenCardHasCardComponent()
        {
            //Assert
            var aceClubs = PlayingCard.Defaults.AceClubs;
            
            //Act
            //Assert
            Assert.IsTrue(aceClubs.IsCard());
            Assert.IsFalse(aceClubs.IsJoker());
        }

        [Test, Combinatorial]
        public void Parse_ShouldParseCard_WhenInputValueIsValidCardStringRepresentation(
            [Values("2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A")] string rankValue,
            [Values("♠", "♥", "♦", "♣")] string suitValue)
        {
            //Arrange
            var value = rankValue + suitValue;
            
            //Act
            var card = PlayingCard.Parse(value);
            
            //Assert
            Assert.NotNull(card);
            Assert.IsTrue(card.IsCard());
        }
        
        [Test, Combinatorial]
        public void Parse_ShouldParseCard_WhenInputValueIsValidCardStringRepresentationReversed(
            [Values("2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A")] string rankValue,
            [Values("♠", "♥", "♦", "♣")] string suitValue)
        {
            //Arrange
            var valueReversed = suitValue + rankValue;
            
            //Act
            var cardReversed = PlayingCard.Parse(valueReversed);
            
            //Assert
            Assert.NotNull(cardReversed);
            Assert.IsTrue(cardReversed.IsCard());
        }
        
        [Test]
        public void Parse_ShouldThrowArgumentException_WhenInputValueIsNullOrEmpty(
            [Values(null, "", "   ")] string value)
        {
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => PlayingCard.Parse(value));
        }
        
        [Test]
        public void Parse_ShouldThrowArgumentException_WhenInputValueInvalid(
            [Values("11", "★2", "11♦", "a5", "asdccc", "♦")] string value)
        {
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => PlayingCard.Parse(value));
        }
        
        [Test, Combinatorial]
        public void TryParse_ShouldReturnTrue_WhenInputValueIsValidCardStringRepresentation(
            [Values("2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A")] string rankValue,
            [Values("♠", "♥", "♦", "♣")] string suitValue)
        {
            //Arrange
            var value = rankValue + suitValue;
            
            //Act
            var result = PlayingCard.TryParse(value, out var card);
            
            //Assert
            Assert.IsTrue(result);
            Assert.IsTrue(card.IsCard());
        }
        
        [Test, Combinatorial]
        public void TryParse_ShouldReturnTrue_WhenInputValueIsValidCardStringRepresentationReversed(
            [Values("2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A")] string rankValue,
            [Values("♠", "♥", "♦", "♣")] string suitValue)
        {
            //Arrange
            var valueReversed = suitValue + rankValue;
            
            //Act
            var resultReversed = PlayingCard.TryParse(valueReversed, out var cardReversed);
            
            //Assert
            Assert.IsTrue(resultReversed);
            Assert.IsTrue(cardReversed.IsCard());
        }
        
        [Test]
        public void Parse_ShouldSuccessfullyParseJoker_WhenInputValueIsValidJokerStringRepresentation(
            [Values("★0", "★1", "0★", "1★")] string jokerValue)
        {
            //Act
            var card = PlayingCard.Parse(jokerValue);
            
            //Assert
            Assert.NotNull(card);
            Assert.IsTrue(card.IsJoker());
        }
        
        [Test]
        public void TryParse_ShouldReturnTrue_WhenInputValueIsValidJokerStringRepresentation(
            [Values("★0", "★1", "0★", "1★")] string jokerValue)
        {
            //Act
            var result = PlayingCard.TryParse(jokerValue, out var card);
            
            //Assert
            Assert.IsTrue(result);
            Assert.IsTrue(card.IsJoker());
        }
    }
}