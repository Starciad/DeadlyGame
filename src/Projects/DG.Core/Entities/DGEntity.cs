using DeadlyGame.Core.Components;
using DeadlyGame.Core.Objects;

namespace DeadlyGame.Core.Entities
{
    public abstract class DGEntity : DGObject
    {
        internal string Name { get; set; }
        internal int Id { get; set; }
        internal DGComponentContainer ComponentContainer => this.componentContainer;

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
