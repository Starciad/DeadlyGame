namespace DeadlyGame.Core.Managers
{
    public sealed class DGGameStateManager : DGManager
    {
        public delegate void GameStartedEventHandler();
        public delegate void GameFinishedEventHandler();
        public delegate void GameCanceledEventHandler();

        public event GameStartedEventHandler OnGameStarted;
        public event GameFinishedEventHandler OnGameFinished;
        public event GameCanceledEventHandler OnGameCanceled;

        public bool IsStarted => this.isStarted;
        public bool IsFinished => this.isFinished;
        public bool IsCanceled => this.isCanceled;

        private bool isStarted;
        private bool isFinished;
        private bool isCanceled;

        public DGGameStateManager(DGGame game) : base(game)
        {

        }

        public void StartGame()
        {
            this.OnGameStarted?.Invoke();
            this.isStarted = true;
        }

        public void FinishGame()
        {
            this.OnGameFinished?.Invoke();
            this.isFinished = true;
        }

        public void CancelGame()
        {
            this.OnGameCanceled?.Invoke();
            this.isCanceled = true;
        }
    }
}
