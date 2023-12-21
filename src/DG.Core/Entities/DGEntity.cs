using DG.Core.Components;
using DG.Core.Objects;

namespace DG.Core.Entities
{
    internal abstract class DGEntity : DGObject
    {
        internal string Name { get; set; }
        internal DGComponentContainer ComponentContainer => this.componentContainer;

        private readonly DGComponentContainer componentContainer = new();

        internal override void Build(DGGame game)
        {
            this.componentContainer.Build(game);
            base.Build(game);
        }
    }
}
