using Kartishki.Core.Builders.Card;
using Kartishki.Core.Builders.Joker;

namespace Kartishki.Core.Builders
{
    public class PlayerCardBuilder
    {
        private static readonly CardBuilder CardBuilder = new();
        private static readonly JokerBuilder JokerBuilder = new();
        
        public ICardBuilderStepSetupRank Card() => CardBuilder;
        public IJokerBuilder Joker() => JokerBuilder;
    }
}