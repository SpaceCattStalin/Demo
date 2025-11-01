namespace Repositories.Entities
{
    public class CategoryImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
