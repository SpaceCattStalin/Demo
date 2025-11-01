namespace Services.DTOs
{
    public class UpdateProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public List<UpdateProductImageModel>? Images { get; set; } = new List<UpdateProductImageModel>();
    }

    public class UpdateProductImageModel
    {
        public int Id { get; set; }
        public string Url { get; set; }

        //public int SortOrder { get; set; }
    }
}
