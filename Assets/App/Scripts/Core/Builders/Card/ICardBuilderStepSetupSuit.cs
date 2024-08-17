using Kartishki.Core.Components;

namespace Kartishki.Core.Builders.Card
{
    public interface ICardBuilderStepSetupSuit
    {
        PlayingCard WithSuit(in SuitComponent suit);
        PlayingCard WithSuit(char value, string name, int color);
    }
}