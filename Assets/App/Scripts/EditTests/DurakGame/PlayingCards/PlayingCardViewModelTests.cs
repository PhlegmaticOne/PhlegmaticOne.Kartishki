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
            Assert.AreEqual(card.Card.Rank, viewModel.Rank);
            Assert.AreEqual(card.Card.Rank.Name, viewModel.RankView);
        }
        
        [Test]
        public void FromCard_ShouldCorrectlySetCardSuit()
        {
            //Arrange
            var card = PlayingCard.Defaults.EightDiamonds;

            //Act
            var viewModel = PlayingCardViewModel.FromCard(card);
            
            //Assert
            Assert.AreEqual(card.Card.Suit.GetValueString(), viewModel.SuitView);
        }
        
        [TestCaseSource(typeof(CardsSource), nameof(CardsSource.GetCards))]
        public void FromCard_ShouldSetNumericCardType_WhenCardRankIsNumeric(PlayingCard card)
        {
            //Act
            var viewModel = PlayingCardViewModel.FromCard(card);
            
            //Assert
            if (card.Card.Rank.IsNumeric())
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
            if (card.Card.Rank.IsLetter())
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
        
        [TestCaseSource(typeof(CardsSource), nameof(CardsSource.GetCards))]
        public void FromCard_ShouldSetJokerColor_WhenCardIsJoker(PlayingCard card)
        {
            //Act
            var viewModel = PlayingCardViewModel.FromCard(card);
            
            //Assert
            if (card.IsJoker())
            {
                Assert.AreEqual(card.Joker.Color, viewModel.Color);
            }
            else
            {
                Assert.AreNotEqual(card.Joker.Color, viewModel.Color);
            }
        }
        
        [TestCaseSource(typeof(CardsSource), nameof(CardsSource.GetCards))]
        public void FromCard_ShouldSetCardSuitColor_WhenCardIsNotJoker(PlayingCard card)
        {
            //Act
            var viewModel = PlayingCardViewModel.FromCard(card);
            
            //Assert
            if (card.IsCard())
            {
                Assert.AreEqual(card.Card.Suit.Color, viewModel.Color);
            }
            else
            {
                Assert.AreNotEqual(card.Card.Suit.Color, viewModel.Color);
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