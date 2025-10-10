using System.Text.Json.Serialization;

namespace Repositories.Entities
{
    public class Product
    {
        public int ProductId { get; set; }           // Primary Key
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }

        // Navigation
        [JsonIgnore]

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
