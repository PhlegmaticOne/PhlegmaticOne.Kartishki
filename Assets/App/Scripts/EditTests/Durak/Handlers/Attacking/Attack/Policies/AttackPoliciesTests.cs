using App.Scripts.Durak.Handlers.Attacking.Attack.Policies;
using App.Scripts.Durak.Players.Extensions;
using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;
using Kartishki.Core;
using NUnit.Framework;

namespace App.Scripts.EditTests.Durak.Handlers.Attacking.Attack.Policies
{
    [TestFixture]
    public class AttackPoliciesTests
    {
        [Test]
        public void SameRank_ShouldReturnFalse_WhenDefenderHasNoCards()
        {
            //Arrange
            var policy = new AttackPolicySameRank();

            //Act
            var result = policy.CanAttack(new AttackPolicyData
            {
                TurnCards = new TurnCardsContainer(),
                Defender = DurakPlayer.New,
                AttackCard = PlayingCard.Defaults.AceClubs
            });
            
            //Assert
            Assert.IsFalse(result);
        }
        
        [Test]
        public void SameRank_ShouldReturnTrue_WhenThereAreNoCardsAtTheTable()
        {
            //Arrange
            var policy = new AttackPolicySameRank();

            //Act
            var result = policy.CanAttack(new AttackPolicyData
            {
                TurnCards = new TurnCardsContainer(),
                Defender = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceDiamonds),
                AttackCard = PlayingCard.Defaults.AceClubs
            });
            
            //Assert
            Assert.IsTrue(result);
        }
        
        [Test]
        public void SameRank_ShouldReturnTrue_WhenThereAreCardWithSameRankAtTheTable()
        {
            //Arrange
            var policy = new AttackPolicySameRank();
            var turnCards = new TurnCardsContainer();
            turnCards.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.EightClubs));

            //Act
            var result = policy.CanAttack(new AttackPolicyData
            {
                TurnCards = turnCards,
                Defender = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceDiamonds),
                AttackCard = PlayingCard.Defaults.EightHearts
            });
            
            //Assert
            Assert.IsTrue(result);
        }
    }
}