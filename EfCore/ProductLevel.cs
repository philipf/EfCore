namespace EfCore
{
    public class ProductLevel : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
	
        public ProductLevel(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}