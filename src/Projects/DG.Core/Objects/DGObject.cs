using DeadlyGame.Core.Interface;

namespace DeadlyGame.Core.Objects
{
    public abstract class DGObject : IStartable, IUpdatable
    {
        protected DGGame DGGameInstance { get; private set; }

        protected DGObject(DGGame game)
        {
            this.DGGameInstance = game;
        }

        public abstract void Start();
        public abstract void Update();
    }
}
