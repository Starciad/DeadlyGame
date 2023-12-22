using System;

namespace DG.Core.Utilities
{
    public static class DGPercentageUtilities
    {
        /// <summary>
        /// Calculates the percentage of a value in relation to a total.
        /// </summary>
        /// <param name="value">Value for which the percentage is to be calculated.</param>
        /// <param name="total">Total against which the percentage will be calculated.</param>
        /// <returns>The calculated percentage.</returns>
        /// <exception cref="ArgumentException" />
        public static double CalculatePercentage(double value, double total)
        {
            if (total == 0)
            {
                throw new ArgumentException("Total cannot be zero.");
            }

            return (value / total) * 100;
        }

        /// <summary>
        /// Calculates the value corresponding to a given percentage of a total.
        /// </summary>
        /// <param name="percentage">Desired percentage.</param>
        /// <param name="total">Total on which the percentage will be calculated.</param>
        /// <returns>The value corresponding to the percentage.</returns>
        /// <exception cref="ArgumentException" />
        public static double CalculateValueFromPercentage(double percentage, double total)
        {
            if (total == 0)
            {
                throw new ArgumentException("Total cannot be zero.");
            }

            if (percentage < 0 || percentage > 100)
            {
                throw new ArgumentException("The percentage value must be between 0 and 100.");
            }

            return (percentage / 100) * total;
        }
    }
}
