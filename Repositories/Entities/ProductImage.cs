namespace Repositories.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int? MainProductId { get; set; }
        public int? ProductVariantId { get; set; }
        public int ImageTypeId { get; set; }
        public string Url { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsCategory { get; set; } = false;
        public int SortOrder { get; set; } = 1;
        public int CreatedAt { get; set; }
        public int UpdatedAt { get; set; }
        public Product Product { get; set; }
        public ProductVariant ProductVariant { get; set; }
        public ImageType ImageType { get; set; }

    }
}
