using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Objects;

namespace DeadlyGame.Core.Components
{
    public class DGComponent : DGObject
    {
        protected DGEntity Entity { get; private set; }

        public void SetEntityInstance(DGEntity entity)
        {
            this.Entity = entity;
        }
    }
}
