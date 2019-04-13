using Microsoft.EntityFrameworkCore;

namespace EfCore
{
    public class TermsSection : BaseSection
    {
        public virtual Rail Rail { get; set; }
        public int MinTerms { get; set; }
        public int MaxTerms { get; set; }

        public virtual Address Postal { get; set; }
        public virtual Address Physical { get; set; }

        public TermsSection()
        {
            Postal = new Address();
            Physical = new Address();

        }
    }
}

[Owned]
public class Address
{
    public string Line1{ get; set; }
    public string Line2{ get; set; }
}