using System;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;
using ECommerceSystem.Core;
using ECommerceSystem.Core.Interfaces;
using ECommerceSystem.Infrastructure.Security;
using ECommerceSystem.Core.Services;

namespace ECommerceSystem.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            IUnitOfWork unitOfWork,
            JwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<User> RegisterAsync(
            string email,
            string password,
            UserRole role)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                PasswordHash = hashedPassword,
                Role = role,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return user;
        }

        public async Task<string> LoginAsync(
            string email,
            string password)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            return _jwtTokenGenerator.GenerateToken(user);
        }
    }
}
