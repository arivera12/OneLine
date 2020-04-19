using System;

namespace OneLine.Extensions
{
    public static class DayOfWeekExtensions
    {
        public static bool IsWeekend(this DayOfWeek value)
        {
            return value == DayOfWeek.Sunday || value == DayOfWeek.Saturday;
        }

        public static bool IsFirstDayOfWeekend(this DayOfWeek value)
        {
            return value == DayOfWeek.Saturday;
        }

        public static bool IsLastDayOfWeekend(this DayOfWeek value)
        {
            return value == DayOfWeek.Sunday;
        }

        public static bool IsWeekday(this DayOfWeek value)
        {
            return value != DayOfWeek.Sunday && value != DayOfWeek.Saturday;
        }

        public static bool IsFirstDayOfWeek(this DayOfWeek value)
        {
            return value == DayOfWeek.Monday;
        }

        public static bool IsLastDayOfWeek(this DayOfWeek value)
        {
            return value == DayOfWeek.Friday;
        }

        public static bool IsSunday(this DayOfWeek value)
        {
            return value == DayOfWeek.Sunday;
        }

        public static bool IsMonday(this DayOfWeek value)
        {
            return value == DayOfWeek.Monday;
        }

        public static bool IsTuesday(this DayOfWeek value)
        {
            return value == DayOfWeek.Tuesday;
        }

        public static bool IsWednesday(this DayOfWeek value)
        {
            return value == DayOfWeek.Wednesday;
        }

        public static bool IsThursday(this DayOfWeek value)
        {
            return value == DayOfWeek.Thursday;
        }

        public static bool IsFriday(this DayOfWeek value)
        {
            return value == DayOfWeek.Friday;
        }

        public static bool IsSaturday(this DayOfWeek value)
        {
            return value == DayOfWeek.Saturday;
        }
    }
}
