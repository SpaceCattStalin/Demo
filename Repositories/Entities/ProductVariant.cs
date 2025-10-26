namespace Repositories.Entities
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CreatedAt { get; set; }
        public int UpdatedAt { get; set; }
        public string VariantCode { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public ICollection<ProductImage> Images { get; set; }
    }
}
