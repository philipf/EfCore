namespace EfCore
{
    public class BasicInfoSection : BaseSection
    {
        public string Code { get; set; }
        public string Description { get; set; }

        protected BasicInfoSection() { }  // EF Core requirement

        public BasicInfoSection(OverrideRule overrideRule, bool useParent = true) : base(overrideRule, useParent)
        {
        }
    }
}