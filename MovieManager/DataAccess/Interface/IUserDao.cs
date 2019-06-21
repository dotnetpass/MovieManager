using System;
using MovieManager.Entities;

namespace MovieManager.DataAccess.Interface
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
