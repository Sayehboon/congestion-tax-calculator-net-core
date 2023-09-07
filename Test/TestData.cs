using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace congestion.calculator.Test
{
    public class TestData
    {
        static TestData()
        {
            AddDate("2013-01-14 21:00:00");

            AddDate("2013-01-15 21:00:00");

            AddDate("2013-02-07 06:23:27");
            AddDate("2013-02-07 15:27:00");

            AddDate("2013-02-08 06:27:00");
            AddDate("2013-02-08 06:20:27");
            AddDate("2013-02-08 14:35:00");
            AddDate("2013-02-08 15:29:00");
            AddDate("2013-02-08 15:47:00");
            AddDate("2013-02-08 16:01:00");
            AddDate("2013-02-08 16:48:00");
            AddDate("2013-02-08 17:49:00");
            AddDate("2013-02-08 18:29:00");
            AddDate("2013-02-08 18:35:00");

            AddDate("2013-03-26 14:25:00");
            AddDate("2013-03-28 14:07:27");
        }

        private static void AddDate(string dateTime)
        {
            Dates.Add(DateTime.Parse(dateTime));
        }

        public static List<DateTime> Dates { get; } = new List<DateTime>();

    }
}
