using System.Collections;
using App.Scripts.DurakGame.PlayingCards.Views;
using App.Scripts.DurakGame.PlayingCards.Views.Components.Joker;
using App.Scripts.DurakGame.PlayingCards.Views.Components.Ranks;
using App.Scripts.DurakGame.PlayingCards.Views.Components.Suits;
using App.Scripts.DurakGame.PlayingCards.Views.ViewModel;
using Kartishki.Core;
using Kartishki.Core.Components;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace App.Scripts.PlayTests.PlayingCards
{
    public class PlayingCardViewTests
    {
        private const string PlayingCardViewPath = "PlayingCards/PlayingCard";
        
        private PlayingCardView _cardView;
        
        [SetUp]
        public void Setup()
        {
            var cardView = Resources.Load<PlayingCardView>(PlayingCardViewPath);
            _cardView = Object.Instantiate(cardView);
        }

        [UnityTest]
        public IEnumerator View_ShouldHave_JokerView_Enabled_WhenCardIsJoker()
        {
            //Arrange
            var jokerView = _cardView.GetComponentInChildren<JokerView>(true);
            var viewModel = PlayingCardViewModel.FromCard(PlayingCard.Defaults.JokerBlack);
            
            //Act
            _cardView.UpdateView(viewModel);
            yield return null;
            
            //Assert
            Assert.IsTrue(jokerView.gameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator View_ShouldHave_JokerView_Disabled_WhenCardIsNotJoker()
        {
            //Arrange
            var jokerView = _cardView.GetComponentInChildren<JokerView>(true);
            var viewModel = PlayingCardViewModel.FromCard(PlayingCard.Defaults.EightClubs);
            
            //Act
            _cardView.UpdateView(viewModel);
            yield return null;
            
            //Assert
            Assert.IsFalse(jokerView.gameObject.activeSelf);
        }
        
        [UnityTest]
        public IEnumerator View_ShouldHave_RankCornerViews_Enabled_WhenCardIsNotJoker()
        {
            //Arrange
            var rankCornerViews = _cardView.GetComponentsInChildren<RankCornerView>(true);
            var viewModel = PlayingCardViewModel.FromCard(PlayingCard.Defaults.EightClubs);
            
            //Act
            _cardView.UpdateView(viewModel);
            yield return null;
            
            //Assert
            foreach (var rankCornerView in rankCornerViews)
            {
                Assert.IsTrue(rankCornerView.gameObject.activeSelf);
            }
        }

        [UnityTest]
        public IEnumerator View_ShouldHave_RankCornerViews_Disabled_WhenCardIsJoker()
        {
            //Arrange
            var rankCornerViews = _cardView.GetComponentsInChildren<RankCornerView>(true);
            var viewModel = PlayingCardViewModel.FromCard(PlayingCard.Defaults.JokerBlack);
            
            //Act
            _cardView.UpdateView(viewModel);
            yield return null;
            
            //Assert
            foreach (var rankCornerView in rankCornerViews)
            {
                Assert.IsFalse(rankCornerView.gameObject.activeSelf);
            }
        }
        
        [UnityTest]
        public IEnumerator View_ShouldHave_RankFrontView_Disabled_WhenCardHasNumericRank()
        {
            //Arrange
            var rankFrontView = _cardView.GetComponentInChildren<RankFrontView>(true);
            var viewModel = PlayingCardViewModel.FromCard(PlayingCard.Defaults.EightClubs);
            
            //Act
            _cardView.UpdateView(viewModel);
            yield return null;
            
            //Assert
            Assert.IsFalse(rankFrontView.gameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator View_ShouldHave_RankFrontView_Disabled_WhenCardIsJoker()
        {
            //Arrange
            var rankFrontView = _cardView.GetComponentInChildren<RankFrontView>(true);
            var viewModel = PlayingCardViewModel.FromCard(PlayingCard.Defaults.JokerBlack);
            
            //Act
            _cardView.UpdateView(viewModel);
            yield return null;
            
            //Assert
            Assert.IsFalse(rankFrontView.gameObject.activeSelf);
        }
        
        [UnityTest]
        public IEnumerator View_ShouldHave_RankCornerViews_Enabled_WhenCardHasLetterRank()
        {
            //Arrange
            var rankFrontView = _cardView.GetComponentInChildren<RankFrontView>(true);
            var viewModel = PlayingCardViewModel.FromCard(PlayingCard.Defaults.JackClubs);
            
            //Act
            _cardView.UpdateView(viewModel);
            yield return null;
            
            //Assert
            Assert.IsTrue(rankFrontView.gameObject.activeSelf);
        }
        
        [UnityTest]
        public IEnumerator View_ShouldHave_SuitFrontView_Disabled_WhenCardHasLetterRank()
        {
            //Arrange
            var suitFrontView = _cardView.GetComponentInChildren<SuitFrontView>(true);
            var viewModel = PlayingCardViewModel.FromCard(PlayingCard.Defaults.QueenClubs);
            
            //Act
            _cardView.UpdateView(viewModel);
            yield return null;
            
            //Assert
            Assert.IsFalse(suitFrontView.gameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator View_ShouldHave_SuitFrontView_Disabled_WhenCardIsJoker()
        {
            //Arrange
            var suitFrontView = _cardView.GetComponentInChildren<SuitFrontView>(true);
            var viewModel = PlayingCardViewModel.FromCard(PlayingCard.Defaults.JokerBlack);
            
            //Act
            _cardView.UpdateView(viewModel);
            yield return null;
            
            //Assert
            Assert.IsFalse(suitFrontView.gameObject.activeSelf);
        }
        
        [UnityTest]
        public IEnumerator View_ShouldHave_SuitFrontView_Enabled_WhenCardHasNumericRank()
        {
            //Arrange
            var suitFrontView = _cardView.GetComponentInChildren<SuitFrontView>(true);
            var viewModel = PlayingCardViewModel.FromCard(PlayingCard.Defaults.SevenClubs);
            
            //Act
            _cardView.UpdateView(viewModel);
            yield return null;
            
            //Assert
            Assert.IsTrue(suitFrontView.gameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator View_ShouldHaveSuitEntryViewsCountEqualToCardRankNumericValue()
        {
            //Arrange
            var suitFrontView = _cardView.GetComponentInChildren<SuitFrontView>(true);

            for (var rankValue = 2; rankValue <= 10; rankValue++)
            {
                var card = PlayingCard.Create().Card()
                    .WithRank(rankValue, rankValue.ToString())
                    .WithSuit(SuitComponent.Diamonds);

                var viewModel = PlayingCardViewModel.FromCard(card);
                
                //Act
                _cardView.UpdateView(viewModel);

                yield return null;

                var suitEntryViews = suitFrontView.GetComponentsInChildren<SuitEntryView>();
                
                //Assert
                Assert.AreEqual(rankValue, suitEntryViews.Length);
            }
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_cardView.gameObject);
        }
    }
}