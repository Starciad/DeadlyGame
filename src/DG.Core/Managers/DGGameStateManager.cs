namespace DG.Core.Managers
{
    internal sealed class DGGameStateManager : DGManager
    {
        internal delegate void GameStartedEventHandler();
        internal delegate void GameFinishedEventHandler();
        internal delegate void GameCanceledEventHandler();

        internal event GameStartedEventHandler OnGameStarted;
        internal event GameFinishedEventHandler OnGameFinished;
        internal event GameCanceledEventHandler OnGameCanceled;

        internal bool IsStarted => this.isStarted;
        internal bool IsFinished => this.isFinished;
        internal bool IsCanceled => this.isCanceled;

        private bool isStarted;
        private bool isFinished;
        private bool isCanceled;

        internal void Start()
        {
            this.OnGameStarted?.Invoke();
            this.isStarted = true;
        }

        internal void Finish()
        {
            this.OnGameFinished?.Invoke();
            this.isFinished = true;
        }

        internal void Cancel()
        {
            this.OnGameCanceled?.Invoke();
            this.isCanceled = true;
        }
    }
}
