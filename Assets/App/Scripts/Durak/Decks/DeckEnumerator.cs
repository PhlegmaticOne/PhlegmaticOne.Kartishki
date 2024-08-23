using System.Collections.Generic;
using System.Linq;
using Kartishki.Core;
using Kartishki.Core.Components;

namespace App.Scripts.Durak.Decks
{
    internal static class DeckEnumerator
    {
        public static IEnumerable<PlayingCard> Enumerate36Cards()
        {
            return 
                from rank in From6ToAce() 
                from suit in EnumerateSuits()
                select PlayingCard.Create().Card().WithRank(rank).WithSuit(suit);
        }
        
        public static IEnumerable<PlayingCard> Enumerate52Cards()
        {
            return 
                from rank in From2ToAce() 
                from suit in EnumerateSuits() 
                select PlayingCard.Create().Card().WithRank(rank).WithSuit(suit);
        }
        
        public static IEnumerable<PlayingCard> Enumerate54Cards()
        {
            foreach (var rank in From2ToAce())
            {
                foreach (var suit in EnumerateSuits())
                {
                    yield return PlayingCard.Create().Card().WithRank(rank).WithSuit(suit);
                }
            }

            yield return PlayingCard.Create().Joker().WithColor(PlayingCardConsts.RedColor);
            yield return PlayingCard.Create().Joker().WithColor(PlayingCardConsts.RedColor);
        }

        private static IEnumerable<SuitComponent> EnumerateSuits()
        {
            yield return SuitComponent.Spades;
            yield return SuitComponent.Hearts;
            yield return SuitComponent.Diamonds;
            yield return SuitComponent.Clubs;
        }
        
        private static IEnumerable<RankComponent> From6ToAce()
        {
            yield return RankComponent.Six;
            yield return RankComponent.Seven;
            yield return RankComponent.Eight;
            yield return RankComponent.Nine;
            yield return RankComponent.Ten;
            yield return RankComponent.Jack;
            yield return RankComponent.Queen;
            yield return RankComponent.King;
            yield return RankComponent.Ace;
        }
        
        private static IEnumerable<RankComponent> From2ToAce()
        {
            yield return RankComponent.Two;
            yield return RankComponent.Three;
            yield return RankComponent.Four;
            yield return RankComponent.Five;
            yield return RankComponent.Six;
            yield return RankComponent.Seven;
            yield return RankComponent.Eight;
            yield return RankComponent.Nine;
            yield return RankComponent.Ten;
            yield return RankComponent.Jack;
            yield return RankComponent.Queen;
            yield return RankComponent.King;
            yield return RankComponent.Ace;
        }
    }
}