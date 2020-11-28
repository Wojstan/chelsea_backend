using aiproject.Repositories;

namespace aiproject.Entities
{
    public class UserEntity : IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}