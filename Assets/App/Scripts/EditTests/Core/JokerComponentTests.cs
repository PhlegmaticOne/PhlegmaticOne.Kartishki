using System;
using System.Collections;
using System.Collections.Generic;
using Kartishki.Core.Components;
using NUnit.Framework;

namespace App.Scripts.EditTests.Core
{
    [TestFixture]
    public class JokerComponentTests
    {
        [Test]
        public void Create_ShouldThrowArgumentException_WhenColorTypeLessThanZero()
        {
            //Arrange
            const int colorType = -42;

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => JokerComponent.Create(colorType));
        }
        
        [Test]
        public void Equals_ShouldReturnTrue_WhenComparingPredefinedJokerWithSameCreatedJoker()
        {
            //Arrange
            var predefined = JokerComponent.Red;
            
            //Act
            var created = JokerComponent.Create(JokerComponent.RedColor);
            
            //Assert
            Assert.AreEqual(predefined, created);
        }
        
        [TestCaseSource(typeof(JokerParseData), nameof(JokerParseData.Valid))]
        public JokerComponent Parse_ShouldReturnParsedJoker_WhenInputValueIsValidJokerColor(string value)
        {
            //Act
            //Assert
            return JokerComponent.Parse(value);
        }

        [TestCaseSource(typeof(JokerParseData), nameof(JokerParseData.Invalid))]
        public void Parse_ShouldThrowArgumentException_WhenInputValueIsNotValidJokerColor(string value)
        {
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => RankComponent.Parse(value));
        }
        
        [Test]
        public void TryParse_ShouldReturnTrue_WhenInputValueIsDefaultJokerColor(
            [Values("0", "1")] string value)
        {
            //Act
            var result = JokerComponent.TryParse(value, out _);
            
            //Assert
            Assert.IsTrue(result);
        }
        
        [TestCaseSource(typeof(JokerParseData), nameof(JokerParseData.Invalid))]
        public void TryParse_ShouldReturnFalse_WhenInputValueIsNotDefaultJokerColor(string value)
        {
            //Act
            var result = JokerComponent.TryParse(value, out _);
            
            //Assert
            Assert.IsFalse(result);
        }
    }
    
    public class JokerParseData
    {
        public static IEnumerable Valid()
        {
            yield return new TestCaseData("0").Returns(JokerComponent.Red);
            yield return new TestCaseData("1").Returns(JokerComponent.Black);
        }

        public static IEnumerable<string> Invalid()
        {
            yield return "-1";
            yield return "15";
            yield return "Z";
            yield return "V";
            yield return "o";
        }
    }
}