namespace aiproject.Dto
{
    public class UserRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public UserRequest(string username, string email)
        {
            Username = username;
            Email = email;
        }
    }
}