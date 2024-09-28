using System.Numerics;

namespace DeadlyGame.Core.Components.Common
{
    internal sealed class DGTransformComponent : DGComponent
    {
        internal Vector2 Position { get; private set; }

        internal void SetPosition(Vector2 position)
        {
            this.Position = position;
        }

        internal void Move(Vector2 value)
        {
            this.Position += value;
        }
    }
}
