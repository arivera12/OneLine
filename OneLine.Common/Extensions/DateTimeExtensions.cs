using System;
using System.Collections.Generic;
using System.Globalization;

namespace OneLine.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsBetween(this DateTime input, DateTime startDate, DateTime endDate, bool compareTime = false)
        {
            return compareTime ? input >= startDate && input <= endDate : input.Date >= startDate.Date && input.Date <= endDate.Date;
        }

        public static bool IsWeekend(this DateTime value)
        {
            return value.DayOfWeek == DayOfWeek.Sunday || value.DayOfWeek == DayOfWeek.Saturday;
        }

        public static bool IsFirstDayOfWeekend(this DateTime value)
        {
            return value.DayOfWeek == DayOfWeek.Saturday;
        }

        public static bool IsLastDayOfWeekend(this DateTime value)
        {
            return value.DayOfWeek == DayOfWeek.Sunday;
        }

        public static bool IsWeekday(this DateTime value)
        {
            return value.DayOfWeek != DayOfWeek.Sunday && value.DayOfWeek != DayOfWeek.Saturday;
        }

        public static bool IsFirstDayOfWeek(this DateTime value)
        {
            return value.DayOfWeek == DayOfWeek.Monday;
        }

        public static bool IsLastDayOfWeek(this DateTime value)
        {
            return value.DayOfWeek == DayOfWeek.Friday;
        }

        public static bool IsSunday(this DateTime value)
        {
            return value.DayOfWeek == DayOfWeek.Sunday;
        }

        public static bool IsMonday(this DateTime value)
        {
            return value.DayOfWeek == DayOfWeek.Monday;
        }

        public static bool IsTuesday(this DateTime value)
        {
            return value.DayOfWeek == DayOfWeek.Tuesday;
        }

        public static bool IsWednesday(this DateTime value)
        {
            return value.DayOfWeek == DayOfWeek.Wednesday;
        }

        public static bool IsThursday(this DateTime value)
        {
            return value.DayOfWeek == DayOfWeek.Thursday;
        }

        public static bool IsFriday(this DateTime value)
        {
            return value.DayOfWeek == DayOfWeek.Friday;
        }

        public static bool IsSaturday(this DateTime value)
        {
            return value.DayOfWeek == DayOfWeek.Saturday;
        }
                                                                 
        public static DateTime AddWorkdays(this DateTime input, int days, List<DateTime> excludedDates = null)
        {
            excludedDates = excludedDates ?? new List<DateTime>();

            var sign = Math.Sign(days);
            var unsignedDays = Math.Abs(days);
            for (var index = 0; index < unsignedDays; index++)
            {
                do
                {
                    input = input.AddDays(sign);
                } while (input.IsWeekend() || excludedDates.Contains(input));
            }
            return input;
        }

        public static DateTime EndOfDay(this DateTime date)
        {
            return date.Date.AddDays(1).AddMilliseconds(-1);
        }

        public static DateTime BeginningOfDay(this DateTime date)
        {
            return date.Date;
        }

        public static DateTime EndOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999);
        }

        public static bool IsEndOfMonth(this DateTime date)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            var EndOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59, 999);
            return EndOfMonth.ToShortDateString() == date.ToShortDateString();
        }

        public static DateTime BeginningOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0, 0);
        }

        public static bool IsBeginningOfMonth(this DateTime date)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            var BegnningOfMonth = new DateTime(year, month, 1, 0, 0, 0, 0);
            return BegnningOfMonth.ToShortDateString() == date.ToShortDateString();
        }

        public static DateTime BeginningOfYear(this DateTime date)
        {
            int year = DateTime.Now.Year;
            return new DateTime(year, 1, 1);
        }

        public static bool IsBeginningOfYear(this DateTime date)
        {
            int year = DateTime.Now.Year;
            DateTime firstDay = new DateTime(year, 1, 1);
            return firstDay.ToShortDateString() == date.ToShortDateString();
        }

        public static DateTime EndOfYear(this DateTime date)
        {
            int year = DateTime.Now.Year;
            return new DateTime(year, 12, 31);                             
        }

        public static bool IsEndOfYear(this DateTime date)
        {
            int year = DateTime.Now.Year;
            DateTime lastDay = new DateTime(year, 12, 31);
            return lastDay.ToShortDateString() == date.ToShortDateString();
        }

        public static DateTime BreakfastTime(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 7, 00, 0, 0);
        }

        public static DateTime LunchTime(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 12, 00, 0, 0);
        }

        public static DateTime DinnerTime(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 4, 00, 0, 0);
        }

        public static bool IsBreakfastTime(this DateTime date)
        {
            return date < BreakfastTime(date);
        }

        public static bool IsLunchTime(this DateTime date)
        {
            return date > BreakfastTime(date) && date < DinnerTime(date);
        }

        public static bool IsDinnerTime(this DateTime date)
        {
            return date > DinnerTime(date);
        }

        public static DateTime BreakfastTime(this DateTime date, double AddHours)
        {
            return new DateTime(date.Year, date.Month, date.Day, 7, 00, 0, 0).AddHours(AddHours);
        }

        public static DateTime LunchTime(this DateTime date, double AddHours)
        {
            return new DateTime(date.Year, date.Month, date.Day, 12, 00, 0, 0).AddHours(AddHours);
        }

        public static DateTime DinnerTime(this DateTime date, double AddHours)
        {
            return new DateTime(date.Year, date.Month, date.Day, 4, 00, 0, 0).AddHours(AddHours);
        }

        public static bool IsBreakfastTime(this DateTime date, double AddHours)
        {
            return date < BreakfastTime(date, AddHours);
        }

        public static bool IsLunchTime(this DateTime date, double AddHours)
        {
            return date > BreakfastTime(date, AddHours) && date < DinnerTime(date, AddHours);
        }

        public static bool IsDinnerTime(this DateTime date, double AddHours)
        {
            return date > DinnerTime(date, AddHours);
        }

        public static DateTime BreakfastTime(this DateTime date, double AddHours, double AddMinutes)
        {
            return new DateTime(date.Year, date.Month, date.Day, 7, 00, 0, 0).AddHours(AddHours).AddMinutes(AddMinutes);
        }

        public static DateTime LunchTime(this DateTime date, double AddHours, double AddMinutes)
        {
            return new DateTime(date.Year, date.Month, date.Day, 12, 00, 0, 0).AddHours(AddHours).AddMinutes(AddMinutes);
        }

        public static DateTime DinnerTime(this DateTime date, double AddHours, double AddMinutes)
        {
            return new DateTime(date.Year, date.Month, date.Day, 4, 00, 0, 0).AddHours(AddHours).AddMinutes(AddMinutes);
        }

        public static bool IsBreakfastTime(this DateTime date, double AddHours, double AddMinutes)
        {
            return date < BreakfastTime(date, AddHours, AddMinutes);
        }

        public static bool IsLunchTime(this DateTime date, double AddHours, double AddMinutes)
        {
            return date > BreakfastTime(date, AddHours, AddMinutes) && date < DinnerTime(date, AddHours, AddMinutes);
        }

        public static bool IsDinnerTime(this DateTime date, double AddHours, double AddMinutes)
        {
            return date > DinnerTime(date, AddHours, AddMinutes);
        }

        public static IEnumerable<DateTime> Range(this DateTime startDate, DateTime endDate)
        {
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                yield return date;
            }
        }

        public static string ToFriendlyDateString(this DateTime date)
        {
            return date.ToString("dd MMM yyyy");
        }

        public static string ToString(this DateTime? date)
        {
            return date.ToString(null, DateTimeFormatInfo.CurrentInfo, string.Empty);
        }

        public static string ToString(this DateTime? date, string format)
        {
            return date.ToString(format, DateTimeFormatInfo.CurrentInfo, string.Empty);
        }

        public static string ToString(this DateTime? date, IFormatProvider provider)
        {
            return date.ToString(null, provider, string.Empty);
        }

        public static string ToString(this DateTime? date, string format, IFormatProvider provider, string returnIfNull)
        {
            if (date.HasValue)
            {
                return date.Value.ToString(format, provider);
            }
            else
            {
                return returnIfNull;
            }
        }

        public static string ToShortDateString(this DateTime? input)
        {
            return input.ToShortDateString(string.Empty);
        }

        public static string ToShortDateString(this DateTime? input, string returnIfNull)
        {
            if (input.HasValue)
            {
                return input.Value.ToShortDateString();
            }

            return returnIfNull;
        }
    }
}

