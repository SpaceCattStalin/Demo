namespace Repositories.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; }
        public string? SizeType { get; set; }
        public int? SizeId { get; set; }
        public Size? Size { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
