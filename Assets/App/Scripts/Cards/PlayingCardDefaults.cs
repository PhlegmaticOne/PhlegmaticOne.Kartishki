using System.Diagnostics;
using App.Scripts.Cards.Components;

namespace App.Scripts.Cards
{
    public class PlayingCardDefaults
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard TwoSpades => PlayingCard.Create().Card().WithRank(RankComponent.Two).WithSuit(SuitComponent.Spades);
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard ThreeSpades => PlayingCard.Create().Card().WithRank(RankComponent.Three).WithSuit(SuitComponent.Spades);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard FourSpades => PlayingCard.Create().Card().WithRank(RankComponent.Four).WithSuit(SuitComponent.Spades);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard FiveSpades => PlayingCard.Create().Card().WithRank(RankComponent.Five).WithSuit(SuitComponent.Spades);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard SixSpades => PlayingCard.Create().Card().WithRank(RankComponent.Six).WithSuit(SuitComponent.Spades);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard SevenSpades => PlayingCard.Create().Card().WithRank(RankComponent.Seven).WithSuit(SuitComponent.Spades);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard EightSpades => PlayingCard.Create().Card().WithRank(RankComponent.Eight).WithSuit(SuitComponent.Spades);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard NineSpades => PlayingCard.Create().Card().WithRank(RankComponent.Nine).WithSuit(SuitComponent.Spades);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard TenSpades => PlayingCard.Create().Card().WithRank(RankComponent.Ten).WithSuit(SuitComponent.Spades);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard JackSpades => PlayingCard.Create().Card().WithRank(RankComponent.Jack).WithSuit(SuitComponent.Spades);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard QueenSpades => PlayingCard.Create().Card().WithRank(RankComponent.Queen).WithSuit(SuitComponent.Spades);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard KingSpades => PlayingCard.Create().Card().WithRank(RankComponent.King).WithSuit(SuitComponent.Spades);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard AceSpades => PlayingCard.Create().Card().WithRank(RankComponent.Ace).WithSuit(SuitComponent.Spades);
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard TwoHearts => PlayingCard.Create().Card().WithRank(RankComponent.Two).WithSuit(SuitComponent.Hearts);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard ThreeHearts => PlayingCard.Create().Card().WithRank(RankComponent.Three).WithSuit(SuitComponent.Hearts);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard FourHearts => PlayingCard.Create().Card().WithRank(RankComponent.Four).WithSuit(SuitComponent.Hearts);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard FiveHearts => PlayingCard.Create().Card().WithRank(RankComponent.Five).WithSuit(SuitComponent.Hearts);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard SixHearts => PlayingCard.Create().Card().WithRank(RankComponent.Six).WithSuit(SuitComponent.Hearts);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard SevenHearts => PlayingCard.Create().Card().WithRank(RankComponent.Seven).WithSuit(SuitComponent.Hearts);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard EightHearts => PlayingCard.Create().Card().WithRank(RankComponent.Eight).WithSuit(SuitComponent.Hearts);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard NineHearts => PlayingCard.Create().Card().WithRank(RankComponent.Nine).WithSuit(SuitComponent.Hearts);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard TenHearts => PlayingCard.Create().Card().WithRank(RankComponent.Ten).WithSuit(SuitComponent.Hearts);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard JackHearts => PlayingCard.Create().Card().WithRank(RankComponent.Jack).WithSuit(SuitComponent.Hearts);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard QueenHearts => PlayingCard.Create().Card().WithRank(RankComponent.Queen).WithSuit(SuitComponent.Hearts);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard KingHearts => PlayingCard.Create().Card().WithRank(RankComponent.King).WithSuit(SuitComponent.Hearts);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard AceHearts => PlayingCard.Create().Card().WithRank(RankComponent.Ace).WithSuit(SuitComponent.Hearts);
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard TwoDiamonds => PlayingCard.Create().Card().WithRank(RankComponent.Two).WithSuit(SuitComponent.Diamonds);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard ThreeDiamonds => PlayingCard.Create().Card().WithRank(RankComponent.Three).WithSuit(SuitComponent.Diamonds);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard FourDiamonds => PlayingCard.Create().Card().WithRank(RankComponent.Four).WithSuit(SuitComponent.Diamonds);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard FiveDiamonds => PlayingCard.Create().Card().WithRank(RankComponent.Five).WithSuit(SuitComponent.Diamonds);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard SixDiamonds => PlayingCard.Create().Card().WithRank(RankComponent.Six).WithSuit(SuitComponent.Diamonds);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard SevenDiamonds => PlayingCard.Create().Card().WithRank(RankComponent.Seven).WithSuit(SuitComponent.Diamonds);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard EightDiamonds => PlayingCard.Create().Card().WithRank(RankComponent.Eight).WithSuit(SuitComponent.Diamonds);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard NineDiamonds => PlayingCard.Create().Card().WithRank(RankComponent.Nine).WithSuit(SuitComponent.Diamonds);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard TenDiamonds => PlayingCard.Create().Card().WithRank(RankComponent.Ten).WithSuit(SuitComponent.Diamonds);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard JackDiamonds => PlayingCard.Create().Card().WithRank(RankComponent.Jack).WithSuit(SuitComponent.Diamonds);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard QueenDiamonds => PlayingCard.Create().Card().WithRank(RankComponent.Queen).WithSuit(SuitComponent.Diamonds);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard KingDiamonds => PlayingCard.Create().Card().WithRank(RankComponent.King).WithSuit(SuitComponent.Diamonds);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard AceDiamonds => PlayingCard.Create().Card().WithRank(RankComponent.Ace).WithSuit(SuitComponent.Diamonds);
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard TwoClubs => PlayingCard.Create().Card().WithRank(RankComponent.Two).WithSuit(SuitComponent.Clubs);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard ThreeClubs => PlayingCard.Create().Card().WithRank(RankComponent.Three).WithSuit(SuitComponent.Clubs);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard FourClubs => PlayingCard.Create().Card().WithRank(RankComponent.Four).WithSuit(SuitComponent.Clubs);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard FiveClubs => PlayingCard.Create().Card().WithRank(RankComponent.Five).WithSuit(SuitComponent.Clubs);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard SixClubs => PlayingCard.Create().Card().WithRank(RankComponent.Six).WithSuit(SuitComponent.Clubs);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard SevenClubs => PlayingCard.Create().Card().WithRank(RankComponent.Seven).WithSuit(SuitComponent.Clubs);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard EightClubs => PlayingCard.Create().Card().WithRank(RankComponent.Eight).WithSuit(SuitComponent.Clubs);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard NineClubs => PlayingCard.Create().Card().WithRank(RankComponent.Nine).WithSuit(SuitComponent.Clubs);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard TenClubs => PlayingCard.Create().Card().WithRank(RankComponent.Ten).WithSuit(SuitComponent.Clubs);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard JackClubs => PlayingCard.Create().Card().WithRank(RankComponent.Jack).WithSuit(SuitComponent.Clubs);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard QueenClubs => PlayingCard.Create().Card().WithRank(RankComponent.Queen).WithSuit(SuitComponent.Clubs);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard KingClubs => PlayingCard.Create().Card().WithRank(RankComponent.King).WithSuit(SuitComponent.Clubs);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard AceClubs => PlayingCard.Create().Card().WithRank(RankComponent.Ace).WithSuit(SuitComponent.Clubs);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard JokerRed => PlayingCard.Create().Joker().WithColor(PlayingCardConsts.RedColor);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PlayingCard JokerBlack => PlayingCard.Create().Joker().WithColor(PlayingCardConsts.BlackColor);
    }
}