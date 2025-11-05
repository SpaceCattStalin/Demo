using Services.Utils;
using System.ComponentModel;

namespace Services.DTOs
{
    public class AdminOrderFilterRequest
    {
        public string? Keyword { get; set; }          // Tìm theo tên, email user, mã đơn
        public OrderStatusEnum? Status { get; set; }
        public PaymentStatusEnum? PaymentStatus { get; set; }
        public ShippingStatusEnum? ShippingStatus { get; set; }
        public int? UserId { get; set; }              // Lọc theo người dùng cụ thể
        public int? FromDate { get; set; }
        public int? ToDate { get; set; }
        [DefaultValue(1)]
        public int CurrentPage { get; set; } = 1;
        [DefaultValue(10)]
        public int PageSize { get; set; } = 20;
        public string? SortBy { get; set; } = "CreatedDate"; // Cột sắp xếp
        public bool IsDescending { get; set; } = true;       // Sắp xếp giảm dần
    }
    public class UserOrderFilterRequest
    {
        public OrderStatusEnum? Status { get; set; }
        public PaymentStatusEnum? PaymentStatus { get; set; }
        public ShippingStatusEnum? ShippingStatus { get; set; }

        public int? FromDate { get; set; }
        public int? ToDate { get; set; }

        [DefaultValue(1)]
        public int CurrentPage { get; set; } = 1;
        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
    }
    public class UserOrderSummaryModel
    {
        public int OrderId { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }

        // Short shipping + payment info for quick view
        public string PaymentStatus { get; set; }
        public string ShippingStatus { get; set; }
    }
    public class UserOrderDetailModel
    {
        // 1. Order info
        public int OrderId { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // 2. Items
        public List<UserOrderItemModel> Items { get; set; } = new();

        // 3. Payment summary
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }

        // 4. Shipping info
        public string ShippingStatus { get; set; }
        public string StartAddress { get; set; }
        public string EndAddress { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
    }
    public class UserOrderItemModel
    {
        public string ProductName { get; set; }
        public string? Color { get; set; }
        public string? SizeType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal => Price * Quantity;
        public string? ImageUrl { get; set; }
    }

    public class AddOrderItemDTO
    {
        public int ProductVariantId { get; set; }
        public int? SizeId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateOrderItemQuantityDTO
    {
        public int ProductVariantId { get; set; }
        public int? SizeId { get; set; }
        public int NewQuantity { get; set; }
    }

    public class RemoveOrderItemDTO
    {
        public int ProductVariantId { get; set; }
        public int? SizeId { get; set; }
    }
}
