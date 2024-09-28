using DeadlyGame.Core.Components;
using DeadlyGame.Core.Mathematics.Primitives;

using System.Numerics;

namespace DeadlyGame.Core.GameContent.Components
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
