namespace Repositories.Entities
{
    public class ProductSize
    {
        public int Id { get; set; }
        public int StockQuantity { get; set; }
        public int SizeId { get; set; }
        public Size Size { get; set; }
        //public int? ProductId { get; set; }
        //public Product? Product { get; set; }
        public int? ProductVariantId { get; set; }
        public ProductVariant? ProductVariant { get; set; }
    }
}
