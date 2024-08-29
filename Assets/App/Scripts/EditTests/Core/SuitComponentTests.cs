using System;
using System.Collections.Generic;
using App.Scripts.Cards.Components;
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
            Assert.Throws<ArgumentException>(() => SuitComponent.Create('♠', suitName));
        }
        
        [Test]
        public void Create_ShouldThrowArgumentException_WhenSuitValueIsInvalidChar(
            [Values('\0', '5', ',', ' ', '-', '\t', '\n')] char value)
        {
            const string suitName = "Suit";
            
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => SuitComponent.Create(value, suitName));
        }

        [TestCaseSource(typeof(SuitValidData), nameof(SuitValidData.Get))]
        public void Create_ShouldCreateSuit_WhenDataIsValid((char value, string name) testData)
        {
            //Act
            //Assert
            Assert.DoesNotThrow(() => SuitComponent.Create(testData.value, testData.name));
        }

        [Test]
        public void Equals_ShouldBeEqual_WhenSuitValueAndNameAreEqual()
        {
            //Arrange
            var compareOne = SuitComponent.Clubs;
            var compareTwo = SuitComponent.Create('♣', "Clubs");
            
            //Act
            //Assert
            Assert.AreEqual(compareOne, compareTwo);
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