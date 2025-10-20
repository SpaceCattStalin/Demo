using Repositories.Entities;

namespace API.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public DateOnly? Birthday { get; set; }

        public string? Address { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool? IsActive { get; set; }

        public RoleModel Role { get; set; }
    }
}
