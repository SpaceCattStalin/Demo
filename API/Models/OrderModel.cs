using Repositories.Entities;
using Services;
using Services.DTOs;
using Services.Utils;

namespace API.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }

        public decimal Total { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        //public int UserId { get; set; }

        public ICollection<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();

        public virtual PaymentModel? Payment { get; set; }

        public virtual ShippingModel? Shipping { get; set; }
    }
    public class CheckoutRequest
    {
        public int PaymentMethodId { get; set; } = 1;
        public string EndAddress { get; set; }
        public string? DiscountCode { get; set; }
    }

    public class OrderItemDTO
    {
        public string Name { get; set; }
        public string SizeType { get; set; }
        public int Quantity { get; set; }

        //public ProductVariantModel ProductVariant { get; set; }
    }
    //public class UpdateOrderStatusRequest
    //{
    //    public OrderStatusEnum Status { get; set; }
}
