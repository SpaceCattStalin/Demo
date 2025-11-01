using Services.Utils;
using System.ComponentModel;

namespace Services.DTOs
{
    public class AdminPaymentFilterRequest
    {
        public string? Keyword { get; set; }      // Order ID, Email, User name, Method
        public PaymentStatusEnum? Status { get; set; }
        public int? MethodId { get; set; }
        public int? UserId { get; set; }
        public int? FromDate { get; set; }
        public int? ToDate { get; set; }
        [DefaultValue(1)]
        public int CurrentPage { get; set; } = 1;
        [DefaultValue(10)]
        public int PageSize { get; set; } = 20;
        public string? SortBy { get; set; } = "CreatedDate";
        public bool IsDescending { get; set; } = true;
    }
}
