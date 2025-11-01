using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Services;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public UserController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        //Lấy thông tin người dùng theo userId
        [Authorize]
        [HttpGet("information")]
        public async Task<IActionResult> GetUserById()
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var userModel = _mapper.Map<UserModel>(user);
            return Ok(userModel);
        }

        //Đăng ký người dùng mới
        //[HttpPost("register")]
        //public async Task<IActionResult> RegisterUser([FromBody] CreateUserModel registerUser)
        //{
        //    var userEntity = _mapper.Map<User>(registerUser);
        //    var isSuccess = await _userService.RegisterUser(userEntity);
        //    if (!isSuccess)
        //    {
        //        return BadRequest("Registration failed. User may already exist.");
        //    }
        //    return Ok();
        //}

        //Cập nhật thông tin người dùng
        [Authorize]
        [HttpPut("information/update")]
        public async Task<IActionResult> UpdateUserInformation([FromBody] UpdateUserModel updateUser)
        {
            var userPrincipal = HttpContext.User;
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdClaim.Value);

            var updateUserEntity = _mapper.Map<User>(updateUser);
            updateUserEntity.Id = userId;

            var isSuccess = await _userService.UpdateUserAsync(updateUserEntity);
            if (!isSuccess)
            {
                return BadRequest(new { isSuccess = false, message = "Cập nhật thất bại" });
            }
            return Ok(new { isSuccess = true, message = "Cập nhật thành công" });
        }
    }
}
