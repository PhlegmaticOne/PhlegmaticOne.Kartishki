using Kartishki.Core.Components;

namespace Kartishki.Core.Builders.Joker
{
    internal class JokerBuilder : IJokerBuilder
    {
        public PlayingCard WithColor(int color)
        {
            return new PlayingCard(color, RankComponent.Joker, SuitComponent.Joker);
        }
    }
}