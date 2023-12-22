using System.Numerics;

namespace DG.Core.Components.Common
{
    public sealed class DGTransformComponent : DGComponent
    {
        public Vector2 Position { get; private set; }

        public void SetPosition(Vector2 position)
        {
            this.Position = position;
        }

        public void Move(Vector2 value)
        {
            this.Position += value;
        }
    }
}
