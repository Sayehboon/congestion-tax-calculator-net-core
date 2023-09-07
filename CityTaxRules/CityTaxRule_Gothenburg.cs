using congestion.calculator.TaxRules;
using congestion.calculator.TaxRules.PostProcessRules;

namespace congestion.calculator.CityTaxRules
{
    public class CityTaxRule_Gothenburg : CityTaxRule
    {
        public CityTaxRule_Gothenburg()
        {
            FreeDateRules.AddDayOfWeek(System.DayOfWeek.Saturday);
            FreeDateRules.AddDayOfWeek(System.DayOfWeek.Sunday);

            FreeDateRules.AddTollFreeHoliday(2013, 1, 1);
            FreeDateRules.AddTollFreeHoliday(2013, 3, 28, 29);
            FreeDateRules.AddTollFreeHoliday(2013, 4, 1, 30);
            FreeDateRules.AddTollFreeHoliday(2013, 5, 1, 8, 9);
            FreeDateRules.AddTollFreeHoliday(2013, 6, 5, 6, 21);
            FreeDateRules.AddTollFreeMonth(2013, 7);
            FreeDateRules.AddTollFreeHoliday(2013, 11, 1);
            FreeDateRules.AddTollFreeHoliday(2013, 12, 24, 25, 26, 31);

            FreeVehicles.Add(TollFreeVehicleType.Motorcycle);
            FreeVehicles.Add(TollFreeVehicleType.Tractor);
            FreeVehicles.Add(TollFreeVehicleType.Emergency);
            FreeVehicles.Add(TollFreeVehicleType.Diplomat);
            FreeVehicles.Add(TollFreeVehicleType.Foreign);
            FreeVehicles.Add(TollFreeVehicleType.Military);

            TollFeeRules.Add("06:00", "06:29", 8);
            TollFeeRules.Add("06:30", "06:59", 13);
            TollFeeRules.Add("07:00", "07:59", 18);
            TollFeeRules.Add("08:00", "08:29", 13);
            TollFeeRules.Add("08:30", "14:59", 8);
            TollFeeRules.Add("15:00", "15:29", 13);
            TollFeeRules.Add("15:30", "16:59", 18);
            TollFeeRules.Add("17:00", "17:59", 13);
            TollFeeRules.Add("18:00", "18:29", 8);
            TollFeeRules.Add("18:30", "05:59", 0);

            PostProcessRules.Add(new PostProcessRule_ReducingDuplicateCarTolls(minutes: 60));
            PostProcessRules.Add(new PostProcessRule_MaxFeeForEachDate(maximumFee: 60));
        }

    }
}
