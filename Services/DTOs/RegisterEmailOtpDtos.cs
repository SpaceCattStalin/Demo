using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public record RegisterInitRequest(string Name, string Email, string Password, string ConfirmPassword);
    public record RegisterInitResponse(Guid TransactionId, int ExpiresInSeconds);
    public record RegisterVerifyRequest(Guid TransactionId, string Otp);

    public record RegisterVerifyResponse(int UserId, string Email, string Name);
    public record RegisterResendRequest(Guid TransactionId);
    public record RegisterResendResponse(bool Ok, int ExpiresInSeconds, DateTime ExpiresAtUtc);

}
