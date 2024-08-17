using System;
using App.Scripts.Durak.Players.Models;

namespace App.Scripts.Durak.Game
{
    public readonly struct DurakGameResult : IEquatable<DurakGameResult>
    {
        public static DurakGameResult Lost(DurakPlayer durak)
        {
            return new DurakGameResult(true, durak);
        }

        public static DurakGameResult Active()
        {
            return new DurakGameResult(false, null);
        }
        
        private DurakGameResult(bool isEnded, DurakPlayer durak)
        {
            IsEnded = isEnded;
            Durak = durak;
        }

        public bool IsEnded { get; }
        public DurakPlayer Durak { get; }

        public bool Equals(DurakGameResult other)
        {
            return IsEnded == other.IsEnded && Equals(Durak, other.Durak);
        }

        public override bool Equals(object obj)
        {
            return obj is DurakGameResult other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsEnded, Durak);
        }
    }
}