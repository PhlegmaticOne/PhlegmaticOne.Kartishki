namespace App.Scripts.Durak.Game
{
    public class DurakGameResultProvider : IDurakGameResultProvider
    {
        private readonly IDurakGameResultChecker _gameResultChecker;

        public DurakGameResultProvider(IDurakGameResultChecker gameResultChecker)
        {
            _gameResultChecker = gameResultChecker;
        }
        
        public DurakGameResult GameResult { get; private set; }

        public void Update()
        {
            GameResult = _gameResultChecker.CheckResult();
        }
    }
}