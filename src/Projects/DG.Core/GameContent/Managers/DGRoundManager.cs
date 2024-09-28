using DeadlyGame.Core.Managers;

namespace DeadlyGame.Core.GameContent.Managers
{
    public sealed class DGRoundManager : DGManager
    {
        public int CurrentRound => this.currentRound;

        private int currentRound;

        public event RoundStartedEventHandler OnRoundStarted;
        public event RoundEndedEventHandler OnRoundEnded;

        public delegate void RoundStartedEventHandler();
        public delegate void RoundEndedEventHandler();

        public DGRoundManager(DGGame game) : base(game)
        {
            this.currentRound = 0;
        }

        public override void Start()
        {
            return;
        }

        public override void Update()
        {
            return;
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
