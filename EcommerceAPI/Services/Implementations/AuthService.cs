using EcommerceAPI.Data;
using EcommerceAPI.DTOs;
using EcommerceAPI.Models;
using EcommerceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceAPI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly EcommerceDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(EcommerceDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // Registration with Name, Email, and Password
        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                return "Email already registered";

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return "Registration successful";
        }

        // Login with Email and Password only
        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return null;

            var token = GenerateJwtToken(user);
            return new AuthResponseDto { Token = token,
        User = new UserDto
        {
            UserId = user.UserId,
            Name = user.Name,
            Email = user.Email,
            IsPremium = user.IsPremium 
        } };
        }

        // Generate JWT Token
        private string GenerateJwtToken(User user)
        {
            var jwtKey = _config["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
                throw new ArgumentNullException("Jwt:Key", "JWT key is not configured properly.");

            var key = Encoding.ASCII.GetBytes(jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("IsPremium", user.IsPremium.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<UserDto?> GetUserAsync(string email)
        {
            var user = await _context.Users
                .AsNoTracking()  // Improves performance since we don’t need to update this user
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return null;

            // Map User to UserDto
            var userDto = new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                IsPremium = user.IsPremium
            };

            return userDto;
        }
    }
}
