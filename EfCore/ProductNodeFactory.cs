namespace EfCore
{
    public static class ProductNodeFactory
    {
        public static ProductNode Create(ProductLevel level)
        {
            var n = new ProductNode();
            n.Level = level;

            if (level.Code == "PT")
            {
                var bs = new BasicInfoSection();
                bs.OverrideRule = OverrideRule.Top;
                bs.UseParent = false;
                n.Sections.Add(bs);

                var ts = new TermsSection();
                ts.OverrideRule = OverrideRule.Top;
                ts.UseParent = false;

                n.Sections.Add(ts);

            }

            if (level.Code == "P")
            {
                var bs = new BasicInfoSection();
                bs.OverrideRule = OverrideRule.AlwaysInherits;
                bs.UseParent = false;
                n.Sections.Add(bs);

                var ts = new TermsSection();
                ts.OverrideRule = OverrideRule.UserChooses;
                ts.UseParent = true;

                n.Sections.Add(ts);

                var rs  = new RentalSection(new Rail("return 123;"));
                n.Sections.Add(rs);
            }

            return n;
        }
    }
}