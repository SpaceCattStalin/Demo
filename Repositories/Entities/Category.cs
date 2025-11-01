namespace Repositories.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CreatedAt { get; set; } = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
        public int UpdatedAt { get; set; } = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
        public bool IsDeleted { get; set; } = false;
        public List<Product> Products { get; set; }
        public CategoryImage Image { get; set; }
    }
}
