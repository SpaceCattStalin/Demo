using Repositories.Entities;

namespace API.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string Type { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime? ProcessedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
