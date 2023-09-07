using System;
using System.Linq;
using congestion.calculator.CityTaxRules;
using congestion.calculator.TaxRules;
using congestion.calculator.Test;
using congestion.calculator.Vehicles;

public class CongestionTaxCalculator
{

    public void Test()
    {

        var Result = GetTax(new Car(), TestData.Dates.ToArray(), TaxRuleManager.Gothenburg);
    }

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total congestion tax for that day
     */
    public int GetTax(Vehicle vehicle, DateTime[] dates, CityTaxRule cityTaxRule)
    {
        return cityTaxRule.GetTax(vehicle, dates);

    }

}