using System;

namespace App.Scripts.Durak.Handlers.Transfer.Results
{
    public readonly struct TransferResult : IEquatable<TransferResult>
    {
        private TransferResult(bool isSuccess, TransferFailureReason failureReason)
        {
            IsSuccess = isSuccess;
            FailureReason = failureReason;
        }

        public static TransferResult Successful() => new(true, TransferFailureReason.None);
        public static TransferResult PlayerNotDefender() => new(false, TransferFailureReason.PlayerIsNotDefender);
        public static TransferResult InvalidTransferCard() => new(false, TransferFailureReason.InvalidTransferCard);
        public static TransferResult InvalidGameState() => new(false, TransferFailureReason.InvalidGameState);
        public bool IsSuccess { get; }
        public TransferFailureReason FailureReason { get; }

        public bool Equals(TransferResult other)
        {
            return IsSuccess == other.IsSuccess && FailureReason == other.FailureReason;
        }

        public override bool Equals(object obj)
        {
            return obj is TransferResult other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsSuccess, (int)FailureReason);
        }
    }
}