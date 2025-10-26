namespace Repositories.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CreatedAt { get; set; }
        public int UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<Product> Products { get; set; }
    }
}
