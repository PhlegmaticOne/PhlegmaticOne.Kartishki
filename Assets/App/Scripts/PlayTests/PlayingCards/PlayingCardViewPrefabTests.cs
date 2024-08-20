using App.Scripts.DurakGame.PlayingCards.Views;
using App.Scripts.DurakGame.PlayingCards.Views.Components.Joker;
using App.Scripts.DurakGame.PlayingCards.Views.Components.Ranks;
using App.Scripts.DurakGame.PlayingCards.Views.Components.Suits;
using NUnit.Framework;
using UnityEngine;

namespace App.Scripts.PlayTests.PlayingCards
{
    public class PlayingCardViewPrefabTests
    {
        private const string PlayingCardViewPath = "PlayingCards/PlayingCard";

        private PlayingCardView _cardView;
        
        [OneTimeSetUp]
        public void Setup()
        {
            var cardView = Resources.Load<PlayingCardView>(PlayingCardViewPath);
            _cardView = Object.Instantiate(cardView);
        }
        
        [Test]
        public void View_ShouldExistInResources()
        {
            Assert.NotNull(_cardView);
        }

        [Test]
        public void View_ShouldHave2RankCornerViews()
        {
            //Act
            var rankCornerViews = _cardView.GetComponentsInChildren<RankCornerView>(true);
            
            //Assert
            Assert.AreEqual(2, rankCornerViews.Length);
        }
        
        [Test]
        public void View_ShouldHaveSuitFrontView()
        {
            //Act
            var suitFrontView = _cardView.GetComponentInChildren<SuitFrontView>(true);
            
            //Assert
            Assert.NotNull(suitFrontView);
        }
        
        [Test]
        public void View_ShouldHaveRankFrontView()
        {
            //Act
            var rankFrontView = _cardView.GetComponentInChildren<RankFrontView>(true);
            
            //Assert
            Assert.NotNull(rankFrontView);
        }
        
        [Test]
        public void View_ShouldHaveJokerView()
        {
            //Act
            var jokerView = _cardView.GetComponentInChildren<JokerView>(true);
            
            //Assert
            Assert.NotNull(jokerView);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Object.Destroy(_cardView.gameObject);
        }
    }
}