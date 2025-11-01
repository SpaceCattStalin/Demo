namespace API.Models
{
    public class UpdateUserModel
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        //public string Password { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public DateOnly? Birthday { get; set; }

        public string? Address { get; set; }
    }
}
