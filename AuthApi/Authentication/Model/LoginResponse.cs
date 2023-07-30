namespace AuthApi.Authentication.Model
{
    public class LoginResponse
    {
        public string UserName { get; set; } = string.Empty;
        public string JwtToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
    }
}
