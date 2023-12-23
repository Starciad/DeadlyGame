using DG.Core.Components.Common;

using System.Numerics;

namespace DG.Core.Information.Components
{
    public struct DGTransformInfo
    {
        public Vector2 Position { get; set; }

        internal static DGTransformInfo Create(DGTransformComponent component)
        {
            return new()
            {
                Position = component.Position
            };
        }
    }
}
