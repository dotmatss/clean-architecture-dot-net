
using Application.Auth;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthController(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var existing = await _userRepository.GetUserByEmailAsync(request.Email);
            if (existing != null) return BadRequest("Email already exists.");

            var user = new User
            {
                Email = request.Email,
                PasswordHash = PasswordHasher.Hash(request.Password)
            };

            await _userRepository.AddUserAsync(user);
            return Ok("Registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(RegisterRequest request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null) return Unauthorized("User not found.");

            if (!PasswordHasher.Verify(request.Password, user.PasswordHash))
                return Unauthorized("Invalid password.");

            var jwt = GenerateToken(user);
            return Ok(new { token = jwt });
        }


        private string GenerateToken(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentNullException("User email cannot be null");

            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        // Use email as unique identifier
            new Claim("userId", user.Email),
            new Claim(ClaimTypes.Email, user.Email)
        };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(5),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

