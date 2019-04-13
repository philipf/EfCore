namespace EfCore
{
    public class BonusPaymentSection : BaseSection
    {
        public bool IsAllowed { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
    }
}