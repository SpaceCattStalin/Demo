using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("dev")]
    public class DevEmailController : ControllerBase
    {
        private readonly IEmailSender _email;
        public DevEmailController(IEmailSender email) => _email = email;

        public record EmailTestRequest(string To, string? Subject, string? Body);

        [HttpPost("email-test")]
        public async Task<IActionResult> Send([FromBody] EmailTestRequest req)
        {
            var subject = string.IsNullOrWhiteSpace(req.Subject) ? "SMTP test" : req.Subject!;
            var body = string.IsNullOrWhiteSpace(req.Body)
                ? "<p>Xin chào, đây là email test từ ECommerceSolution.</p>"
                : req.Body!;
            await _email.SendAsync(req.To, subject, body);
            return Ok(new { ok = true });
        }
    }
}
