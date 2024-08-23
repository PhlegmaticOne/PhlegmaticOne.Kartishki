using System.Collections.Generic;
using App.Scripts.Durak.Decks;
using App.Scripts.DurakGame.PlayingCards.Views.ViewModel;
using Kartishki.Core;
using NUnit.Framework;

namespace App.Scripts.EditTests.DurakGame.PlayingCards
{
    [TestFixture]
    public class PlayingCardViewModelTests
    {
        [Test]
        public void FromCard_ShouldCorrectlySetCardRank()
        {
            //Arrange
            var card = PlayingCard.Defaults.EightDiamonds;

            //Act
            var viewModel = PlayingCardViewModel.FromCard(card);

            //Assert
            Assert.AreEqual(card.Rank, viewModel.Rank);
            Assert.AreEqual(card.Rank.Name, viewModel.RankView);
        }
        
        [Test]
        public void FromCard_ShouldCorrectlySetCardSuit()
        {
            //Arrange
            var card = PlayingCard.Defaults.EightDiamonds;

            //Act
            var viewModel = PlayingCardViewModel.FromCard(card);
            
            //Assert
            Assert.AreEqual(card.Suit.ToString(), viewModel.SuitView);
        }
        
        [Test]
        public void FromCard_ShouldCorrectlySetCardColor()
        {
            //Arrange
            var card = PlayingCard.Defaults.EightDiamonds;

            //Act
            var viewModel = PlayingCardViewModel.FromCard(card);
            
            //Assert
            Assert.AreEqual(card.Color, viewModel.Color);
        }
        
        [TestCaseSource(typeof(CardsSource), nameof(CardsSource.GetCards))]
        public void FromCard_ShouldSetNumericCardType_WhenCardRankIsNumeric(PlayingCard card)
        {
            //Act
            var viewModel = PlayingCardViewModel.FromCard(card);
            
            //Assert
            if (card.Rank.IsNumeric())
            {
                Assert.AreEqual(PlayingCardViewModelType.Numeric, viewModel.CardType);
            }
            else
            {
                Assert.AreNotEqual(PlayingCardViewModelType.Numeric, viewModel.CardType);
            }
        }

        [TestCaseSource(typeof(CardsSource), nameof(CardsSource.GetCards))]
        public void FromCard_ShouldSetLetterCardType_WhenCardRankIsLetter(PlayingCard card)
        {
            //Act
            var viewModel = PlayingCardViewModel.FromCard(card);
            
            //Assert
            if (card.Rank.IsLetter())
            {
                Assert.AreEqual(PlayingCardViewModelType.Letter, viewModel.CardType);
            }
            else
            {
                Assert.AreNotEqual(PlayingCardViewModelType.Letter, viewModel.CardType);
            }
        }

        [TestCaseSource(typeof(CardsSource), nameof(CardsSource.GetCards))]
        public void FromCard_ShouldSetJokerCardType_WhenCardIsJoker(PlayingCard card)
        {
            //Act
            var viewModel = PlayingCardViewModel.FromCard(card);
            
            //Assert
            if (card.IsJoker())
            {
                Assert.AreEqual(PlayingCardViewModelType.Joker, viewModel.CardType);
            }
            else
            {
                Assert.AreNotEqual(PlayingCardViewModelType.Joker, viewModel.CardType);
            }
        }
    }

    internal class CardsSource
    {
        public static IEnumerable<PlayingCard> GetCards()
        {
            return Deck.Enumerate54Cards();
        }
    }
}