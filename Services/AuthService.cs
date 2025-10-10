
using Repositories.Entities;

namespace Services
{
    public sealed class AuthService
    {

        private readonly IUnitOfWork _unitOfWork;
        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetUserAccount(string username, string password)
        {
            try
            {
                //var user = await _respository.GetUser(username, password);
                var user = await _unitOfWork.SystemUserAccountRepository.GetUserAsync(username, password);

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error login", ex);
            }
        }
    }
}
