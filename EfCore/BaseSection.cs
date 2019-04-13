namespace EfCore
{
    public abstract class BaseSection : BaseEntity
    {
        public OverrideRule OverrideRule { get; set; }
        public bool UseParent { get; set; }

        protected BaseSection()   
        {
            UseParent = true;
        }
    }
}