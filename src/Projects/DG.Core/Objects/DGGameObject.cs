using DeadlyGame.Core.Interface;

namespace DeadlyGame.Core.Objects
{
    public abstract class DGGameObject : IStartable, IUpdatable
    {
        protected DGGame DGGameInstance { get; private set; }

        protected DGGameObject(DGGame game)
        {
            this.DGGameInstance = game;
        }

        public abstract void Start();
        public abstract void Update();
    }
}
