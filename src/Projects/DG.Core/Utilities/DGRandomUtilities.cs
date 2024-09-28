using System;

namespace DeadlyGame.Core.Utilities
{
    internal sealed class DGRandomUtilities
    {
        private readonly Random _random;

        internal DGRandomUtilities()
        {
            this._random = new();
        }
        internal DGRandomUtilities(int seed)
        {
            this._random = new(seed);
        }
        internal DGRandomUtilities(Random random)
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

        public bool Chance(int chance, int total)
        {
            return Range(0, total) < chance;
        }
    }
}
