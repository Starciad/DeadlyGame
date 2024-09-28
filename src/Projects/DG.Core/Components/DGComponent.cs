using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Objects;

namespace DeadlyGame.Core.Components
{
    public class DGComponent : DGObject
    {
        protected DGEntity DGEntityInstance { get; private set; }

        public DGComponent(DGGame game, DGEntity entity) : base(game)
        {
            this.DGEntityInstance = entity;
        }
    }
}
