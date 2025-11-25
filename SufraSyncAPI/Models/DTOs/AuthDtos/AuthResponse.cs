namespace SufraSyncAPI.Models.DTOs.AuthDtos
{
    public class AuthResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        // Optional: public string Email { get; set; }
        // Optional: public List<string> Roles { get; set; }
    }
}
