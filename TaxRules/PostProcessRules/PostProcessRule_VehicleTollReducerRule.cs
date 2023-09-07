using System.Linq;

namespace congestion.calculator.TaxRules.PostProcessRules
{
    public class PostProcessRule_ReducingDuplicateCarTolls : PostProcessRuleBase
    {
        public PostProcessRule_ReducingDuplicateCarTolls(int minutes)
        {
            Minutes = minutes;
        }
        public int Minutes { get; set; }

        public override void Process(TaxContext context)
        {
            foreach (var currentDay in context.Items)
            {
                TaxResult_DateAndTime Previous = null;
                foreach (var taxItem in currentDay.Items.ToArray())
                {
                    if (Previous == null)
                    {
                        Previous = taxItem;
                        continue;
                    }

                    var Diff = (taxItem.TaxDate - Previous.TaxDate).TotalMinutes;
                    if (Diff > Minutes)
                    {
                        Previous = taxItem;
                        continue;
                    }

                    if (taxItem.Price > Previous.Price)
                    {
                        currentDay.Remove(Previous);
                        Previous = taxItem;
                    }
                    else
                    {
                        currentDay.Remove(taxItem);
                    }

                }
            }
        }

    }
}
