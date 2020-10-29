using System;

namespace OneLine.Extensions
{
    public static class DayOfWeekExtensions
    {
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> equals <see cref="DayOfWeek.Sunday"/> or <see cref="DayOfWeek.Saturday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsWeekend(this DayOfWeek value)
        {
            return value.Equals(DayOfWeek.Sunday) || value.Equals(DayOfWeek.Saturday);
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> equals <see cref="DayOfWeek.Saturday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsFirstDayOfWeekend(this DayOfWeek value)
        {
            return value.Equals(DayOfWeek.Saturday);
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> equals <see cref="DayOfWeek.Sunday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsLastDayOfWeekend(this DayOfWeek value)
        {
            return value.Equals(DayOfWeek.Sunday);
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is not equal to <see cref="DayOfWeek.Sunday"/> and <see cref="DayOfWeek.Saturday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsWeekday(this DayOfWeek value)
        {
            return !value.Equals(DayOfWeek.Sunday) && !value.Equals(DayOfWeek.Saturday);
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Monday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsFirstDayOfWeek(this DayOfWeek value)
        {
            return value.Equals(DayOfWeek.Monday);
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Friday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsLastDayOfWeek(this DayOfWeek value)
        {
            return value.Equals(DayOfWeek.Friday);
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Sunday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsSunday(this DayOfWeek value)
        {
            return value.Equals(DayOfWeek.Sunday);
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Monday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsMonday(this DayOfWeek value)
        {
            return value.Equals(DayOfWeek.Monday);
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Tuesday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsTuesday(this DayOfWeek value)
        {
            return value.Equals(DayOfWeek.Tuesday);
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Wednesday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsWednesday(this DayOfWeek value)
        {
            return value.Equals(DayOfWeek.Wednesday);
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Thursday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsThursday(this DayOfWeek value)
        {
            return value.Equals(DayOfWeek.Thursday);
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Friday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsFriday(this DayOfWeek value)
        {
            return value.Equals(DayOfWeek.Friday);
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Saturday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsSaturday(this DayOfWeek value)
        {
            return value.Equals(DayOfWeek.Saturday);
        }
    }
}
