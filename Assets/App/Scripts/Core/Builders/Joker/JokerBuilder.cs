using Kartishki.Core.Components;

namespace Kartishki.Core.Builders.Joker
{
    internal class JokerBuilder : IJokerBuilder
    {
        public PlayingCard WithComponent(in JokerComponent joker)
        {
            return PlayingCard.Create(in CardComponent.Invalid, in joker);
        }

        public PlayingCard WithColor(int color)
        {
            var component = JokerComponent.Create(color);
            return WithComponent(in component);
        }
    }
}