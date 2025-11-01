using System.ComponentModel;

namespace Repositories.DTOs
{
    public class AdminOrderFilterRequest
    {
        //public string? Keyword { get; set; }          // Tìm theo tên, email user, mã đơn
        public string? Status { get; set; }
        public string? PaymentStatus { get; set; }
        public string? ShippingStatus { get; set; }
        public int? UserId { get; set; }              // Lọc theo người dùng cụ thể
        public int? FromDate { get; set; }
        public int? ToDate { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? SortBy { get; set; } = "CreatedDate"; // Cột sắp xếp
        public bool IsDescending { get; set; } = true;       // Sắp xếp giảm dần
    }
    public class UserOrderFilterRequest
    {
        public string? Status { get; set; }
        public string? PaymentStatus { get; set; }
        public string? ShippingStatus { get; set; }

        public int? FromDate { get; set; }
        public int? ToDate { get; set; }

        [DefaultValue(1)]
        public int CurrentPage { get; set; } = 1;
        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
    }
}
