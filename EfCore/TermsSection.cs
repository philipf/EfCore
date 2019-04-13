﻿using Microsoft.EntityFrameworkCore;

namespace EfCore
{
    public class TermsSection : BaseSection
    {
        public Rail Rail { get; set; }
        public int MinTerms { get; set; }
        public int MaxTerms { get; set; }

        public Address Postal { get; set; }
        public Address Physical { get; set; }

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