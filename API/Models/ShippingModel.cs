using Repositories.Entities;

namespace API.Models
{
    public class ShippingModel
    {
        public int ShippingId { get; set; }

        public string Status { get; set; } = null!;

        public string StartAddress { get; set; } = null!;

        public string EndAddress { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }
    }
    public class ShippingDetailDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string StartAddress { get; set; }
        public string EndAddress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }

        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public decimal Total { get; set; }

        public List<ShippingOrderItemDto> Items { get; set; }
    }

    public class ShippingOrderItemDto
    {
        public string ProductName { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
    }
}
