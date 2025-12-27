using ECommerceSystem.Core.Entities;
using ECommerceSystem.Core.Interfaces;
using ECommerceSystem.Core.Services;
using ECommerceSystem.Infrastructure.Security;

namespace ECommerceSystem.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUnitOfWork unitOfWork, JwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<User> RegisterAsync(string email, string password, UserRole role)
        {
            var existingUser = await _unitOfWork.Users.GetByEmailAsync(email);
            if (existingUser != null)
                throw new Exception("Email already exists");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                PasswordHash = PasswordHasher.HashPassword(password),
                Role = role,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return user;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(email);
            if (user == null)
                throw new Exception("Invalid credentials");

            if (!PasswordHasher.VerifyPassword(password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            return _jwtTokenGenerator.GenerateToken(user);
        }
    }
}
