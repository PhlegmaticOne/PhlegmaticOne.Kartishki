using System.Linq;
using App.Scripts.Durak.Decks;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Circle;

namespace App.Scripts.Durak.Game
{
    public class DurakGameResultChecker : IDurakGameResultChecker
    {
        private readonly IDeck _deck;
        private readonly IDurakPlayersObserver _playersObserver;

        public DurakGameResultChecker(IDeck deck, IDurakPlayersObserver playersObserver)
        {
            _deck = deck;
            _playersObserver = playersObserver;
        }
        
        public DurakGameResult CheckResult()
        {
            if (!_deck.IsEmpty)
            {
                return DurakGameResult.Active();
            }

            var playersWithCards = _playersObserver.AllPlayers.Where(x => x.HasCards).ToArray();

            return playersWithCards.Length switch
            {
                0 => DurakGameResult.Lost(_playersObserver.DefenderPlayer()),
                > 1 => DurakGameResult.Active(),
                _ => DurakGameResult.Lost(playersWithCards[0])
            };
        }
    }
}