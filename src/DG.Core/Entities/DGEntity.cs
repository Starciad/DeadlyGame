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
            base.SetGameInstance(game);
            this.componentContainer.SetGameInstance(game);
            this.componentContainer.SetEntityInstance(this);
        }

        protected override void OnStart()
        {
            this.componentContainer.Initialize();
        }
        protected override void OnUpdate()
        {
            this.componentContainer.Update();
        }
    }
}
