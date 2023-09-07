using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace congestion.calculator.TaxRules
{
    public class TollFeeRule
    {
        public TollFeeRule(TimeSpan startTime, TimeSpan endTime, int price)
        {
            StartTime = startTime;
            EndTime = endTime;
            Price = price;
        }

        public TimeSpan StartTime { get; }
        public TimeSpan EndTime { get; }
        public int Price { get; }

        public bool IsMatch(TimeSpan timeOfDay)
        {
            return (timeOfDay >= StartTime) && (timeOfDay <= EndTime);
        }

    }

    public class TollFeeRuleCollection : List<TollFeeRule>
    {

        public TollFeeRule GetTollFee(DateTime date)
        {
            var time = date.TimeOfDay;
            return this.FirstOrDefault(t => t.IsMatch(time));
        }

        public void Add(string start, string end, int price)
        {
            if (string.IsNullOrEmpty(start))
            {
                throw new ArgumentException($"'{nameof(start)}' cannot be null or empty.", nameof(start));
            }

            if (string.IsNullOrEmpty(end))
            {
                throw new ArgumentException($"'{nameof(end)}' cannot be null or empty.", nameof(end));
            }

            Add(new TollFeeRule(TimeSpan.Parse(start), TimeSpan.Parse(end), price));
        }
    }
}
