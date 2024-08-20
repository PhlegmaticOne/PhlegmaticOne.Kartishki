using System.Collections;
using System.Collections.Generic;
using App.Scripts.Durak.Decks;
using App.Scripts.Durak.Players.Extensions;
using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Players.Policies.FirstAttacker;
using Kartishki.Core;
using Moq;
using NUnit.Framework;

namespace App.Scripts.EditTests.Durak.Players.Policies
{
    [TestFixture]
    public class FirstAttackerPolicyTests
    {
        private static class Mocks
        {
            public static PlayingCard Trump => PlayingCard.Defaults.NineClubs;
            
            public static IDeck CreateDeckWithTrumpCard(PlayingCard trumpCard)
            {
                var mock = new Mock<IDeck>();
                mock.SetupGet(x => x.Trump).Returns(trumpCard);
                return mock.Object;
            }
        }
        
        [TestCaseSource(typeof(FirstAttackerPolicyDataLowestTrumpData), nameof(FirstAttackerPolicyDataLowestTrumpData.Get))]
        public DurakPlayer LowestTrump_Test(List<DurakPlayer> allPlayers)
        {
            //Arrange
            var deck = Mocks.CreateDeckWithTrumpCard(Mocks.Trump);
            var policy = new FirstAttackerPolicyLowestTrump();
            
            //Act
            var firstAttackPlayer = policy.GetFirstAttacker(new FirstAttackerPolicyData
            {
                Deck = deck,
                AllPlayers = allPlayers
            });
            
            //Assert
            return firstAttackPlayer;
        }
    }

    internal class FirstAttackerPolicyDataLowestTrumpData
    {
        public static IEnumerable Get()
        {
            var withTrump = WithTrump();
            yield return new TestCaseData(withTrump.allPlayers)
                .Returns(withTrump.firstAttacker)
                .SetName("Returns player with lowest trump");

            var withoutTrump = WithoutTrump();
            yield return new TestCaseData(withoutTrump.allPlayers)
                .Returns(withoutTrump.firstAttacker)
                .SetName("Return first player because no one have trump");
        }

        private static (List<DurakPlayer> allPlayers, DurakPlayer firstAttacker) WithTrump()
        {
            var firstPlayer = DurakPlayer.New.PushCards(
                PlayingCard.Defaults.EightClubs,
                PlayingCard.Defaults.AceDiamonds,
                PlayingCard.Defaults.EightHearts);

            var secondPlayer = DurakPlayer.New.PushCards(
                PlayingCard.Defaults.FourClubs,
                PlayingCard.Defaults.AceSpades,
                PlayingCard.Defaults.KingHearts);

            var thirdPlayer = DurakPlayer.New.PushCards(
                PlayingCard.Defaults.FourDiamonds,
                PlayingCard.Defaults.FourHearts,
                PlayingCard.Defaults.QueenDiamonds);

            var players = new List<DurakPlayer>
            {
                firstPlayer, secondPlayer, thirdPlayer
            };

            return (players, secondPlayer);
        }

        private static (List<DurakPlayer> allPlayers, DurakPlayer firstAttacker) WithoutTrump()
        {
            var firstPlayer = DurakPlayer.New.PushCards(
                PlayingCard.Defaults.FiveDiamonds,
                PlayingCard.Defaults.AceDiamonds,
                PlayingCard.Defaults.EightHearts);

            var secondPlayer = DurakPlayer.New.PushCards(
                PlayingCard.Defaults.AceHearts,
                PlayingCard.Defaults.AceSpades,
                PlayingCard.Defaults.KingHearts);

            var thirdPlayer = DurakPlayer.New.PushCards(
                PlayingCard.Defaults.FourDiamonds,
                PlayingCard.Defaults.FourHearts,
                PlayingCard.Defaults.QueenDiamonds);

            var players = new List<DurakPlayer>
            {
                firstPlayer, secondPlayer, thirdPlayer
            };

            return (players, firstPlayer);
        }
    }
}