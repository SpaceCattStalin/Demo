namespace Services.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsAvailable { get; set; }

        public string CategoryName { get; set; }

        public List<ProductVariantDto> Variants { get; set; }
        public List<ProductImageDto> Images { get; set; }
    }

    public class ProductVariantDto
    {
        public int Id { get; set; }
        public string VariantCode { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public List<ProductImageDto> Images { get; set; }
    }

    public class ProductImageDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsCategory { get; set; }
        public int SortOrder { get; set; }
        public string ImageTypeCode { get; set; }
    }
}
