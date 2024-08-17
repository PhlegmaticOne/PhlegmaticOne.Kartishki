using System;

namespace App.Scripts.Durak.Handlers.FailDefense.Results
{
    public readonly struct FailDefenseResult : IEquatable<FailDefenseResult>
    {
        private FailDefenseResult(bool isSuccess, FailDefenseFailureReason failureReason)
        {
            IsSuccess = isSuccess;
            FailureReason = failureReason;
        }

        public static FailDefenseResult Successful() => new(true, FailDefenseFailureReason.None);
        public static FailDefenseResult PlayerNotDefender() => new(false, FailDefenseFailureReason.PlayerIsNotDefender);
        public static FailDefenseResult DefenderCantAccept() => new(false, FailDefenseFailureReason.DefenderCantAccept);
        public static FailDefenseResult InvalidGameState() => new(false, FailDefenseFailureReason.InvalidGameState);
        public bool IsSuccess { get; }
        public FailDefenseFailureReason FailureReason { get; }

        public bool Equals(FailDefenseResult other)
        {
            return IsSuccess == other.IsSuccess && FailureReason == other.FailureReason;
        }

        public override bool Equals(object obj)
        {
            return obj is FailDefenseResult other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsSuccess, (int)FailureReason);
        }
    }
}