using Kartishki.Core.Components;

namespace Kartishki.Core.Builders.Joker
{
    public interface IJokerBuilder
    {
        PlayingCard WithComponent(in JokerComponent joker);
        PlayingCard WithColor(int color);
    }
}