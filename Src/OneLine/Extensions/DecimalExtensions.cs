using System;
namespace OneLine.Extensions
{
    public static class DecimalExtensions
    {
        /// <summary>
        /// Converts a decimal value to it's currency
        /// </summary>
        /// <param name="input">The decimal value</param>
        /// <returns></returns>
        public static string ToCurrency(this decimal? input)
        {
            if (!input.HasValue)
            {
                return string.Empty;
            }
            return string.Format("{0:C}", input.Value);
        }
        /// <summary>
        /// Converts a decimal value to it's currency
        /// </summary>
        /// <param name="input">The decimal value</param>
        /// <returns></returns>
        public static string ToCurrencyRounded(this decimal input, int numberOfDecimals = 2, MidpointRounding midpointRounding = MidpointRounding.AwayFromZero)
        {
            return string.Format("{0:C}", decimal.Round(input, numberOfDecimals, midpointRounding));
        }
        /// <summary>
        /// Converts a decimal value to it's currency
        /// </summary>
        /// <param name="input">The decimal value</param>
        /// <returns></returns>
        public static string ToCurrencyRounded(this decimal? input, int numberOfDecimals = 2, MidpointRounding midpointRounding = MidpointRounding.AwayFromZero)
        {
            if (!input.HasValue)
            {
                return string.Empty;
            }
            return string.Format("{0:C}", decimal.Round(input.Value, numberOfDecimals, midpointRounding));
        }
    }
}

