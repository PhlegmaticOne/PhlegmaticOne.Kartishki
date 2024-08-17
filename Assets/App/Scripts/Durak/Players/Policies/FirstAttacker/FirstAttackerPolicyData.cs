using System.Collections.Generic;
using App.Scripts.Durak.Decks;
using App.Scripts.Durak.Players.Models;

namespace App.Scripts.Durak.Players.Policies.FirstAttacker
{
    public struct FirstAttackerPolicyData
    {
        public IReadOnlyList<DurakPlayer> AllPlayers { get; set; }
        public IDeck Deck { get; set; }
    }
}