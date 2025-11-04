using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utils
{
    public class OtpGenerator : IOtpGenerator
    {
        public string Generate4()
        {
            Span<byte> bytes = stackalloc byte[4];
            RandomNumberGenerator.Fill(bytes);

            Span<char> digits = stackalloc char[4];
            for (int i = 0; i < 4; i++)
                digits[i] = (char)('0' + (bytes[i] % 10));

            return new string(digits);
        }
    }
}
