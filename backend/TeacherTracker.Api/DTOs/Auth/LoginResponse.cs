namespace TeacherTracker.Api.DTOs.Auth
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;

        public string TokenType { get; set; } = string.Empty;

        public int ExpiresIn { get; set; } = 900;
    }
}
