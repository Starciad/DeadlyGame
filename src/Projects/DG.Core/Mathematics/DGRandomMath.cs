using System;

namespace DeadlyGame.Core.Mathematics
{
    public sealed class DGRandomMath
    {
        private readonly Random _random;

        public DGRandomMath()
        {
            this._random = new();
        }
        public DGRandomMath(int seed)
        {
            this._random = new(seed);
        }
        public DGRandomMath(Random random)
        {
            this._random = random;
        }

        public byte Range(byte minimum, byte maximum)
        {
            return (byte)this._random.Next(minimum, maximum);
        }
        public int Range(int minimum, int maximum)
        {
            return this._random.Next(minimum, maximum);
        }
        public int Range(float minimum, float maximum)
        {
            return this._random.Next((int)minimum, (int)maximum);
        }

        public bool Chance(int chance, int total)
        {
            return Range(0, total) < chance;
        }
    }
}
