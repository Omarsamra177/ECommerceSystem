using Microsoft.AspNetCore.Mvc;
using ECommerceSystem.Core.Interfaces;
using ECommerceSystem.Core.Entities;
using System.Threading.Tasks;
using ECommerceSystem.Core.Services;

namespace ECommerceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterRequest request)
        {
            var user = await _authService.RegisterAsync(
                request.Email,
                request.Password,
                request.Role
            );

            return Ok(new
            {
                user.Id,
                user.Email,
                user.Role
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request)
        {
            var token = await _authService.LoginAsync(
                request.Email,
                request.Password
            );

            return Ok(new
            {
                Token = token
            });
        }
    }

    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
