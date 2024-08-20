using System;
using System.Collections;
using System.Collections.Generic;
using Kartishki.Core.Components;
using NUnit.Framework;

namespace App.Scripts.Tests.Core
{
    [TestFixture]
    public class RankComponentTests
    {
        [Test]
        public void Create_ShouldThrowArgumentException_WhenRankValueLessThanZero()
        {
            //Arrange
            const int value = -42;
            const string name = "test";

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => RankComponent.Create(value, name));
        }

        [Test]
        public void Create_ShouldThrowArgumentException_WhenRankNameIsBlankString(
            [Values(null, "", "    ")] string name)
        {
            //Arrange
            const int value = 3;

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => RankComponent.Create(value, name));
        }

        [Test]
        public void Equals_ShouldReturnTrue_WhenComparingPredefinedRankWithSameCreatedRank()
        {
            //Arrange
            var predefined = RankComponent.Ace;
            var created = RankComponent.Create(14, "A");
            
            //Act
            //Assert
            Assert.AreEqual(predefined, created);
        }

        [TestCaseSource(typeof(RankParseData), nameof(RankParseData.Valid))]
        public RankComponent Parse_ShouldReturnParsedRank_WhenInputValueIsDefaultRankValue(string value)
        {
            //Act
            //Assert
            return RankComponent.Parse(value);
        }

        [TestCaseSource(typeof(RankParseData), nameof(RankParseData.Invalid))]
        public void Parse_ShouldThrowArgumentException_WhenInputValueIsNotDefaultRankValue(string value)
        {
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => RankComponent.Parse(value));
        }

        [Test]
        public void TryParse_ShouldReturnTrue_WhenInputValueIsDefaultRankValue(
            [Values("2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A")] string value)
        {
            //Act
            var result = RankComponent.TryParse(value, out _);
            
            //Assert
            Assert.IsTrue(result);
        }
        
        [TestCaseSource(typeof(RankParseData), nameof(RankParseData.Invalid))]
        public void TryParse_ShouldReturnFalse_WhenInputValueIsNotDefaultRankValue(string value)
        {
            //Act
            var result = RankComponent.TryParse(value, out _);
            
            //Assert
            Assert.IsFalse(result);
        }
    }
    
    public class RankParseData
    {
        public static IEnumerable Valid()
        {
            yield return new TestCaseData("2").Returns(RankComponent.Two);
            yield return new TestCaseData("3").Returns(RankComponent.Three);
            yield return new TestCaseData("4").Returns(RankComponent.Four);
            yield return new TestCaseData("5").Returns(RankComponent.Five);
            yield return new TestCaseData("6").Returns(RankComponent.Six);
            yield return new TestCaseData("7").Returns(RankComponent.Seven);
            yield return new TestCaseData("8").Returns(RankComponent.Eight);
            yield return new TestCaseData("9").Returns(RankComponent.Nine);
            yield return new TestCaseData("10").Returns(RankComponent.Ten);
            yield return new TestCaseData("J").Returns(RankComponent.Jack);
            yield return new TestCaseData("Q").Returns(RankComponent.Queen);
            yield return new TestCaseData("K").Returns(RankComponent.King);
            yield return new TestCaseData("A").Returns(RankComponent.Ace);
        }

        public static IEnumerable<string> Invalid()
        {
            yield return "-1";
            yield return "1";
            yield return "15";
            yield return "Z";
            yield return "V";
            yield return "o";
        }
    }
}