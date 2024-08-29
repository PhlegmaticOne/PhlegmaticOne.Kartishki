using System;
using System.Runtime.CompilerServices;
using App.Scripts.Durak.Factory;
using App.Scripts.Durak.Game;
using App.Scripts.Durak.Handlers.AcceptDefense;
using App.Scripts.Durak.Handlers.AcceptDefense.Results;
using App.Scripts.Durak.Handlers.Attacking;
using App.Scripts.Durak.Handlers.Attacking.Results;
using App.Scripts.Durak.Handlers.Defense;
using App.Scripts.Durak.Handlers.Defense.Results;
using App.Scripts.Durak.Handlers.FailDefense;
using App.Scripts.Durak.Handlers.FailDefense.Results;
using App.Scripts.Durak.Handlers.Transfer;
using App.Scripts.Durak.Handlers.Transfer.Results;

namespace App.Scripts.Durak
{
    public class DurakSession
    {
        private readonly IDurakGameResultProvider _gameResultProvider;
        
        private readonly IAttackHandler _attackHandler;
        private readonly IDefenseHandler _defenseHandler;
        private readonly ITransferHandler _transferHandler;
        private readonly IFailDefenseHandler _failDefenseHandler;
        private readonly IAcceptDefenseHandler _acceptDefenseHandler;

        public Guid Id { get; }
        public DurakGameResult GameResult => _gameResultProvider.GameResult;

        public static DurakSession Create(in DurakSessionFactoryData factoryData)
        {
            return DurakSessionFactory.Create(factoryData);
        }

        internal DurakSession(
            Guid id,
            IDurakGameResultProvider gameResultProvider,
            IAttackHandler attackHandler,
            IDefenseHandler defenseHandler,
            ITransferHandler transferHandler,
            IFailDefenseHandler failDefenseHandler,
            IAcceptDefenseHandler acceptDefenseHandler)
        {
            Id = id;
            _gameResultProvider = gameResultProvider;
            _attackHandler = attackHandler;
            _defenseHandler = defenseHandler;
            _transferHandler = transferHandler;
            _failDefenseHandler = failDefenseHandler;
            _acceptDefenseHandler = acceptDefenseHandler;
        }

        public AcceptDefenseResult AcceptDefence(in AcceptDefenseHandlerData handlerData)
        {
            if (IsGameEnd())
            {
                return AcceptDefenseResult.Unaccepted();
            }

            return _acceptDefenseHandler.Handle(handlerData);
        }

        public AttackResult Attack(in AttackHandlerData handlerData)
        {
            if (IsGameEnd())
            {
                return AttackResult.InvalidGameState();
            }

            return _attackHandler.Handle(handlerData);
        }

        public DefenseResult Defend(in DefenseHandlerData handlerData)
        {
            if (IsGameEnd())
            {
                return DefenseResult.InvalidGameState();
            }

            return _defenseHandler.Handle(handlerData);
        }

        public TransferResult Transfer(in TransferHandlerData handlerData)
        {
            if (IsGameEnd())
            {
                return TransferResult.InvalidGameState();
            }

            return _transferHandler.Handle(handlerData);
        }

        public FailDefenseResult FailDefence(in FailDefenseHandlerData handlerData)
        {
            if (IsGameEnd())
            {
                return FailDefenseResult.InvalidGameState();
            }

            return _failDefenseHandler.Handle(handlerData);
        }

        private bool IsGameEnd()
        {
            return GameResult.IsEnded;
        }
    }
}