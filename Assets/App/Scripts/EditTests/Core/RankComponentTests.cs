using System;
using System.Collections;
using System.Collections.Generic;
using Kartishki.Core.Components;
using NUnit.Framework;

namespace App.Scripts.EditTests.Core
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
    }
}