﻿using System.Linq;
using MovieEntity;
using MovieInterface;
using MovieManagerContext;
using System.Collections.Generic;
using System.Threading;

namespace MovieManagerImplement
{
    public class UserDao : IUserDao
    {
        private UserContext context;
        private static Mutex mutex;

        public UserDao(UserContext context)
        {
            this.context = context;
            mutex = new Mutex();
        }

        public long CreateUser(User user)
        {
            if (user.nick == "" || user.nick == null || user.password == "" || user.password == null)
            {
                return -2;
            }
            IEnumerable<User> temp_u = context.users.Where(t_u => t_u.nick == user.nick);
            if (temp_u.Count() == 0)
            {
                mutex.WaitOne();
                context.Add(user);
                context.SaveChanges();
                var u = context.users.Where(t_u => t_u.nick == user.nick).First();
                mutex.ReleaseMutex();
                return u.id;
            }
            //重名
            return -1;
        }

        public bool DeleteUserById(long id)
        {
            User user = context.users.SingleOrDefault(s => s.id == id);
            context.users.Remove(user);
            return context.SaveChanges() > 0;

        }

        public User GetUserById(long id)
        {
            User user = context.users.SingleOrDefault(s => s.id == id);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public bool UpdateUserById(long id, User user)
        {

            User user_check = context.users.SingleOrDefault(s => s.id == id);
            if (user != null)
            {
                user_check.nick = user.nick;
                user_check.password = user.password;
                user_check.gender = user.gender;
                user_check.avatar_url = user.avatar_url;
                return context.SaveChanges() > 0;
            }
            return false;
        }

        public long Login(string user_name, string password)
        {
            User user = context.users.SingleOrDefault(s => s.nick == user_name && s.password == password);
            if (user == null)
            {
                return -1;
            }
            return user.id;
        }
    }
}
