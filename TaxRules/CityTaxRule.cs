using congestion.calculator.TaxRules;
using congestion.calculator.TaxRules.PostProcessRules;
using System;
using System.Collections.Generic;
using System.Linq;
using congestion.calculator.Vehicles;

namespace congestion.calculator.TaxRules
{
    public class CityTaxRule
    {

        public TollFreeDateRuleCollection FreeDateRules { get; } = new TollFreeDateRuleCollection();

        public TollFreeVehicleCollection FreeVehicles { get; } = new TollFreeVehicleCollection();

        public TollFeeRuleCollection TollFeeRules { get; } = new TollFeeRuleCollection();

        public List<PostProcessRuleBase> PostProcessRules { get; } = new List<PostProcessRuleBase>();


        private bool IsTollFreeVehicle(Vehicle vehicle)
        {
            if (vehicle == null) return false;
            String vehicleType = vehicle.GetVehicleType();
            return FreeVehicles.IsTollFree(vehicleType);
        }

        private bool IsTollFreeDate(DateTime date)
        {
            return FreeDateRules.IsTollFreeDate(date);
        }

        public TaxResult_DateAndTime GetTollFee(DateTime date, Vehicle vehicle)
        {
            var result = new TaxResult_DateAndTime(date);
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle))
            {
                return result;
            }
            else
            {
                var Finded = TollFeeRules.GetTollFee(date);
                result.Price = Finded?.Price ?? 0;
                return result;
            }
        }

        public int GetTax(Vehicle vehicle, DateTime[] dates)
        {
            TaxContext context = new TaxContext();
            var items = dates.Select(t => GetTollFee(t, vehicle)).Where(t => t.Price > 0).OrderBy(t => t.TaxDate).ToList();
            context.Items = items.GroupBy(t => t.TaxDate.Date).Select(t => new TaxResult_Date() { Date = t.Key, Items = t.OrderBy(t => t.TaxDate).ToList() }).ToList();

            foreach (var postProcessRule in PostProcessRules)
            {
                postProcessRule.Process(context);
            }
            return context.TotalFee;
        }
    }

    public class TaxResult_DateAndTime
    {
        public TaxResult_DateAndTime(DateTime taxDate, int price = 0)
        {
            TaxDate = taxDate;
            Price = price;
        }
        public DateTime TaxDate { get; }
        public int Price { get; set; }

        public override string ToString()
        {
            return $"{TaxDate} , Total Fee = {Price}";
        }
    }

    public class TaxResult_Date
    {
        public DateTime Date { get; set; }
        public List<TaxResult_DateAndTime> Items { get; set; }

        public int TotalFee
        {
            get
            {
                return Items?.Sum(t => t.Price) ?? 0;
            }
        }

        public void Remove(TaxResult_DateAndTime item)
        {
            Items?.Remove(item);
        }

        public override string ToString()
        {
            return $"{Date} , Total Fee = {TotalFee}";
        }

    }
}


//DateTime intervalStart = dates[0];
//int totalFee = 0;
//foreach (DateTime date in dates)
//{
//    int nextFee = cityTaxRule.GetTollFee(date, vehicle);
//    int tempFee = cityTaxRule.GetTollFee(intervalStart, vehicle);

//    long diffInMillies = date.Millisecond - intervalStart.Millisecond;
//    long minutes = diffInMillies / 1000 / 60;

//    if (minutes <= 60)
//    {
//        if (totalFee > 0) totalFee -= tempFee;
//        if (nextFee >= tempFee) tempFee = nextFee;
//        totalFee += tempFee;
//    }
//    else
//    {
//        totalFee += nextFee;
//    }
//}
//if (totalFee > 60) totalFee = 60;
//return totalFee;