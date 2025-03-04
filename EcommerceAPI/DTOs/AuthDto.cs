namespace EcommerceAPI.DTOs
{
    // For Registration
    public class RegisterDto
    {
        public string Name { get; set; } = string.Empty;   // Required for registration
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    // For Login
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;   // Required for login
        public string Password { get; set; } = string.Empty;
    }

    // For Login Response
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public UserDto User { get; set; } = new UserDto();
    }
}
