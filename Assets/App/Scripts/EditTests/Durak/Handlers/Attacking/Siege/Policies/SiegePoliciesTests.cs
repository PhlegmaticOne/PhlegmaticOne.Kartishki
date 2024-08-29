using App.Scripts.Cards;
using App.Scripts.Durak.Handlers.Attacking.Siege.Policies;
using App.Scripts.Durak.Players.Extensions;
using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;
using NUnit.Framework;

namespace App.Scripts.EditTests.Durak.Handlers.Attacking.Siege.Policies
{
    [TestFixture]
    public class SiegePoliciesTests
    {
        [Test]
        public void Default_ShouldReturnFalse_WhenItIsFirstTurnAndThereAreMaxAttackCardsCountAtTheTable()
        {
            //Arrange
            var policy = new SiegePolicyDefault(2);
            
            var turnCards = new TurnCardsContainer();
            turnCards.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.EightDiamonds));
            turnCards.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.EightClubs));

            //Act
            var result = policy.CanBesiege(new SiegePolicyData
            {
                TurnCards = turnCards,
                SiegeCard = PlayingCard.Defaults.AceClubs,
                TurnNumber = 1,
                Defender = DurakPlayer.New
            });
            
            //Assert
            Assert.IsFalse(result);
        }
        
        [Test]
        public void Default_ShouldReturnFalse_WhenDefenderHasNoCards()
        {
            //Arrange
            var policy = new SiegePolicyDefault(2);
            
            var turnCards = new TurnCardsContainer();
            turnCards.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.EightDiamonds));

            //Act
            var result = policy.CanBesiege(new SiegePolicyData
            {
                TurnCards = turnCards,
                SiegeCard = PlayingCard.Defaults.AceClubs,
                TurnNumber = 2,
                Defender = DurakPlayer.New
            });
            
            //Assert
            Assert.IsFalse(result);
        }
        
        [Test]
        public void Default_ShouldReturnTrue_WhenThereAreAttackCardWithSiegeCardRank()
        {
            //Arrange
            var policy = new SiegePolicyDefault(2);
            
            var turnCards = new TurnCardsContainer();
            turnCards.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.EightDiamonds));

            //Act
            var result = policy.CanBesiege(new SiegePolicyData
            {
                TurnCards = turnCards,
                SiegeCard = PlayingCard.Defaults.EightClubs,
                TurnNumber = 3,
                Defender = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceSpades)
            });
            
            //Assert
            Assert.IsTrue(result);
        }
        
        [Test]
        public void Default_ShouldReturnTrue_WhenThereAreDefenseCardWithSiegeCardRank()
        {
            //Arrange
            var policy = new SiegePolicyDefault(2);
            
            var turnCards = new TurnCardsContainer();
            turnCards.AddAttackCard(TurnAttackCard.WithoutPlayer(PlayingCard.Defaults.EightDiamonds));
            turnCards.AddDefenseCard(PlayingCard.Defaults.NineDiamonds, 0);

            //Act
            var result = policy.CanBesiege(new SiegePolicyData
            {
                TurnCards = turnCards,
                SiegeCard = PlayingCard.Defaults.NineClubs,
                TurnNumber = 1,
                Defender = DurakPlayer.New.PushCards(PlayingCard.Defaults.AceSpades)
            });
            
            //Assert
            Assert.IsTrue(result);
        }
    }
}