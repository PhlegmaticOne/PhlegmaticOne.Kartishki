namespace App.Scripts.Durak.Game
{
    public interface IDurakGameResultProvider
    {
        DurakGameResult GameResult { get; }
        void Update();
    }
}