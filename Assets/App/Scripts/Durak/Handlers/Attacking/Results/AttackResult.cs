using System;

namespace App.Scripts.Durak.Handlers.Attacking.Results
{
    public readonly struct AttackResult : IEquatable<AttackResult>
    {
        private AttackResult(bool isSuccess, AttackFailureReason failureReason)
        {
            IsSuccess = isSuccess;
            FailureReason = failureReason;
        }

        public static AttackResult Successful() => new(true, AttackFailureReason.None);
        public static AttackResult InvalidAttackState() => new(false, AttackFailureReason.InvalidAttackData);
        public static AttackResult InvalidGameState() => new(false, AttackFailureReason.InvalidGameState);

        public bool IsSuccess { get; }
        public AttackFailureReason FailureReason { get; }

        public bool Equals(AttackResult other)
        {
            return IsSuccess == other.IsSuccess && FailureReason == other.FailureReason;
        }

        public override bool Equals(object obj)
        {
            return obj is AttackResult other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsSuccess, (int)FailureReason);
        }
    }
}