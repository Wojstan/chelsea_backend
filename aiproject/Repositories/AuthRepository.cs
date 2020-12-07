using System.Linq;
using aiproject.Entities;

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
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            userEntity.PasswordHash = passwordHash;
            userEntity.PasswordSalt = passwordSalt;

            _databaseContext.Set<UserEntity>().Add(userEntity);
            _databaseContext.SaveChanges();

            return userEntity;
        }

        public UserEntity Login(string username, string password)
        {
            var user = _databaseContext.Set<UserEntity>().FirstOrDefault(userEntity => userEntity.Username == username);
            if (user == null)
            {
                return null;
            }

            if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public bool UserExists(string username)
        {
            if (_databaseContext.Set<UserEntity>().Any(user => user.Username == username))
                return true;
            return false;
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }
        
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}