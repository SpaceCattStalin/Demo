using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("auth/email-otp")]
    public class AuthEmailOtpController : ControllerBase
    {
        private readonly IAuthEmailOtpService _svc;
        public AuthEmailOtpController(IAuthEmailOtpService svc) => _svc = svc;

        [HttpPost("init")]
        public async Task<ActionResult<RegisterInitResponse>> Init([FromBody] RegisterInitRequest req)
        {
            var res = await _svc.InitAsync(req);
            return Ok(res);
        }

        [HttpPost("verify")]
        public async Task<ActionResult<RegisterVerifyResponse>> Verify([FromBody] RegisterVerifyRequest req)
        {
            try
            {
                var res = await _svc.VerifyAsync(req);
                return Ok(res);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost("resend")]
        public async Task<ActionResult<RegisterResendResponse>> Resend([FromBody] RegisterResendRequest req)
        {
            try
            {
                var res = await _svc.ResendAsync(req);
                return Ok(res);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
