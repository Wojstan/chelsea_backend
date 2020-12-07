using System.Collections.Generic;

namespace aiproject.Entities
{
    public class RoleEntity : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}