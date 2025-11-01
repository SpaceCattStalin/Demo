namespace Repositories.Entities
{
    public class Size
    {
        public int Id { get; set; }
        public string SizeType { get; set; }
        public ICollection<ProductSize> ProductSize { get; set; }
    }
}
