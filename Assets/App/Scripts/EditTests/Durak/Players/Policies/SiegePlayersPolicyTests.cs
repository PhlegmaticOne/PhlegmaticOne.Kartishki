using System.Collections.Generic;
using System.Linq;
using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Players.Policies.Siege;
using NUnit.Framework;

namespace App.Scripts.EditTests.Durak.Players.Policies
{
    [TestFixture]
    public class SiegePlayersPolicyTests
    {
        [TestCaseSource(typeof(SiegePlayersPolicyNeighborsDataSource), nameof(SiegePlayersPolicyNeighborsDataSource.Get))]
        public void NeighborsTest(SiegePlayersPolicyNeighborsData data)
        {
            //Arrange
            var policy = new SiegePlayersPolicyNeighbors();

            //Act
            var siegePlayers = policy.GetSiegePlayers(new SiegePolicyData
            {
                Defender = data.Defender,
                AllPlayers = data.AllPlayers
            });
            
            //Assert
            Assert.IsTrue(siegePlayers.Select(x => x.Player).SequenceEqual(data.Neighbors));
        }
    }

    public class SiegePlayersPolicyNeighborsData
    {
        private readonly string _description;
        public DurakPlayer Defender { get; }
        public List<DurakPlayer> AllPlayers { get; }
        public List<DurakPlayer> Neighbors { get; }

        public SiegePlayersPolicyNeighborsData(DurakPlayer defender, List<DurakPlayer> allPlayers, List<DurakPlayer> neighbors, string description)
        {
            _description = description;
            Defender = defender;
            AllPlayers = allPlayers;
            Neighbors = neighbors;
        }

        public override string ToString()
        {
            return _description;
        }
    }
    
    internal class SiegePlayersPolicyNeighborsDataSource
    {
        internal static IEnumerable<SiegePlayersPolicyNeighborsData> Get()
        {
            yield return NextAndPreviousIsLastInList();
            yield return NextAndPreviousInList();
            yield return NextIsFirstInListAndPrevious();
        }

        private static SiegePlayersPolicyNeighborsData NextAndPreviousIsLastInList()
        {
            var defender = DurakPlayer.New;
            var clockwise = DurakPlayer.New;
            var counterclockwise = DurakPlayer.New;
            var opposite = DurakPlayer.New;

            return new SiegePlayersPolicyNeighborsData(defender,
                new List<DurakPlayer> { defender, clockwise, opposite, counterclockwise },
                new List<DurakPlayer> { clockwise, counterclockwise },
                "Next player is in list and previous is last in list");
        }

        private static SiegePlayersPolicyNeighborsData NextAndPreviousInList()
        {
            var defender = DurakPlayer.New;
            var clockwise = DurakPlayer.New;
            var counterclockwise = DurakPlayer.New;
            var opposite = DurakPlayer.New;

            return new SiegePlayersPolicyNeighborsData(defender,
                new List<DurakPlayer> { opposite, counterclockwise, defender, clockwise },
                new List<DurakPlayer> { clockwise, counterclockwise },
                "Next player is in list and previous is in list");
        }

        private static SiegePlayersPolicyNeighborsData NextIsFirstInListAndPrevious()
        {
            var defender = DurakPlayer.New;
            var clockwise = DurakPlayer.New;
            var counterclockwise = DurakPlayer.New;
            var opposite = DurakPlayer.New;

            return new SiegePlayersPolicyNeighborsData(defender,
                new List<DurakPlayer> { clockwise, opposite, counterclockwise, defender },
                new List<DurakPlayer> { clockwise, counterclockwise },
                "Next player is first in list and previous is in list");
        }
    }
}