using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Identity.Client;
using Repositories.Entities;
using Repositories.UnitOfWorks;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //Lấy tất cả danh sách người dùng
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }

        //Lấy thông tin người dùng theo userId
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(userId);
        }

        //Đăng ký người dùng mới
        public async Task<bool> RegisterUser(User user)
        {
            var existingUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return false;
            }

            user.Password = user.Password;
            user.CreatedDate = DateTime.Now;
            user.IsActive = true;
            user.RoleId = 2;
            var numberUserValid = await _unitOfWork.UserRepository.CreateAsync(user);
            
            return numberUserValid > 0;
        }

        //Cập nhật thông tin người dùng
        public async Task<bool> UpdateUserAsync(User user)
        {
            var numberChange = await _unitOfWork.UserRepository.UpdateAsync(user);
            if (numberChange == 0)
            {
                return false;
            }
            return true;
        }
    }
}
