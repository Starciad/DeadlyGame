using System;

namespace DeadlyGame.Core.Models
{
    public partial struct DGColor : IEquatable<DGColor>
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public DGColor(byte r, byte g, byte b, byte a = 255)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public DGColor(int r, int g, int b, int a = 255)
        {
            this.R = (byte)Math.Clamp(r, 0, 255);
            this.G = (byte)Math.Clamp(g, 0, 255);
            this.B = (byte)Math.Clamp(b, 0, 255);
            this.A = (byte)Math.Clamp(a, 0, 255);
        }

        public override readonly bool Equals(object obj)
        {
            return obj is DGColor color && Equals(color);
        }

        public readonly bool Equals(DGColor other)
        {
            return R == other.R && G == other.G && B == other.B && A == other.A;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(R, G, B, A);
        }

        public override readonly string ToString()
        {
            return $"RGBA({R}, {G}, {B}, {A})";
        }

        public static bool operator ==(DGColor left, DGColor right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DGColor left, DGColor right)
        {
            return !(left == right);
        }

        public static DGColor Lerp(DGColor from, DGColor to, float amount)
        {
            amount = Math.Clamp(amount, 0, 1);
            byte r = (byte)(from.R + (to.R - from.R) * amount);
            byte g = (byte)(from.G + (to.G - from.G) * amount);
            byte b = (byte)(from.B + (to.B - from.B) * amount);
            byte a = (byte)(from.A + (to.A - from.A) * amount);
            return new DGColor(r, g, b, a);
        }

        public readonly string ToHex()
        {
            return $"#{R:X2}{G:X2}{B:X2}{A:X2}";
        }

        public readonly void Deconstruct(out byte r, out byte g, out byte b, out byte a)
        {
            r = this.R;
            g = this.G;
            b = this.B;
            a = this.A;
        }
    }
}
