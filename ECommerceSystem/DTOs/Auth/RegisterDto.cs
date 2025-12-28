using ECommerceSystem.Core.Entities;

namespace ECommerceSystem.DTOs.Auth
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
