using aiproject.Entities;

namespace aiproject.Repositories
{
    public interface IAuthRepository
    {
        UserEntity Register(UserEntity userEntity, string password);
        UserEntity Login(string username, string password);
        bool UserExists(string username);
    }
}