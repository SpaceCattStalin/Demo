using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAuthEmailOtpService
    {
        Task<RegisterInitResponse> InitAsync(RegisterInitRequest req);
        Task<RegisterVerifyResponse> VerifyAsync(RegisterVerifyRequest req);
        Task<RegisterResendResponse> ResendAsync(RegisterResendRequest req);

    }
}
