using DG.Core.Entities;
using DG.Core.Objects;

namespace DG.Core.Components
{
    internal class DGComponent : DGObject
    {
        protected DGEntity Entity { get; private set; }

        internal void SetEntityInstance(DGEntity entity)
        {
            this.Entity = entity;
        }
    }
}
