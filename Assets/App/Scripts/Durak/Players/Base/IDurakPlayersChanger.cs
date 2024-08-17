namespace App.Scripts.Durak.Players.Base
{
    public interface IDurakPlayersChanger : IDurakPlayersObserver
    {
        void ChangePlayersOnDefenceFailed();
        void ChangePlayersOnDefenceSucceed();
    }
}