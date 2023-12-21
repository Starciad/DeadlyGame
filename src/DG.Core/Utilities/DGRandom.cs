using System;

namespace DG.Core.Utilities
{
    internal sealed class DGRandom
    {
        private readonly Random _random = new();

        internal DGRandom()
        {
            this._random = new();
        }
        internal DGRandom(int seed)
        {
            this._random = new(seed);
        }
        internal DGRandom(Random random)
        {
            this._random = random;
        }

        internal byte Range(byte minimum, byte maximum)
        {
            return (byte)this._random.Next(minimum, maximum);
        }
        internal int Range(int minimum, int maximum)
        {
            return this._random.Next(minimum, maximum);
        }
        internal int Range(float minimum, float maximum)
        {
            return this._random.Next((int)minimum, (int)maximum);
        }
    }
}
