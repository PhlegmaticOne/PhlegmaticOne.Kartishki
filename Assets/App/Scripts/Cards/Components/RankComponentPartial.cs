using System.Collections.Generic;

namespace App.Scripts.Cards.Components
{
    public partial struct RankComponent
    {
        private static readonly Dictionary<string, int> RanksLetterMap = new()
        {
            { PlayingCardConsts.Jack, 11 },
            { PlayingCardConsts.Queen, 12 },
            { PlayingCardConsts.King, 13 },
            { PlayingCardConsts.Ace, 14 },
            { PlayingCardConsts.Joker, 15 },
        };
        
        public static RankComponent MaxPossible = new(int.MaxValue, string.Empty); 

        public static RankComponent Invalid = new(-1, string.Empty);
        public static RankComponent Two => new(2, PlayingCardConsts.Two);
        public static RankComponent Three => new(3, PlayingCardConsts.Three);
        public static RankComponent Four => new(4, PlayingCardConsts.Four);
        public static RankComponent Five => new(5, PlayingCardConsts.Five);
        public static RankComponent Six => new(6, PlayingCardConsts.Six);
        public static RankComponent Seven => new(7, PlayingCardConsts.Seven);
        public static RankComponent Eight => new(8, PlayingCardConsts.Eight);
        public static RankComponent Nine => new(9, PlayingCardConsts.Nine);
        public static RankComponent Ten => new(10, PlayingCardConsts.Ten);
        public static RankComponent Jack => new(RanksLetterMap[PlayingCardConsts.Jack], PlayingCardConsts.Jack);
        public static RankComponent Queen => new(RanksLetterMap[PlayingCardConsts.Queen], PlayingCardConsts.Queen);
        public static RankComponent King => new(RanksLetterMap[PlayingCardConsts.King], PlayingCardConsts.King);
        public static RankComponent Ace => new(RanksLetterMap[PlayingCardConsts.Ace], PlayingCardConsts.Ace);
        public static RankComponent Joker => new(RanksLetterMap[PlayingCardConsts.Joker], PlayingCardConsts.Joker);
    }
}