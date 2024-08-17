using Kartishki.Core.Components;

namespace Kartishki.Core.Builders.Card
{
    public interface ICardBuilderStepSetupRank
    {
        ICardBuilderStepSetupSuit WithRank(in RankComponent rank);
        ICardBuilderStepSetupSuit WithRank(int value, string name);
    }
}