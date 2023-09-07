using System;
using System.Collections.Generic;
using System.Linq;

namespace congestion.calculator.TaxRules
{
    public class TollFreeDateRuleCollection : List<TollFreeDateRule>
    {
        public void AddDayOfWeek(DayOfWeek dayOfWeek)
        {
            Add(new TollFreeDate_DayOfWeek(dayOfWeek));
        }
        public void AddTollFreeHoliday(int year, int month, params int[] days)
        {
            Add(new TollFreeDate_Holiday(year, month, days));
        }
        public void AddTollFreeMonth(int year, int month)
        {
            Add(new TollFreeDate_Month(year, month));
        }
        public bool IsTollFreeDate(DateTime date)
        {
            return this.Any(t => t.IsTollFreeDate(date));
        }
    }

    public abstract class TollFreeDateRule
    {
        public abstract bool IsTollFreeDate(DateTime date);
    }

    public class TollFreeDate_DayOfWeek : TollFreeDateRule
    {
        public TollFreeDate_DayOfWeek(DayOfWeek dayOfWeek)
        {
            DayOfWeek = dayOfWeek;
        }
        public DayOfWeek DayOfWeek { get; }

        public override bool IsTollFreeDate(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek;
        }
    }

    public class TollFreeDate_Holiday : TollFreeDateRule
    {
        public TollFreeDate_Holiday(int year, int month, params int[] days)
        {
            foreach (int day in days)
            {
                var currentDate = new DateTime(year, month, day, 0, 0, 0);
                Days.Add(currentDate);
                var PreDate = currentDate.AddDays(-1);
                Days.Add(PreDate);
            }
        }

        public List<DateTime> Days { get; } = new List<DateTime>();

        public override bool IsTollFreeDate(DateTime date)
        {
            var targetDate = date.Date;
            return Days.Any(t => t.Date.Equals(targetDate));
        }
    }

    public class TollFreeDate_Month : TollFreeDateRule
    {
        public TollFreeDate_Month(int year, int month)
        {
            Year = year;
            Month = month;
        }

        public int Year { get; }
        public int Month { get; }

        public override bool IsTollFreeDate(DateTime date)
        {
            return date.Year == Year && date.Month == Month;
        }
    }
}
