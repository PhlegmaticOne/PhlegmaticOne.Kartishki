using System;
using System.Linq;
using App.Scripts.Durak.Turns;
using Kartishki.Core;
using Kartishki.Core.Components;
using NUnit.Framework;

namespace App.Scripts.Tests.Durak.Turns
{
    [TestFixture]
    public class TurnCardsContainerTests
    {
        [Test]
        public void AddAttackCard_ShouldAddAttackCardAndNotIncrementDefenseCardsCount()
        {
            //Arrange
            var container = new TurnCardsContainer();
            
            //Act
            container.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.AceHearts));
            
            //Assert
            Assert.AreEqual(1, container.AttackCardsCount);
            Assert.AreEqual(0, container.DefenseCardsCount);
        }

        [Test]
        public void AddDefenseCard_ShouldThrowArgumentException_WhenBeatingCardIndexIsOutOfAttackCardsRange()
        {
            //Arrange
            var container = new TurnCardsContainer();
            
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => container.AddDefenseCard(PlayingCard.Defaults.AceHearts, 2));
        }

        [Test]
        public void AddDefenseCard_ShouldBeatExistingAttackCardAndIncrementDefenseCardsCount()
        {
            //Arrange
            var container = new TurnCardsContainer();
            
            //Act
            container.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.EightClubs));
            container.AddDefenseCard(PlayingCard.Defaults.TenClubs, 0);
            
            //Assert
            Assert.AreEqual(1, container.AttackCardsCount);
            Assert.AreEqual(1, container.DefenseCardsCount);
        }

        [Test]
        public void HasAttackCardWithRank_ShouldReturnTrue_WhenContainerHasCardWithRankInAttackCards()
        {
            //Arrange
            var rank = RankComponent.Eight;
            var container = new TurnCardsContainer();
            
            //Act
            container.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.EightClubs));
            
            //Assert
            Assert.IsTrue(container.HasAttackCardWithRank(rank));
        }
        
        [Test]
        public void HasAttackCardWithRank_ShouldReturnFalse_WhenContainerDoesNotHaveCardWithRankInAttackCards()
        {
            //Arrange
            var rank = RankComponent.Nine;
            var container = new TurnCardsContainer();
            
            //Act
            container.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.EightClubs));
            
            //Assert
            Assert.IsFalse(container.HasAttackCardWithRank(rank));
        }
        
        [Test]
        public void HasAttackCardWithRank_ShouldReturnFalse_WhenContainerHasCardWithRankInDefenseCards()
        {
            //Arrange
            var rank = RankComponent.Eight;
            var container = new TurnCardsContainer();
            
            //Act
            container.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.NineClubs));
            container.AddDefenseCard(PlayingCard.Defaults.EightClubs, 0);
            
            //Assert
            Assert.IsFalse(container.HasAttackCardWithRank(rank));
        }
        
        [Test]
        public void HasCardWithRank_ShouldReturnTrue_WhenContainerHasCardWithRankInAttackCards()
        {
            //Arrange
            var rank = RankComponent.Eight;
            var container = new TurnCardsContainer();
            
            //Act
            container.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.EightClubs));
            
            //Assert
            Assert.IsTrue(container.HasCardWithRank(rank));
        }
        
        [Test]
        public void HasCardWithRank_ShouldReturnTrue_WhenContainerHasCardWithRankInDefenseCards()
        {
            //Arrange
            var rank = RankComponent.Eight;
            var container = new TurnCardsContainer();
            
            //Act
            container.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.NineClubs));
            container.AddDefenseCard(PlayingCard.Defaults.EightClubs, 0);
            
            //Assert
            Assert.IsTrue(container.HasCardWithRank(rank));
        }

        [Test]
        public void GetAttackCardAt_ShouldThrowArgumentException_WhenCardIndexIsOutOfRange()
        {
            //Arrange
            var container = new TurnCardsContainer();
            
            //Act
            container.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.NineClubs));

            //Assert
            Assert.Throws<ArgumentException>(() => container.GetTurnAttackCardAt(2));
        }
        
        [Test]
        public void GetAttackCardAt_ShouldReturnAttackCardAtIndex_WhenCardIndexWithinRange()
        {
            //Arrange
            var container = new TurnCardsContainer();
            var card = TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.EightClubs);
            
            //Act
            container.AddAttackCard(card);

            //Assert
            Assert.AreEqual(card, container.GetTurnAttackCardAt(0));
        }

        [Test]
        public void TakeCards_ShouldReturnAllAttackCardsAndAddedDefenseCards()
        {
            //Arrange
            var container = new TurnCardsContainer();
            
            container.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.EightClubs));
            container.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.NineClubs));
            container.AddDefenseCard(PlayingCard.Defaults.AceHearts, 0);

            //Act
            var takenCards = container.TakeCards().ToArray();
            
            //Assert
            Assert.AreEqual(3, takenCards.Length);
            CollectionAssert.Contains(takenCards, PlayingCard.Defaults.EightClubs);
            CollectionAssert.Contains(takenCards, PlayingCard.Defaults.NineClubs);
            CollectionAssert.Contains(takenCards, PlayingCard.Defaults.AceHearts);
        }

        [Test]
        public void IsAllCardsBeaten_ShouldReturnTrue_WhenAllAttackCardsWereBeatenByDefenseCards()
        {
            //Arrange
            var container = new TurnCardsContainer();
            
            //Act
            container.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.EightClubs));
            container.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.NineClubs));
            container.AddDefenseCard(PlayingCard.Defaults.AceHearts, 0);
            container.AddDefenseCard(PlayingCard.Defaults.EightHearts, 1);
            
            //Assert
            Assert.IsTrue(container.IsAllCardsBeaten());
        }
        
        [Test]
        public void IsAllCardsBeaten_ShouldReturnFalse_WhenNotAllAttackCardsWereBeatenByDefenseCards()
        {
            //Arrange
            var container = new TurnCardsContainer();
            
            //Act
            container.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.EightClubs));
            container.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.NineClubs));
            container.AddDefenseCard(PlayingCard.Defaults.AceHearts, 0);
            
            //Assert
            Assert.IsFalse(container.IsAllCardsBeaten());
        }

        [Test]
        public void IncreaseTurnNumber_ShouldIncrementTurnNumberByOne()
        {
            //Arrange
            var container = new TurnCardsContainer();
            
            //Act
            container.IncreaseTurnNumber();
            
            //Assert
            Assert.AreEqual(2, container.TurnNumber);
        }

        [Test]
        public void Clear_ShouldResetAttackAndDefenseCards()
        {
            //Arrange
            var container = new TurnCardsContainer();
            
            container.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.EightClubs));
            container.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.NineClubs));
            container.AddDefenseCard(PlayingCard.Defaults.AceHearts, 0);
            container.IncreaseTurnNumber();
            
            //Act
            container.Clear();
            
            //Assert
            Assert.AreEqual(0, container.AttackCardsCount);
            Assert.AreEqual(0, container.DefenseCardsCount);
        }
    }
}