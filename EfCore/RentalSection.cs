using EfCore;

namespace EfCore
{
    public class RentalSection : BaseSection
    {
        public Rail Rail { get; }
        public bool InArrears { get; set; }	// simplified payment mode
        public int PaymentFrequency { get; set; }

        protected RentalSection() { }  // EF Core requirement

        public RentalSection(OverrideRule overrideRule, bool useParent = true) : base(overrideRule, useParent)
        {
        }
    }
}

public class Rail : BaseEntity
{
    public string Script { get; set; }

    public Rail() {}

    public Rail(string script)
    {
        Script = script;
    }
}