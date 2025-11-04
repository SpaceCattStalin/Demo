using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.Entities;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly AuthService _authService;
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IConfiguration config, AuthService authService, IMapper mapper, UserService userService)
        {
            _config = config;
            _authService = authService;
            _mapper = mapper;
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            var user = await _authService.GetUserByCredentials(request.Email, request.Password);
            if (user == null)
                return Unauthorized();

            var token = GenerateJSONWebToken(user);
            return Ok(token);
        }


        //Đăng ký người dùng mới
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserModel registerUser)
        {
            var userEntity = _mapper.Map<User>(registerUser);
            var isSuccess = await _userService.RegisterUser(userEntity);
            if (!isSuccess)
            {
                return BadRequest("Registration failed. User may already exist.");
            }
            return Ok();
        }

        private string GenerateJSONWebToken(User user)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                    , _config["Jwt:Audience"]
                    , new Claim[]{
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Name.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role.Name),
                    },
                    signingCredentials: credentials,
                    expires: DateTime.Now.AddHours(5)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

    }
}
