using App.Scripts.Cards.Components;

namespace App.Scripts.Cards.Builders.Card
{
    public interface ICardBuilderStepSetupRank
    {
        ICardBuilderStepSetupSuit WithRank(in RankComponent rank);
    }
}