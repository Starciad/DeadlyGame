using DeadlyGame.Core.Components;
using DeadlyGame.Core.Objects;

namespace DeadlyGame.Core.Entities
{
    public abstract class DGEntity : DGGameObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public DGComponentContainer ComponentContainer => this.componentContainer;

        private readonly DGComponentContainer componentContainer;

        public DGEntity(DGGame game) : base(game)
        {
            this.componentContainer = new(game, this);
        }

        public override void Update()
        {
            this.componentContainer.Update();
        }
    }
}
