using System.Collections.Generic;

namespace congestion.calculator.TaxRules.PostProcessRules
{
    public abstract class PostProcessRuleBase
    {

        public abstract void Process(TaxContext context);
    }
}
