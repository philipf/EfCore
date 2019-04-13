namespace EfCore
{
    public class ProductLevel : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
	
        protected ProductLevel() { }  // EF Core requirement

        public ProductLevel(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Code}-{Name}";
        }
    }
}