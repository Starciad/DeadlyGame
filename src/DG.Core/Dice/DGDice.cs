using System;
using System.Linq;

namespace DG.Core.Dice
{
    internal static class DGDice
    {
        internal static int Roll(int size)
        {
            return Random.Shared.Next(1, size + 1);
        }

        internal static int[] Roll(int rolls, int size)
        {
            int[] valueOfRolls = new int[rolls];
            for (int i = 0; i < rolls; i++)
            {
                valueOfRolls[i] = Random.Shared.Next(1, size + 1);
            }

            return valueOfRolls;
        }

        internal static int RollAndGetHighestValue(int rolls, int size)
        {
            return Roll(rolls, size).Max();
        }

        internal static int RollAndGetLowestValue(int rolls, int size)
        {
            return Roll(rolls, size).Max();
        }

        internal static int RollAndGetTotalSum(int rolls, int size)
        {
            return Roll(rolls, size).Sum();
        }
    }
}
