namespace aiproject.Dto
{
    public class UserResponse
    {
        private int Id { get; set; }
        private string Username { get; set; }
        private string Email { get; set; }

        public UserResponse(int id, string username, string email)
        {
            Id = id;
            Username = username;
            Email = email;
        }
    }
}