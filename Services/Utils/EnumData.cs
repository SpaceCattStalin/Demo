namespace Services.Utils
{
    public enum PaymentStatusEnum
    {
        Pending,        // Đang chờ xử lý
        Success,        // Thanh toán thành công
        Failed,         // Thanh toán thất bại
        Cancelled,      // Giao dịch bị hủy
        Expired         // Giao dịch hết hạn
    }
    public enum OrderStatusEnum
    {
        Pending,        // Đang chờ thanh toán
        Processing,     // Đang xử lý đơn hàng
        Shipping,       // Đang vận chuyển
        Success,        // Thanh toán thành công
        Failed,         // Thanh toán thất bại
        Cancelled,      // Giao dịch bị hủy
        Expired         // Giao dịch hết hạn
    }

    public enum ShippingStatusEnum
    {
        Pending,        // Chờ xử lý
        Shipping,       // Đang vận chuyển
        Delivered,      // Đã giao hàng
        Failed,         // Giao hàng thất bại
    }

    public enum ImageTypeEnum
    {
        Front = 1,    // Mặt trước
        Back = 2,     // Mặt sau
        Side = 3,     // Mặt bên
        Top = 4,      // Nhìn từ trên
        Bottom = 5,   // Đế / đáy
        Detail = 6    // Ảnh cận cảnh (logo, chất liệu…)
    }
}
