using System;

namespace App.Scripts.Durak.Handlers.Defense.Results
{
    public readonly struct DefenseResult : IEquatable<DefenseResult>
    {
        private DefenseResult(bool isSuccess, DefenseFailureReason failureReason)
        {
            IsSuccess = isSuccess;
            FailureReason = failureReason;
        }

        public static DefenseResult Successful() => new(true, DefenseFailureReason.None);
        public static DefenseResult PlayerNotDefender() => new(false, DefenseFailureReason.PlayerIsNotDefender);
        public static DefenseResult InvalidDefenseCard() => new(false, DefenseFailureReason.InvalidDefenseCard);
        public static DefenseResult InvalidGameState() => new(false, DefenseFailureReason.InvalidGameState);
        public bool IsSuccess { get; }
        public DefenseFailureReason FailureReason { get; }

        public bool Equals(DefenseResult other)
        {
            return IsSuccess == other.IsSuccess && FailureReason == other.FailureReason;
        }

        public override bool Equals(object obj)
        {
            return obj is DefenseResult other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsSuccess, (int)FailureReason);
        }
    }
}