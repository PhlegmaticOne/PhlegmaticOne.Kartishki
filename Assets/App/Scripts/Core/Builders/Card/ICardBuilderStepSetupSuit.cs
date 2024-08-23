using Kartishki.Core.Components;

namespace Kartishki.Core.Builders.Card
{
    public interface ICardBuilderStepSetupSuit
    {
        PlayingCard WithSuit(in SuitComponent suit);
    }
}