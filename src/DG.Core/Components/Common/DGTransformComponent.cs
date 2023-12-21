using System.Numerics;

namespace DG.Core.Components.Common
{
    internal sealed class DGTransformComponent : DGComponent
    {
        internal Vector2 Position { get; private set; }

        internal void SetPosition(Vector2 position)
        {
            this.Position = position;
        }
    }
}
