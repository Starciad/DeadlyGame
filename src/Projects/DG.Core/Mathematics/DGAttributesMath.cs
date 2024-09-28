using DeadlyGame.Core.Dice;

using System;

namespace DeadlyGame.Core.Mathematics
{
    public static class DGAttributesMath
    {
        public static int GetAttributeModifier(int attributeValue)
        {
            return (int)Math.Floor((attributeValue - 10) / 2f);
        }

        public static int GetAttributeTestValue(DGDice dice, int attributeValue)
        {
            return dice.Roll(attributeValue) + GetAttributeModifier(attributeValue);
        }

        public static bool IsMaxAttributeValueInTest(int attributeValue, int currentValue)
        {
            return currentValue + GetAttributeModifier(attributeValue) == attributeValue;
        }
    }
}