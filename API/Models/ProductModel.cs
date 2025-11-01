using Repositories.Entities;

namespace API.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public List<ProductVariantModel> Variants { get; set; }
        public List<ProductImageModel> Images { get; set; }
    }

    public class ProductVariantModel
    {
        public int Id { get; set; }
        public string VariantCode { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }

        public List<ProductImageModel> Images { get; set; }
        public List<ProductSizeModel> Sizes { get; set; }
    }

    public class ProductImageModel
    {
        public string Url { get; set; }
        public bool IsPrimary { get; set; }
        public int SortOrder { get; set; }
    }

    public class ProductSizeModel
    {
        public int StockQuantity { get; set; }
        public string Size { get; set; }
    }
    public class SizeDTO
    {
        public int Id { get; set; }
        public string SizeType { get; set; }
    }

}
