using EfCore;

namespace EfCore
{
    public class RentalSection : BaseSection
    {
        public virtual Rail Rail { get; set; }
        public bool InArrears { get; set; }	// simplified payment mode
        public int PaymentFrequency { get; set; }

        protected RentalSection() {}

        public RentalSection(Rail rail)
        {
            Rail = rail;
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