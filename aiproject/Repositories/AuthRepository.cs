using System.Linq;
using aiproject.Entities;
using Microsoft.EntityFrameworkCore;

namespace aiproject.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DatabaseContext _databaseContext;

        public AuthRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public UserEntity Register(UserEntity userEntity, string password)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            userEntity.PasswordHash = passwordHash;
            userEntity.PasswordSalt = passwordSalt;

            _databaseContext.Set<UserEntity>().Add(userEntity);
            _databaseContext.SaveChanges();

            return userEntity;
        }

        public UserEntity Login(string username, string password)
        {
            var user = _databaseContext.Set<UserEntity>().Include(u=>u.RoleEntity).FirstOrDefault(userEntity => userEntity.Username == username);
            if (user == null)
            {
                return null;
            }

            return !VerifyPassword(password, user.PasswordHash, user.PasswordSalt) ? null : user;
        }

        public bool UserExists(string username)
        {
            if (_databaseContext.Set<UserEntity>().Any(user => user.Username == username))
                return true;
            return false;
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return !computedHash.Where((t, i) => t != passwordHash[i]).Any();
        }
        
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}