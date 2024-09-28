namespace DG.Core.Objects
{
    internal abstract class DGObject
    {
        protected DGGame Game { get; private set; }

        internal virtual void SetGameInstance(DGGame game)
        {
            this.Game = game;
        }

        internal void Initialize()
        {
            OnAwake();
            OnStart();
        }
        internal void Update()
        {
            OnUpdate();
        }

        protected virtual void OnAwake() { }
        protected virtual void OnStart() { }
        protected virtual void OnUpdate() { }
    }
}
