using DeadlyGame.Core.Components;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Mathematics.Primitives;

namespace DeadlyGame.Core.GameContent.Components
{
    public sealed class DGTransformComponent : DGComponent
    {
        public DGPoint Position { get; private set; }

        public DGTransformComponent(DGGame game, DGEntity entity) : base(game, entity)
        {

        }

        public void SetPosition(DGPoint position)
        {
            this.Position = position;
        }

        public void Move(DGPoint value)
        {
            this.Position += value;
        }
    }
}
