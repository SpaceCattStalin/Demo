namespace Repositories.Entities
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Payment> Payments { get; set; }
    }

}
