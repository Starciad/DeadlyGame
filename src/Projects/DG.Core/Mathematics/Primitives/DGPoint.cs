using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DeadlyGame.Core.Mathematics.Primitives
{
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct DGPoint : IEquatable<DGPoint>
    {
        public static DGPoint Origin => new();

        public int X;
        public int Y;

        internal readonly string DebugDisplayString => string.Concat(
                    this.X.ToString(), "  ",
                    this.Y.ToString()
                );

        public DGPoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public DGPoint(int value)
        {
            this.X = value;
            this.Y = value;
        }

        public DGPoint(DGPoint point)
        {
            this.X = point.X;
            this.Y = point.Y;
        }

        public static DGPoint operator +(DGPoint value1, DGPoint value2)
        {
            return new DGPoint(value1.X + value2.X, value1.Y + value2.Y);
        }

        public static DGPoint operator -(DGPoint value1, DGPoint value2)
        {
            return new DGPoint(value1.X - value2.X, value1.Y - value2.Y);
        }

        public static DGPoint operator *(DGPoint value1, DGPoint value2)
        {
            return new DGPoint(value1.X * value2.X, value1.Y * value2.Y);
        }

        public static DGPoint operator /(DGPoint source, DGPoint divisor)
        {
            return new DGPoint(source.X / divisor.X, source.Y / divisor.Y);
        }

        public static bool operator ==(DGPoint a, DGPoint b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(DGPoint a, DGPoint b)
        {
            return !a.Equals(b);
        }

        public override readonly bool Equals(object obj)
        {
            return obj is DGPoint point && Equals(point);
        }

        public readonly bool Equals(DGPoint other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(this.X, this.Y);
        }

        public override readonly string ToString()
        {
            return "{X:" + this.X + " Y:" + this.Y + "}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly DGPoint ToVector2()
        {
            return new DGPoint(this.X, this.Y);
        }

        public readonly void Deconstruct(out int x, out int y)
        {
            x = this.X;
            y = this.Y;
        }

        internal static double Distance(DGPoint point1, DGPoint point2)
        {
            return Math.Round(Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2)));
        }
    }
}
