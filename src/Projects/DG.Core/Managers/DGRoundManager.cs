namespace DeadlyGame.Core.Managers
{
    public sealed class DGRoundManager : DGManager
    {
        public int CurrentRound => this.currentRound;

        private int currentRound;

        internal event RoundStartedEventHandler OnRoundStarted;
        internal event RoundEndedEventHandler OnRoundEnded;

        internal delegate void RoundStartedEventHandler();
        internal delegate void RoundEndedEventHandler();

        protected override void OnAwake()
        {
            this.currentRound = 0;
        }

        internal void Begin()
        {
            this.currentRound++;
            OnRoundStarted?.Invoke();
        }

        internal void End()
        {
            OnRoundEnded?.Invoke();
        }
    }
}
