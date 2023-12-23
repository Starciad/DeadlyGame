using DG.Core.Information.Round;

namespace DG.Core.Managers
{
    internal sealed class DGRoundManager : DGManager
    {
        internal int CurrentRound => this.currentRound;

        private int currentRound;

        internal delegate void RoundStartedEventHandler();
        internal delegate void RoundEndedEventHandler();

        internal event RoundStartedEventHandler OnRoundStarted;
        internal event RoundEndedEventHandler OnRoundEnded;

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

        internal DGRoundInfo GetInfo()
        {
            return new()
            {
                CurrentRound = this.currentRound,
            };
        }
    }
}
