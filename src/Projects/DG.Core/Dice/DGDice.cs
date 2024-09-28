using System;
using System.Linq;

namespace DeadlyGame.Core.Dice
{
    public sealed class DGDice
    {
        private readonly Random _random;

        public DGDice()
        {
            this._random = new();
        }
        public DGDice(int seed)
        {
            this._random = new(seed);
        }
        public DGDice(Random random)
        {
            this._random = random;
        }

        public int Roll(int size)
        {
            return this._random.Next(1, size + 1);
        }

        public int[] Roll(int rolls, int size)
        {
            int[] valueOfRolls = new int[rolls];
            for (int i = 0; i < rolls; i++)
            {
                valueOfRolls[i] = this._random.Next(1, size + 1);
            }

            return valueOfRolls;
        }

        public int RollAndGetHighestValue(int rolls, int size)
        {
            return Roll(rolls, size).Max();
        }

        public int RollAndGetLowestValue(int rolls, int size)
        {
            return Roll(rolls, size).Max();
        }

        public int RollAndGetTotalSum(int rolls, int size)
        {
            return Roll(rolls, size).Sum();
        }
    }
}
