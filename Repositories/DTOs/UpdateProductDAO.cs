namespace Repositories.DTOs
{
    public class UpdateProductDAO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public List<UpdateProductImageDAO>? Images { get; set; } = new List<UpdateProductImageDAO>();
    }

    public class UpdateProductImageDAO
    {
        public int Id { get; set; }
        public string Url { get; set; }

        //public int SortOrder { get; set; }
    }
}
