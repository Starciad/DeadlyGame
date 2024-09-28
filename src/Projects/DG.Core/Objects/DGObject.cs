namespace DeadlyGame.Core.Objects
{
    public abstract class DGObject
    {
        protected DGGame Game { get; private set; }

        public virtual void SetGameInstance(DGGame game)
        {
            this.Game = game;
        }

        public void Initialize()
        {
            OnAwake();
            OnStart();
        }
        public void Update()
        {
            OnUpdate();
        }

        protected virtual void OnAwake() { }
        protected virtual void OnStart() { }
        protected virtual void OnUpdate() { }
    }
}
