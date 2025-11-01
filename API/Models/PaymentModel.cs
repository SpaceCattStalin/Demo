using Repositories.Entities;

namespace API.Models
{
    public class PaymentModel
    {
        public int PaymentId { get; set; }

        public decimal Amount { get; set; }

        public string Method { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime CreatedDate { get; set; }
    }
}
