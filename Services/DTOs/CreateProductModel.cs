namespace Services.DTOs
{
    public class CreateProductModel
    {
        public string Name { get; set; } = null!;

        public int CategoryId { get; set; }

        public decimal Price { get; set; }
        //public string Color { get; set; }
        //public int StockQuantity { get; set; }

        public List<CreateProductImageModel> Images { get; set; }
        public List<CreateProductVariantModel> Variants { get; set; }
    }

    public class CreateProductVariantModel
    {
        public string VariantCode { get; set; }
        public int StockQuantity { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public List<CreateProductImageModel> Images { get; set; }
    }

    public class CreateProductImageModel
    {
        public int ImageTypeId { get; set; }
        public string Url { get; set; }
        public bool IsPrimary { get; set; }
        public int SortOrder { get; set; }
    }
}
