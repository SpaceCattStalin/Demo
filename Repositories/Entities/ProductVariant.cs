namespace Repositories.Entities
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CreatedAt { get; set; }
        public int UpdatedAt { get; set; }
        public string VariantCode { get; set; }
        //public int StockQuantity { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string? Color { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
        public virtual ICollection<ProductSize> Sizes { get; set; }
    }
}
