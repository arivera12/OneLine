using System;

namespace OneLine.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Inclusive compares a date time range with the current date time and returns true if the date is between the date time range
        /// </summary>
        /// <param name="input">The source date</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <param name="compareTime">Whether to compare the entire date time or only the date part only</param>
        /// <returns></returns>
        public static bool IsBetween(this DateTime input, DateTime startDate, DateTime endDate, bool compareTime = false)
        {
            return compareTime ? input >= startDate && input <= endDate : input.Date >= startDate.Date && input.Date <= endDate.Date;
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> equals <see cref="DayOfWeek.Sunday"/> or <see cref="DayOfWeek.Saturday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsWeekend(this DateTime value)
        {
            return value.DayOfWeek.IsWeekend();
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> equals <see cref="DayOfWeek.Saturday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsFirstDayOfWeekend(this DateTime value)
        {
            return value.DayOfWeek.IsFirstDayOfWeekend();
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> equals <see cref="DayOfWeek.Sunday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsLastDayOfWeekend(this DateTime value)
        {
            return value.DayOfWeek.IsLastDayOfWeekend();
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is not equal to <see cref="DayOfWeek.Sunday"/> and <see cref="DayOfWeek.Saturday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsWeekday(this DateTime value)
        {
            return value.DayOfWeek.IsWeekday();
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Monday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsFirstDayOfWeek(this DateTime value)
        {
            return value.DayOfWeek.IsFirstDayOfWeek();
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Friday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsLastDayOfWeek(this DateTime value)
        {
            return value.DayOfWeek.IsLastDayOfWeek();
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Sunday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsSunday(this DateTime value)
        {
            return value.DayOfWeek.IsSunday();
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Monday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsMonday(this DateTime value)
        {
            return value.DayOfWeek.IsMonday();
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Tuesday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsTuesday(this DateTime value)
        {
            return value.DayOfWeek.IsTuesday();
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Wednesday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsWednesday(this DateTime value)
        {
            return value.DayOfWeek.IsWednesday();
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Thursday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsThursday(this DateTime value)
        {
            return value.DayOfWeek.IsThursday();
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Friday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsFriday(this DateTime value)
        {
            return value.DayOfWeek.IsFriday();
        }
        /// <summary>
        /// Check whether the <see cref="DayOfWeek"/> is equal to <see cref="DayOfWeek.Saturday"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsSaturday(this DateTime value)
        {
            return value.DayOfWeek.IsSaturday();
        }
        /// <summary>
        /// Returns the <see cref="DateTime"/> to end of the day by the date and time
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime date)
        {
            return date.Date.AddDays(1).AddMilliseconds(-1);
        }
        /// <summary>
        /// Returns the <see cref="DateTime"/> to the begginning of the day by the date and time 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime BeginningOfDay(this DateTime date)
        {
            return date.Date;
        }
        /// <summary>
        /// Returns the end of the month of the <see cref="DateTime"/>
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime EndOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999);
        }
        /// <summary>
        /// Check whether the <see cref="DateTime"/> date part is the end of the month
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsEndOfMonth(this DateTime date)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            var EndOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59, 999);
            return EndOfMonth.ToShortDateString() == date.ToShortDateString();
        }
        /// <summary>
        /// Returns the beginning of the month of the <see cref="DateTime"/>
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime BeginningOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0, 0);
        }
        /// <summary>
        /// Check whether the <see cref="DateTime"/> date part is the beginning of the month
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsBeginningOfMonth(this DateTime date)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            var BegnningOfMonth = new DateTime(year, month, 1, 0, 0, 0, 0);
            return BegnningOfMonth.ToShortDateString() == date.ToShortDateString();
        }
        /// <summary>
        /// Returns the beginning of the year of the <see cref="DateTime"/>
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime BeginningOfYear(this DateTime date)
        {
            int year = DateTime.Now.Year;
            return new DateTime(year, 1, 1);
        }
        /// <summary>
        /// Check whether the <see cref="DateTime"/> date part is the beginning of the year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsBeginningOfYear(this DateTime date)
        {
            int year = DateTime.Now.Year;
            DateTime firstDay = new DateTime(year, 1, 1);
            return firstDay.ToShortDateString() == date.ToShortDateString();
        }
        /// <summary>
        /// Returns the end of the year of the <see cref="DateTime"/>
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime EndOfYear(this DateTime date)
        {
            int year = DateTime.Now.Year;
            return new DateTime(year, 12, 31);
        }
        /// <summary>
        /// Check whether the <see cref="DateTime"/> date part is the end of the year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsEndOfYear(this DateTime date)
        {
            int year = DateTime.Now.Year;
            DateTime lastDay = new DateTime(year, 12, 31);
            return lastDay.ToShortDateString() == date.ToShortDateString();
        }
        /// <summary>
        /// Returns the break fast time
        /// </summary>
        /// <param name="date"></param>
        /// <param name="hour">Default value 7</param>
        /// <returns></returns>
        public static DateTime BreakfastTime(this DateTime date, int hour = 7)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, 00, 0, 0);
        }
        /// <summary>
        /// Return the lunch time
        /// </summary>
        /// <param name="date"></param>
        /// <param name="hour">Default value 12</param>
        /// <returns></returns>
        public static DateTime LunchTime(this DateTime date, int hour = 12)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, 00, 0, 0);
        }
        /// <summary>
        /// Returns the dinner time
        /// </summary>
        /// <param name="date"></param>
        /// <param name="hour">Default value 5</param>
        /// <returns></returns>
        public static DateTime DinnerTime(this DateTime date, int hour = 5)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, 00, 0, 0);
        }
        /// <summary>
        /// Checks if it's break fast time
        /// </summary>
        /// <param name="date"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        public static bool IsBreakfastTime(this DateTime date, int hour = 7)
        {
            return date < BreakfastTime(date, hour);
        }
        /// <summary>
        /// Checks if it's lunch time if it's between breakfast time and dinner time
        /// </summary>
        /// <param name="date"></param>
        /// <param name="breakfastHour"></param>
        /// <param name="dinnerHour"></param>
        /// <returns></returns>
        public static bool IsLunchTime(this DateTime date, int breakfastHour = 7, int dinnerHour = 5)
        {
            return date > BreakfastTime(date, breakfastHour) && date < DinnerTime(date, dinnerHour);
        }
        /// <summary>
        /// Check if it's dinner time
        /// </summary>
        /// <param name="date"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        public static bool IsDinnerTime(this DateTime date, int hour = 5)
        {
            return date > DinnerTime(date, hour);
        }
    }
}

