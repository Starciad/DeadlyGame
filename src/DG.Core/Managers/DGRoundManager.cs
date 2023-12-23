using DG.Core.Objects;

namespace DG.Core.Managers
{
    internal sealed class DGRoundManager : DGObject
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
    }
}
