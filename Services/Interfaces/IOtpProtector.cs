using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IOtpProtector
    {
        string Hash(string otp, Guid transactionId);
        bool Verify(string otp, Guid transactionId, string hash);
    }
}
