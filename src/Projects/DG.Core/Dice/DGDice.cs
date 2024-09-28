using System;
using System.Linq;

namespace DeadlyGame.Core.Dice
{
    internal sealed class DGDice
    {
        private readonly Random _random;

        internal DGDice()
        {
            this._random = new();
        }
        internal DGDice(int seed)
        {
            this._random = new(seed);
        }
        internal DGDice(Random random)
        {
            this._random = random;
        }

        internal int Roll(int size)
        {
            return this._random.Next(1, size + 1);
        }

        internal int[] Roll(int rolls, int size)
        {
            int[] valueOfRolls = new int[rolls];
            for (int i = 0; i < rolls; i++)
            {
                valueOfRolls[i] = this._random.Next(1, size + 1);
            }

            return valueOfRolls;
        }

        internal int RollAndGetHighestValue(int rolls, int size)
        {
            return Roll(rolls, size).Max();
        }

        internal int RollAndGetLowestValue(int rolls, int size)
        {
            return Roll(rolls, size).Max();
        }

        internal int RollAndGetTotalSum(int rolls, int size)
        {
            return Roll(rolls, size).Sum();
        }
    }
}
