namespace congestion.calculator.TaxRules.PostProcessRules
{
    public class PostProcessRule_MaxFeeForEachDate : PostProcessRuleBase
    {
        public PostProcessRule_MaxFeeForEachDate(int maximumFee)
        {
            MaximumFee = maximumFee;
        }
        public int MaximumFee { get; set; }

        public override void Process(TaxContext context)
        {
            foreach (var currentDay in context.Items)
            {
                if (currentDay.TotalFee > MaximumFee)
                {
                    currentDay.Items.Clear();
                    currentDay.Items.Add(new TaxResult_DateAndTime(currentDay.Date, MaximumFee));
                }
            }
        }
    }
}
