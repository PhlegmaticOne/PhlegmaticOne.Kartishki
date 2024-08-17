using System.Collections;
using App.Scripts.Durak.Decks;
using App.Scripts.Durak.Handlers.Defense.Policies;
using Kartishki.Core;
using Kartishki.Core.Components;
using Moq;
using NUnit.Framework;

namespace App.Scripts.Tests.Durak.Handlers.Defense.Policies
{
    [TestFixture]
    public class DefensePoliciesTests
    {
        private static class Mocks
        {
            public static IDeck DeckMockWithTrump(SuitComponent trumpSuit)
            {
                var mock = new Mock<IDeck>();

                mock.SetupGet(x => x.Trump)
                    .Returns(PlayingCard.Create().Card().WithRank(RankComponent.Five).WithSuit(trumpSuit));

                return mock.Object;
            }
        }
        
        [TestCaseSource(typeof(DefensePolicyTestData), nameof(DefensePolicyTestData.JokerTestData))]
        public bool Default_ShouldReturnExpectedValue_WhenDefenseCardIsJoker(
            PlayingCard defenseCard, PlayingCard attackCard)
        {
            //Arrange
            var policy = new DefensePolicyDefault();

            //Act
            var canDefend = policy.CanDefend(new DefensePolicyData
            {
                Deck = Mocks.DeckMockWithTrump(SuitComponent.Clubs),
                DefenseCard = defenseCard,
                AttackCard = attackCard
            });

            //Assert
            return canDefend;
        }
        
        [TestCaseSource(typeof(DefensePolicyTestData), nameof(DefensePolicyTestData.CardsTestData))]
        public bool Default_ShouldReturnExpectedValue_WhenDefenseCardIsNotJoker(
            PlayingCard defenseCard, PlayingCard attackCard, SuitComponent trumpSuit)
        {
            //Arrange
            var policy = new DefensePolicyDefault();

            //Act
            var canDefend = policy.CanDefend(new DefensePolicyData
            {
                Deck = Mocks.DeckMockWithTrump(trumpSuit),
                DefenseCard = defenseCard,
                AttackCard = attackCard
            });
            
            //Assert
            return canDefend;
        }
    }

    internal class DefensePolicyTestData
    {
        public static IEnumerable JokerTestData()
        {
            yield return new TestCaseData(PlayingCard.Defaults.JokerRed, PlayingCard.Defaults.AceHearts)
                .Returns(true)
                .SetName("Returns true when defense card is joker with attack card color");
            
            yield return new TestCaseData(PlayingCard.Defaults.JokerBlack, PlayingCard.Defaults.AceHearts)
                .Returns(false)
                .SetName("Returns false when defense card is joker with no attack card color");
            
            yield return new TestCaseData(PlayingCard.Defaults.JokerBlack, PlayingCard.Defaults.JokerRed)
                .Returns(false)
                .SetName("Returns false when defense and attack cards are both jokers");
        }
        
        public static IEnumerable CardsTestData()
        {
            yield return new TestCaseData(PlayingCard.Defaults.SevenClubs, PlayingCard.Defaults.EightClubs, SuitComponent.Hearts)
                .Returns(false)
                .SetName("Returns false when defense card rank less than attack card rank " +
                         "and their suits are equal and not trump suit");
            
            yield return new TestCaseData(PlayingCard.Defaults.SevenClubs, PlayingCard.Defaults.EightClubs, SuitComponent.Clubs)
                .Returns(false)
                .SetName("Returns false when defense card rank less than attack card rank " +
                         "and their suits are equal and trump suit");
            
            yield return new TestCaseData(PlayingCard.Defaults.QueenDiamonds, PlayingCard.Defaults.TenDiamonds, SuitComponent.Hearts)
                .Returns(true)
                .SetName("Returns true when defense card rank grater than attack card rank " +
                         "and their suits are equal and not trump suit");
            
            yield return new TestCaseData(PlayingCard.Defaults.QueenDiamonds, PlayingCard.Defaults.TenDiamonds, SuitComponent.Diamonds)
                .Returns(true)
                .SetName("Returns true when defense card rank greater than attack card rank " +
                         "and their suits are equal and trump suit");
        }
    }
}