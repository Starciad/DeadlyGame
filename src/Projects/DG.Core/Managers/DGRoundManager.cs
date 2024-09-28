namespace DeadlyGame.Core.Managers
{
    public sealed class DGRoundManager : DGManager
    {
        public int CurrentRound => this.currentRound;

        private int currentRound;

        public event RoundStartedEventHandler OnRoundStarted;
        public event RoundEndedEventHandler OnRoundEnded;

        public delegate void RoundStartedEventHandler();
        public delegate void RoundEndedEventHandler();

        protected override void OnAwake()
        {
            this.currentRound = 0;
        }

        public void Begin()
        {
            this.currentRound++;
            OnRoundStarted?.Invoke();
        }

        public void End()
        {
            OnRoundEnded?.Invoke();
        }
    }
}
