using System;

namespace OneLine.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToCurrency(this decimal? input)
        {
            if (!input.HasValue)
            {
                return string.Empty;
            }

            return string.Format("{0:C}", input.Value);
        }

        public static string ToCurrencyRounded(this decimal input, int numberOfDecimals = 2)
        {
            return string.Format("{0:C}", decimal.Round(input, numberOfDecimals, MidpointRounding.AwayFromZero));
        }

        public static string ToCurrencyRounded(this decimal? input, int numberOfDecimals = 2)
        {
            if (!input.HasValue)
            {
                return string.Empty;
            }

            return string.Format("{0:C}", decimal.Round(input.Value, numberOfDecimals, MidpointRounding.AwayFromZero));
        }
    }
}

