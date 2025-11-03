using System.ComponentModel;

namespace Services.DTOs
{
    public class ProductFilterRequest
    {
        public required int CategoryId { get; set; }
        public string? Keyword { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        [DefaultValue(1)]
        public int CurrentPage { get; set; } = 1;
        [DefaultValue(10)]

        public int PageSize { get; set; } = 20;

        // Sorting
        //public string? SortBy { get; set; } = "CreatedAt";
        public bool IsDescending { get; set; } = true;
    }

    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int? StockQuantity { get; set; }
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

    public class ProductSizeDto
    {
        public int StockQuantity { get; set; }
        public string Size { get; set; }
    }


}
