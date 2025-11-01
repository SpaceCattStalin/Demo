namespace API.Models
{
    public class AdminOrderModel
    {
        public int OrderId { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }

        // Customer info
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        // Payment info
        public decimal PaymentAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string? PaymentMethod { get; set; }

        // Shipping info
        public string ShippingStatus { get; set; }
        public string? Destination { get; set; } // EndAddress
    }

    public class AdminOrderDetailModel
    {
        // 1. Thông tin đơn hàng
        public int OrderId { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? DiscountCode { get; set; }
        public string? Note { get; set; }

        // 2. Khách hàng
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserAddress { get; set; }

        // 3. Danh sách sản phẩm
        public List<AdminOrderItemModel> Items { get; set; } = new();

        // 4. Thanh toán
        public PaymentModel Payment { get; set; }

        // 5. Giao hàng
        public ShippingModel Shipping { get; set; }
    }

    public class AdminOrderItemModel
    {
        public string ProductName { get; set; }
        public string? Color { get; set; }
        public string? SizeType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal => Price * Quantity;
        public string? ImageUrl { get; set; }
    }

    //public class AdminPaymentDto
    //{
    //    public int Id { get; set; }
    //    public int OrderId { get; set; }
    //    public string UserName { get; set; }
    //    public string UserEmail { get; set; }
    //    public decimal Amount { get; set; }
    //    public string Type { get; set; }         // COD, VNPay, Stripe
    //    public string Status { get; set; }       // Pending, Success, Failed
    //    public DateTime CreatedDate { get; set; }
    //    public DateTime? ProcessedDate { get; set; }
    //}
}
