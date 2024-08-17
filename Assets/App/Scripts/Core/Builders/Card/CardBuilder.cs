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

        public ICardBuilderStepSetupSuit WithRank(int value, string name)
        {
            _rank = RankComponent.Create(value, name);
            return this;
        }

        public PlayingCard WithSuit(in SuitComponent suit)
        {
            var cardComponent = new CardComponent(in _rank, in suit);
            return PlayingCard.Create(in cardComponent, in JokerComponent.Invalid);
        }

        public PlayingCard WithSuit(char value, string name, int color)
        {
            var suit = SuitComponent.Create(value, name, color);
            return WithSuit(in suit);
        }
    }
}