using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace congestion.calculator.TaxRules
{
    public class TaxContext
    {
        public List<TaxResult_Date> Items { get; internal set; }
        public int TotalFee => Items?.Sum(t => t.TotalFee) ?? 0;
        
    }
}
