using DG.Core.Components.Common;

using System.Numerics;

namespace DG.Core.Information.Components
{
    public struct DGTransformInfo
    {
        public Vector2 Position { get; set; }

        public DGTransformInfo()
        {
            this.Position = Vector2.Zero;
        }

        internal static DGTransformInfo Create(DGTransformComponent component)
        {
            return new()
            {
                Position = component.Position
            };
        }
    }
}
