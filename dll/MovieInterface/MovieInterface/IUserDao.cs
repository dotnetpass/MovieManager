using MovieEntity;

namespace MovieInterface
{
    public interface IUserDao
    {
        object CreateUser(User user);

        bool DeleteUserById(long id);

        bool UpdateUserById(long id, User user);

        User GetUserById(long id);

        long Login(string user_name, string password);
    }
}
