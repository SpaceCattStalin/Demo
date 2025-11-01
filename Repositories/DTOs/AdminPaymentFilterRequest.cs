namespace Repositories.DTOs
{
    public class AdminPaymentFilterRequest
    {
        public string? Keyword { get; set; }      // Order ID, Email, User name, Method
        public string? Status { get; set; }
        public int? MethodId { get; set; }
        public int? UserId { get; set; }
        public int? FromDate { get; set; }
        public int? ToDate { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? SortBy { get; set; } = "CreatedDate";
        public bool IsDescending { get; set; } = true;
    }
}
