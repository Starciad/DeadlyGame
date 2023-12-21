using DG.Core.Components;
using DG.Core.Objects;

namespace DG.Core.Entities
{
    public abstract class DGEntity : DGObject
    {
        public string Name { get; set; }
        public DGComponentContainer ComponentContainer => this.componentContainer;

        private readonly DGComponentContainer componentContainer = new();

        internal override void SetGameInstance(DGGame game)
        {
            this.componentContainer.SetGameInstance(game);
            this.componentContainer.SetEntityInstance(this);
            base.SetGameInstance(game);
        }
    }
}
