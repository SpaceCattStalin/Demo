using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Entities;
using Services.DTOs;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthEmailOtpService : IAuthEmailOtpService
    {
        private readonly DemoDbContext _db;
        private readonly IOtpGenerator _gen;
        private readonly IOtpProtector _prot;
        private readonly IEmailSender _email;

        public AuthEmailOtpService(DemoDbContext db, IOtpGenerator gen, IOtpProtector prot, IEmailSender email)
        {
            _db = db; _gen = gen; _prot = prot; _email = email;
        }

        public async Task<RegisterInitResponse> InitAsync(RegisterInitRequest req)
        {
            // 1) validate tối thiểu
            if (string.IsNullOrWhiteSpace(req.Name)) throw new ArgumentException("Name is required.");
            if (string.IsNullOrWhiteSpace(req.Email)) throw new ArgumentException("Email is required.");
            if (string.IsNullOrWhiteSpace(req.Password)) throw new ArgumentException("Password is required.");
            if (!string.Equals(req.Password, req.ConfirmPassword)) throw new ArgumentException("Passwords do not match.");

            // 2) chống trùng email
            if (await _db.Users.AnyAsync(u => u.Email == req.Email))
                throw new InvalidOperationException("Email already exists.");

            // 3) hash password ngay từ phiên pending
            var pwdHash = BCrypt.Net.BCrypt.HashPassword(req.Password);

            // 4) sinh OTP 4 số + TTL 5 phút
            var code = _gen.Generate4(); // ví dụ: "0385"
            var pending = new PendingEmailRegistration
            {
                Name = req.Name.Trim(),
                Email = req.Email.Trim(),
                PasswordHash = pwdHash,
                RoleId = 2,
                OtpHash = "", // set ngay sau khi có Id
                ExpiresAtUtc = DateTime.UtcNow.AddMinutes(5),
                AttemptCount = 0,
                MaxAttempts = 5,
                ResendCount = 0
            };
            _db.PendingEmailRegistrations.Add(pending);
            await _db.SaveChangesAsync(); // để có pending.Id

            pending.OtpHash = _prot.Hash(code, pending.Id);
            await _db.SaveChangesAsync();

            // 5) gửi email OTP đơn giản
            var html = $@"
            <p>Xin chào {pending.Name},</p>
            <p>Mã xác thực của bạn là:</p>
            <h2 style='letter-spacing:4px'>{code}</h2>
            <p>Mã có hiệu lực trong 5 phút. Vui lòng không chia sẻ cho bất kỳ ai.</p>";
            await _email.SendAsync(pending.Email, "Mã xác thực đăng ký", html);
            pending.LastSentAtUtc = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            return new RegisterInitResponse(pending.Id, 300);
        }

        public async Task<RegisterVerifyResponse> VerifyAsync(RegisterVerifyRequest req)
        {
            // 1) Lấy phiên pending còn hiệu lực
            var p = await _db.PendingEmailRegistrations
                .FirstOrDefaultAsync(x => x.Id == req.TransactionId && !x.IsCompleted)
                ?? throw new InvalidOperationException("Session not found.");

            if (p.ExpiresAtUtc < DateTime.UtcNow) throw new InvalidOperationException("OTP expired.");
            if (p.AttemptCount >= p.MaxAttempts) throw new InvalidOperationException("Too many attempts.");

            // 2) Kiểm mã
            if (!_prot.Verify(req.Otp, p.Id, p.OtpHash))
            {
                p.AttemptCount++;
                await _db.SaveChangesAsync();
                throw new InvalidOperationException("Invalid OTP.");
            }

            // 3) Phòng race-condition email
            if (await _db.Users.AnyAsync(u => u.Email == p.Email))
                throw new InvalidOperationException("Email already exists.");

            // 4) Tạo User + Cart trong transaction
            using var tx = await _db.Database.BeginTransactionAsync();

            // Tạo số điện thoại placeholder 10 số (vì cột Phone non-null, dài 10)
            // Bạn có thể cho user cập nhật sau.
            var rnd = System.Security.Cryptography.RandomNumberGenerator.GetInt32(0, 1_000_000_000);
            var placeholderPhone = "0" + rnd.ToString("D9"); // ví dụ: 0 + 9 chữ số

            var user = new Repositories.Entities.User
            {
                Name = p.Name,
                Email = p.Email,
                Password = p.PasswordHash,      // LƯU BCrypt hash
                Phone = placeholderPhone,
                Birthday = null,
                Address = null,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                IsActive = true,
                RoleId = p.RoleId             // mặc định 2 (User)
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();          // để có user.Id

            // Tạo Cart mặc định rỗng cho user
            var cart = new Repositories.Entities.Cart
            {
                UsersId = user.Id
                // Các field khác (Amount, v.v.) để mặc định nếu không bắt buộc
            };
            _db.Carts.Add(cart);

            // Đánh dấu hoàn tất phiên
            p.IsCompleted = true;

            await _db.SaveChangesAsync();
            await tx.CommitAsync();

            return new RegisterVerifyResponse(user.Id, user.Email, user.Name);
        }
        public async Task<RegisterResendResponse> ResendAsync(RegisterResendRequest req)
        {
            var p = await _db.PendingEmailRegistrations
                .FirstOrDefaultAsync(x => x.Id == req.TransactionId && !x.IsCompleted)
                ?? throw new InvalidOperationException("Session not found.");

            // Cooldown ≥ 60s
            if (p.LastSentAtUtc.HasValue && DateTime.UtcNow < p.LastSentAtUtc.Value.AddSeconds(60))
                throw new InvalidOperationException("Please wait 60 seconds before resending.");

            // Giới hạn số lần gửi lại (ví dụ 3 lần)
            if (p.ResendCount >= 3)
                throw new InvalidOperationException("Resend limit reached.");

            // Nếu phiên đã hết hạn, cho phép cấp lại mã và gia hạn TTL 5 phút
            // (hoặc bạn có thể bắt buộc tạo phiên mới tùy policy)
            var code = _gen.Generate4();
            p.OtpHash = _prot.Hash(code, p.Id);
            p.ExpiresAtUtc = DateTime.UtcNow.AddMinutes(5);
            p.ResendCount += 1;

            var html = $@"
        <p>Xin chào {p.Name},</p>
        <p>Mã xác thực mới của bạn là:</p>
        <h2 style='letter-spacing:4px'>{code}</h2>
        <p>Mã có hiệu lực trong 5 phút. Vui lòng không chia sẻ cho bất kỳ ai.</p>";

            await _email.SendAsync(p.Email, "Mã xác thực mới", html);
            p.LastSentAtUtc = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return new RegisterResendResponse(
                Ok: true,
                ExpiresInSeconds: 300,
                ExpiresAtUtc: p.ExpiresAtUtc
            );
        }

    }
}
