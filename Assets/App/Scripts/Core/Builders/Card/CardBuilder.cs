using Kartishki.Core.Components;

namespace Kartishki.Core.Builders.Card
{
    internal class CardBuilder : ICardBuilderStepSetupRank, ICardBuilderStepSetupSuit
    {
        private RankComponent _rank;
        
        public ICardBuilderStepSetupSuit WithRank(in RankComponent rank)
        {
            _rank = rank;
            return this;
        }

        public PlayingCard WithSuit(in SuitComponent suit)
        {
            var color = suit.Color;
            return new PlayingCard(color, _rank, suit);
        }
    }
}