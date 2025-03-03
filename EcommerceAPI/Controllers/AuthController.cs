using EcommerceAPI.DTOs;
using EcommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto authDto)
        {
            var result = await _authService.RegisterAsync(authDto);
            if (result == "Email already registered")
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto authDto)
        {
            var response = await _authService.LoginAsync(authDto);
            if (response == null)
                return Unauthorized("Invalid credentials");

            return Ok(response);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetUser()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            var user = await _authService.GetUserAsync(email);
            if (user == null) return NotFound();

            return Ok(user);
        }
    }
}
