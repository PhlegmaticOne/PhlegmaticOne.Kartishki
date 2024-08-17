using App.Scripts.Durak.Decks;
using App.Scripts.Durak.Decks.DiscardPiles;
using App.Scripts.Durak.Game;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Turns;

namespace App.Scripts.Durak.Handlers.AcceptDefense.Commit
{
    public class AcceptDefenseCommitHandlerSuccess : AcceptDefenseCommitHandlerBase
    {
        private readonly IDiscardPile _discardPile;

        public AcceptDefenseCommitHandlerSuccess(
            IDurakGameResultProvider gameResultProvider,
            TurnCardsContainer turnCardsContainer, 
            IDurakPlayersChanger playersChanger, 
            IDeck deck,
            IDiscardPile discardPile) : base(gameResultProvider, turnCardsContainer, playersChanger, deck)
        {
            _discardPile = discardPile;
        }

        protected override IPlayingCardConsumer GetCardConsumer(IDurakPlayersObserver playersObserver)
        {
            return _discardPile;
        }

        protected override void ChangePlayers(IDurakPlayersChanger playersChanger)
        {
            playersChanger.ChangePlayersOnDefenceSucceed();
        }
    }
}