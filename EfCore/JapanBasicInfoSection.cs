namespace EfCore
{
    public class JapanBasicInfoSection : BasicInfoSection
    {
        public string KatakanaName { get; set; }

        protected JapanBasicInfoSection() { }  // EF Core requirement

        public JapanBasicInfoSection(OverrideRule overrideRule, bool useParent = true) : base(overrideRule, useParent)
        {
        }
    }
}