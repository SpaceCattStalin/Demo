using Repositories.Entities;
using Repositories.UnitOfWorks;

namespace Services
{
    public sealed class AuthService
    {

        private readonly IUnitOfWork _unitOfWork;
        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User?> GetUserByCredentials(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            var normEmail = email.Trim();
            var user = await _unitOfWork.UserRepository.GetUserWithRoleByEmailAsync(normEmail);
            if (user == null) return null;

            
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            
            if (user.IsActive.HasValue && !user.IsActive.Value)
                return null;

            return user;
        }
    }
}
