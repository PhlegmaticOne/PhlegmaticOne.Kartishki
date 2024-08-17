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
                from rank in RankComponent.From6ToAce() 
                from suit in SuitComponent.Enumerate() 
                select PlayingCard.Create().Card().WithRank(rank).WithSuit(suit);
        }
        
        public static IEnumerable<PlayingCard> Enumerate52Cards()
        {
            return 
                from rank in RankComponent.From2ToAce() 
                from suit in SuitComponent.Enumerate() 
                select PlayingCard.Create().Card().WithRank(rank).WithSuit(suit);
        }
        
        public static IEnumerable<PlayingCard> Enumerate54Cards()
        {
            foreach (var rank in RankComponent.From2ToAce())
            {
                foreach (var suit in SuitComponent.Enumerate())
                {
                    yield return PlayingCard.Create().Card().WithRank(rank).WithSuit(suit);
                }
            }

            yield return PlayingCard.Create().Joker().WithComponent(JokerComponent.Red);
            yield return PlayingCard.Create().Joker().WithComponent(JokerComponent.Black);
        }
    }
}