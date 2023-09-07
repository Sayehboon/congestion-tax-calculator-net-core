using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace congestion.calculator.TaxRules
{
    public class TollFreeVehicleCollection : List<TollFreeVehicleType>
    {
        public void Add(params TollFreeVehicleType[] vehicles)
        {
            this.AddRange(vehicles);
        }

        public bool IsTollFree(string vehicleType)
        {
            return this.Any(t => t.ToString().Equals(vehicleType));
        }
    }
}
