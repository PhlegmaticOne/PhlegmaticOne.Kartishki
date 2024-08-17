using App.Scripts.Durak.Decks;
using Kartishki.Core;

namespace App.Scripts.Durak.Handlers.Defense.Policies
{
    public struct DefensePolicyData
    {
        public PlayingCard AttackCard { get; set; }
        public PlayingCard DefenseCard { get; set; }
        public IDeck Deck { get; set; }
    }
}