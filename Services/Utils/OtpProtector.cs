using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utils
{
    public class OtpProtector : IOtpProtector
    {
        private readonly byte[] _key;
        public OtpProtector(IConfiguration cfg)
        {
            _key = Encoding.UTF8.GetBytes(cfg["Otp:Secret"]!);
        }

        public string Hash(string otp, Guid transactionId)
        {
            var payload = $"{otp}:{transactionId}";
            using var hmac = new HMACSHA256(_key);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            return Convert.ToHexString(hash);
        }

        public bool Verify(string otp, Guid transactionId, string hash)
            => Hash(otp, transactionId).Equals(hash, StringComparison.OrdinalIgnoreCase);
    }
}
