using System;

namespace App.Scripts.Durak.Handlers.AcceptDefense.Results
{
    public readonly struct AcceptDefenseResult : IEquatable<AcceptDefenseResult>
    {
        private AcceptDefenseResult(bool isSuccess, AcceptDefenseActionType actionType)
        {
            IsSuccess = isSuccess;
            ActionType = actionType;
        }

        public static AcceptDefenseResult Accepted() => new(true, AcceptDefenseActionType.Accepted);
        public static AcceptDefenseResult Unaccepted() => new(false, AcceptDefenseActionType.Unaccepted);
        public static AcceptDefenseResult ResultFailDefense() => new(true, AcceptDefenseActionType.ResultFailDefense);
        public static AcceptDefenseResult ResultSuccessDefense() => new(true, AcceptDefenseActionType.ResultSuccessDefense);
        public bool IsSuccess { get; }
        public AcceptDefenseActionType ActionType { get; }

        public bool Equals(AcceptDefenseResult other)
        {
            return IsSuccess == other.IsSuccess && ActionType == other.ActionType;
        }

        public override bool Equals(object obj)
        {
            return obj is AcceptDefenseResult other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsSuccess, (int)ActionType);
        }
    }
}