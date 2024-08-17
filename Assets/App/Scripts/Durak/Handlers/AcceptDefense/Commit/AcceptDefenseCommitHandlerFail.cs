using App.Scripts.Durak.Decks;
using App.Scripts.Durak.Game;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Circle;
using App.Scripts.Durak.Turns;

namespace App.Scripts.Durak.Handlers.AcceptDefense.Commit
{
    public class AcceptDefenseCommitHandlerFail : AcceptDefenseCommitHandlerBase
    {
        public AcceptDefenseCommitHandlerFail(
            IDurakGameResultProvider gameResultProvider,
            TurnCardsContainer turnCardsContainer, 
            IDurakPlayersChanger playersChanger, 
            IDeck deck) : base(gameResultProvider, turnCardsContainer, playersChanger, deck) { }

        protected override IPlayingCardConsumer GetCardConsumer(IDurakPlayersObserver playersObserver)
        {
            return playersObserver.DefenderPlayer();
        }

        protected override void ChangePlayers(IDurakPlayersChanger playersChanger)
        {
            playersChanger.ChangePlayersOnDefenceFailed();
        }
    }
}