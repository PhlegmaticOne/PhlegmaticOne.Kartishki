using App.Scripts.Cards;
using App.Scripts.Durak.Decks;

namespace App.Scripts.Durak.Handlers.Defense.Policies
{
    public struct DefensePolicyData
    {
        public PlayingCard AttackCard { get; set; }
        public PlayingCard DefenseCard { get; set; }
        public IDeck Deck { get; set; }
    }
}