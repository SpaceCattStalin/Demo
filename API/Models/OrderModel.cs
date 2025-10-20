using Repositories.Entities;

namespace API.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public decimal Total { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public int UsersId { get; set; }

        public virtual PaymentModel? Payment { get; set; }

        public virtual ShippingModel? Shipping { get; set; }
    }
}
