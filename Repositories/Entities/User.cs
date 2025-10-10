namespace Repositories.Entities
{
    public class User
    {
        public int UserId { get; set; }              // Primary Key
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Email { get; set; }

        // Navigation
        public Cart Cart { get; set; } = null!;
    }
}
