using System;

namespace EfCore
{
    public abstract class BaseSection : BaseEntity
    {
        public OverrideRule OverrideRule { get; }
        public bool UseParent { get; set; }

        protected BaseSection() { }  // EF Core requirement

        protected BaseSection(OverrideRule overrideRule, bool useParent = true)
        {
            OverrideRule = overrideRule;

            switch (overrideRule)
            {
                case OverrideRule.Top:
                    UseParent = false;
                    break;

                case OverrideRule.UserChooses:
                    UseParent = useParent;
                    break;

                case OverrideRule.NeverInherits:
                    UseParent = false;
                    break;

                case OverrideRule.AlwaysInherits:
                    UseParent = false;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(overrideRule), overrideRule, null);
            }
        }
    }
}