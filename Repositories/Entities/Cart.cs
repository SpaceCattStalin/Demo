namespace Repositories.Entities
{
    public class Cart
    {
        public int CartId { get; set; }              // Primary Key

        // Foreign Keys
        public int UserId { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
