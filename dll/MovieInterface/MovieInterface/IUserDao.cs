using MovieEntity;

namespace MovieInterface
{
    public interface IUserDao
    {
        long CreateUser(User user);

        bool DeleteUserById(long id);

        bool UpdateUserById(long id, User user);

        User GetUserById(long id);

        long Login(string user_name, string password);
    }
}
