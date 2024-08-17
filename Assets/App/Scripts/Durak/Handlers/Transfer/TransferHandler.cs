using App.Scripts.Durak.Handlers.Transfer.Policies;
using App.Scripts.Durak.Handlers.Transfer.Results;
using App.Scripts.Durak.Players.Base;
using App.Scripts.Durak.Players.Circle;
using App.Scripts.Durak.Players.Models;
using App.Scripts.Durak.Turns;
using Kartishki.Core;

namespace App.Scripts.Durak.Handlers.Transfer
{
    public class TransferHandler : ITransferHandler
    {
        private readonly IDurakPlayersChanger _playersChanger;
        private readonly ITransferPolicy _transferPolicy;
        private readonly TurnCardsContainer _turnCardsContainer;

        public TransferHandler(
            IDurakPlayersChanger playersChanger, 
            ITransferPolicy transferPolicy,
            TurnCardsContainer turnCardsContainer)
        {
            _playersChanger = playersChanger;
            _transferPolicy = transferPolicy;
            _turnCardsContainer = turnCardsContainer;
        }
        
        public TransferResult Handle(in TransferHandlerData handlerData)
        {
            var player = handlerData.Player;
            
            if (!_playersChanger.IsDefender(player))
            {
                return TransferResult.PlayerNotDefender();
            }
            
            var isSuccessTransfer = PerformTransfer(player, handlerData);
            return isSuccessTransfer ? TransferResult.Successful() : TransferResult.InvalidTransferCard();
        }

        private bool PerformTransfer(DurakPlayer transferPlayer, in TransferHandlerData handlerData)
        {
            var transferCard = transferPlayer.GetCardAt(handlerData.CardIndex);
            
            if (!CanTransfer(transferCard, transferPlayer))
            {
                return false;
            }
            
            _playersChanger.ChangePlayersOnDefenceSucceed();
            _turnCardsContainer.AddAttackCard(new TurnAttackCard(transferCard, _playersChanger.Attacker));
            return true;
        }

        private bool CanTransfer(PlayingCard transferCard, DurakPlayer transferPlayer)
        {
            return _transferPolicy.CanTransfer(new TransferPolicyData
            {
                Card = transferCard,
                TurnCards = _turnCardsContainer,
                NextPlayer = _playersChanger.GetNextPlayer(transferPlayer)
            });
        }
    }
}