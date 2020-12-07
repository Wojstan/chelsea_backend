using System.Collections.Generic;

namespace aiproject.Entities
{
    public class UserEntity : IEntity
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int RoleId { get; set; }

        public RoleEntity RoleEntity { get; set; }
        public List<RatingEntity> Ratings { get; set; } = new List<RatingEntity>();
    }
}