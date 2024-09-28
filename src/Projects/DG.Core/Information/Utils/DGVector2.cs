using System.Numerics;

namespace DG.Core.Information.Utils
{
    public struct DGVector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public DGVector2()
        {
            this.X = 0;
            this.Y = 0;
        }

        public DGVector2(Vector2 vector2)
        {
            this.X = vector2.X;
            this.Y = vector2.Y;
        }
    }
}
