using App.Scripts.Cards.Components;

namespace App.Scripts.Cards.Builders.Joker
{
    internal class JokerBuilder : IJokerBuilder
    {
        public PlayingCard WithColor(int color)
        {
            return new PlayingCard(color, RankComponent.Joker, SuitComponent.Joker);
        }
    }
}