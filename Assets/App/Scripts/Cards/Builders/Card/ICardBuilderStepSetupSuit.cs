using App.Scripts.Cards.Components;

namespace App.Scripts.Cards.Builders.Card
{
    public interface ICardBuilderStepSetupSuit
    {
        PlayingCard WithSuit(in SuitComponent suit);
    }
}