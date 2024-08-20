using System;
using System.Collections;
using System.Collections.Generic;
using Kartishki.Core.Components;
using NUnit.Framework;

namespace App.Scripts.EditTests.Core
{
    [TestFixture]
    public class SuitComponentTests
    {
        [Test]
        public void Create_ShouldThrowArgumentException_WhenNameIsEmpty(
            [Values(null, "", "      ")] string suitName)
        {
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => SuitComponent.Create('♠', suitName, -1));
        }
        
        [Test]
        public void Create_ShouldThrowArgumentException_WhenSuitValueIsInvalidChar(
            [Values('\0', '5', ',', ' ', '-', '\t', '\n')] char value)
        {
            const string suitName = "Suit";
            
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => SuitComponent.Create(value, suitName, -1));
        }
        
        [Test]
        public void Create_ShouldThrowArgumentException_WhenSuitColorLessThanZero()
        {
            const string suitName = "Suit";
            
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => SuitComponent.Create('♣', suitName, -1));
        }

        [TestCaseSource(typeof(SuitValidData), nameof(SuitValidData.Get))]
        public void Create_ShouldCreateSuit_WhenDataIsValid((char value, string name) testData)
        {
            //Act
            //Assert
            Assert.DoesNotThrow(() => SuitComponent.Create(testData.value, testData.name, 1));
        }

        [Test]
        public void Equals_ShouldBeEqual_WhenSuitValueAndNameAreEqual()
        {
            //Arrange
            var compareOne = SuitComponent.Clubs;
            var compareTwo = SuitComponent.Create('♣', "Clubs", SuitComponent.BlackColor);
            
            //Act
            //Assert
            Assert.AreEqual(compareOne, compareTwo);
        }

        [TestCaseSource(typeof(SuitParseData), nameof(SuitParseData.Valid))]
        public SuitComponent Parse_ShouldReturnParsedSuit_WhenInputValueIsDefaultSuitValue(string value)
        {
            //Act
            //Assert
            return SuitComponent.Parse(value);
        }
        
        [Test]
        public void Parse_ShouldThrowArgumentException_WhenInputValueIsInvalid(
            [Values("1", "♣♣♣", "asf", "")] string value)
        {
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => SuitComponent.Parse(value));
        }
    }

    public class SuitParseData
    {
        public static IEnumerable Valid()
        {
            yield return new TestCaseData("♠").Returns(SuitComponent.Spades);
            yield return new TestCaseData("♥").Returns(SuitComponent.Hearts);
            yield return new TestCaseData("♦").Returns(SuitComponent.Diamonds);
            yield return new TestCaseData("♣").Returns(SuitComponent.Clubs);
        }
    }

    public class SuitValidData
    {
        public static IEnumerable<(char, string)> Get()
        {
            yield return ('♠', "Spades");
            yield return ('a', "test");
            yield return ('L', "Lazy");
        }
    }
}