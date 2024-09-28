using DeadlyGame.Core.Components.Common;
using DeadlyGame.Core.Information.Utils;

namespace DeadlyGame.Core.Information.Components
{
    public struct DGTransformInfo
    {
        public DGVector2 Position { get; set; }

        public DGTransformInfo()
        {
            this.Position = new();
        }

        internal static DGTransformInfo Create(DGTransformComponent component)
        {
            return new()
            {
                Position = new(component.Position)
            };
        }
    }
}
