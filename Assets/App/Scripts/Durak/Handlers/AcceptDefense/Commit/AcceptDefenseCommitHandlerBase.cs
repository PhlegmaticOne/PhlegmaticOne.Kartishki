using App.Scripts.Durak.Decks;
using App.Scripts.Durak.Decks.Extensions;
using App.Scripts.Durak.Game;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Extensions;
using App.Scripts.Durak.Turns;

namespace App.Scripts.Durak.Handlers.AcceptDefense.Commit
{
    public abstract class AcceptDefenseCommitHandlerBase : IAcceptDefenseCommitHandler
    {
        private readonly IDurakGameResultProvider _gameResultProvider;
        private readonly TurnCardsContainer _turnCardsContainer;
        private readonly IDurakPlayersChanger _playersChanger;
        private readonly IDeck _deck;

        protected AcceptDefenseCommitHandlerBase(
            IDurakGameResultProvider gameResultProvider,
            TurnCardsContainer turnCardsContainer,
            IDurakPlayersChanger playersChanger,
            IDeck deck)
        {
            _gameResultProvider = gameResultProvider;
            _turnCardsContainer = turnCardsContainer;
            _playersChanger = playersChanger;
            _deck = deck;
        }
        
        public void Commit()
        {
            var tableCards = _turnCardsContainer.TakeCards();
            GetCardConsumer(_playersChanger).PushCards(tableCards);
            FillPlayersWithCards();
            ChangePlayers(_playersChanger);
            _turnCardsContainer.IncreaseTurnNumber();
            _gameResultProvider.Update();
            _turnCardsContainer.Clear();
        }

        protected abstract IPlayingCardConsumer GetCardConsumer(IDurakPlayersObserver playersObserver);
        protected abstract void ChangePlayers(IDurakPlayersChanger playersChanger);

        private void FillPlayersWithCards()
        {
            _deck.FillCardConsumer(_playersChanger.Attacker);
            _deck.FillCardConsumers(_playersChanger.SiegePlayers);
            _deck.FillCardConsumer(_playersChanger.Defender);
        }
    }
}