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
            return await _unitOfWork.UserRepository.GetUserByEmailPasswordAsync(email, password);
        }
    }
}
