namespace aiproject.Dto
{
    public class LoginResponse
    {
        public UserResponse User { get; set; }
        public string Token { get; set; }
    }

    public class UserResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}