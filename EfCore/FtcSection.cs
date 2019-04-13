namespace EfCore
{
    public class FtcSection : BaseSection
    {
        public bool IsApplicable { get; set; }
        public decimal Cost { get; set; }
        public decimal Apr { get; set; }

        protected FtcSection() { }  // EF Core requirement

        public FtcSection(OverrideRule overrideRule, bool useParent = true) : base(overrideRule, useParent)
        {
        }
    }
}