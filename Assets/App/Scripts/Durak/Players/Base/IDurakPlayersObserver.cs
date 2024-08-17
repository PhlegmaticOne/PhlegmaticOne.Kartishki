using System.Collections.Generic;
using App.Scripts.Durak.Players.Models;

namespace App.Scripts.Durak.Players.Base
{
    public interface IDurakPlayersObserver
    {
        Defender Defender { get; }
        Attacker Attacker { get; }
        IReadOnlyList<Attacker> SiegePlayers { get; }
        IReadOnlyList<DurakPlayer> AllPlayers { get; }
        DurakPlayer GetNextPlayer(DurakPlayer player);
    }
}