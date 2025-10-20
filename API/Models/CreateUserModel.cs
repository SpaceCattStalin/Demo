namespace API.Models
{
    public class CreateUserModel
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public DateOnly? Birthday { get; set; }

        public string? Address { get; set; }
    }
}
