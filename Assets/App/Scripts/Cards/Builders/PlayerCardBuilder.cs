using App.Scripts.Cards.Builders.Card;
using App.Scripts.Cards.Builders.Joker;

namespace App.Scripts.Cards.Builders
{
    public class PlayerCardBuilder
    {
        private static readonly CardBuilder CardBuilder = new();
        private static readonly JokerBuilder JokerBuilder = new();
        
        public ICardBuilderStepSetupRank Card() => CardBuilder;
        public IJokerBuilder Joker() => JokerBuilder;
    }
}