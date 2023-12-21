using DG.Core.Dice;

namespace DG.Tests.Dice
{
    public sealed class DGDice_UnitTest
    {
        [Theory]
        [InlineData(6)]
        [InlineData(10)]
        [InlineData(20)]
        public void Roll_ReturnsValueBetween1AndSize(int size)
        {
            DGDice dice = new();

            int roll = dice.Roll(size);

            Assert.InRange(roll, 1, size);
        }

        [Theory]
        [InlineData(6, 5)]
        [InlineData(10, 8)]
        [InlineData(20, 15)]
        public void Roll_ReturnsArrayOfCorrectLength(int size, int rolls)
        {
            DGDice dice = new();

            int[] result = dice.Roll(rolls, size);

            Assert.Equal(rolls, result.Length);
        }

        [Theory]
        [InlineData(6, 5)]
        [InlineData(10, 8)]
        [InlineData(20, 15)]
        public void RollAndGetHighestValue_ReturnsHighestValue(int size, int rolls)
        {
            DGDice dice = new();

            int highestValue = dice.RollAndGetHighestValue(rolls, size);

            Assert.InRange(highestValue, 1, size);
        }

        [Theory]
        [InlineData(6, 5)]
        [InlineData(10, 8)]
        [InlineData(20, 15)]
        public void RollAndGetLowestValue_ReturnsLowestValue(int size, int rolls)
        {
            DGDice dice = new();

            int lowestValue = dice.RollAndGetLowestValue(rolls, size);

            Assert.InRange(lowestValue, 1, size);
        }

        [Theory]
        [InlineData(6, 5)]
        [InlineData(10, 8)]
        [InlineData(20, 15)]
        public void RollAndGetTotalSum_ReturnsSumOfAllRolls(int size, int rolls)
        {
            DGDice dice = new();

            int totalSum = dice.RollAndGetTotalSum(rolls, size);

            Assert.InRange(totalSum, rolls, rolls * size);
        }
    }
}
