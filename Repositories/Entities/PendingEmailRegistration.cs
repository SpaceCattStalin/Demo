using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class PendingEmailRegistration
    {
        public Guid Id { get; set; } = Guid.NewGuid();  

        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public int RoleId { get; set; } = 2;

        // OTP
        public string OtpHash { get; set; } = default!;        
        public DateTime ExpiresAtUtc { get; set; }             
        public int AttemptCount { get; set; } = 0;             
        public int MaxAttempts { get; set; } = 5;
        public int ResendCount { get; set; } = 0;
        public DateTime? LastSentAtUtc { get; set; }

        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
