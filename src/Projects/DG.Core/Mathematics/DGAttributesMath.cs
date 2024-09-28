using DeadlyGame.Core.Dice;

using System;

namespace DeadlyGame.Core.Mathematics
{
    internal static class DGAttributesMath
    {
        internal static int GetAttributeModifier(int attributeValue)
        {
            return (int)Math.Floor((attributeValue - 10) / 2f);
        }

        internal static int GetAttributeTestValue(DGDice dice, int attributeValue)
        {
            return dice.Roll(attributeValue) + GetAttributeModifier(attributeValue);
        }

        internal static bool IsMaxAttributeValueInTest(int attributeValue, int currentValue)
        {
            return currentValue + GetAttributeModifier(attributeValue) == attributeValue;
        }
    }
}