namespace EfCore
{
    public static class ProductNodeFactory
    {
        public static ProductNode Create(ProductLevel level)
        {
            var n = new ProductNode(level);

            if (level.Code == "PT")
            {
                var bs = new BasicInfoSection(OverrideRule.Top);
                n.AddSection(bs);

                var ts = new TermsSection(OverrideRule.Top);
                n.AddSection(ts);

            } 
            else if (level.Code == "P")
            {
                var bs = new BasicInfoSection(OverrideRule.AlwaysInherits);
                n.AddSection(bs);

                var ts = new TermsSection(OverrideRule.UserChooses, true);
                n.AddSection(ts);

                var rs  = new RentalSection(OverrideRule.NeverInherits);
                n.AddSection(rs);
            } 
            else if (level.Code == "C")
            {
                var bs = new BasicInfoSection(OverrideRule.AlwaysInherits);
                n.AddSection(bs);

                var ts = new TermsSection(OverrideRule.UserChooses, true);
                n.AddSection(ts);

                var rs  = new RentalSection(OverrideRule.NeverInherits);
                n.AddSection(rs);
            }

            return n;
        }
    }
}