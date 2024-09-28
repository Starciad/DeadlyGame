using DeadlyGame.Core.Mathematics.Primitives;

using System.Numerics;

namespace DeadlyGame.Core.Components.Common
{
    internal sealed class DGTransformComponent : DGComponent
    {
        internal DGPoint Position { get; private set; }

        internal void SetPosition(DGPoint position)
        {
            this.Position = position;
        }

        internal void Move(DGPoint value)
        {
            this.Position += value;
        }
    }
}
