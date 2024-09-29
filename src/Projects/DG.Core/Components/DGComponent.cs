using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Objects;

namespace DeadlyGame.Core.Components
{
    public class DGComponent : DGGameObject
    {
        protected DGEntity DGEntityInstance { get; private set; }

        public DGComponent(DGGame game, DGEntity entity) : base(game)
        {
            this.DGEntityInstance = entity;
        }

        public override void Start()
        {
            return;
        }

        public override void Update()
        {
            return;
        }
    }
}
