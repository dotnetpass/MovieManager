using System.Linq;
using MovieManager.Entities;
using MovieManager.DataAccess.Interface;
using MovieManager.DataAccess.Base;
using System.Collections.Generic;

namespace MovieManager.DataAccess.Implement
{
    public class UserForumDao : IUserForumDao
    {
        private UserForumContext context;

        public UserForumDao(UserForumContext context)
        {
            this.context = context;
        }

        public int CreateUserForum(UserForum userForum)
        {
            var temp_user_forum = context.userForums.Where(t => t.user_id == userForum.user_id && t.forum_id == userForum.forum_id);
            if (temp_user_forum.Count() > 0)
            {
                return -1;
            }
            else
            {
                var new_user_forum = context.userForums.Add(userForum);
                context.SaveChanges();
                return new_user_forum.Entity.id;
            }
        }

        public bool DeleteUserForum(long user_id, int forum_id)
        {
            var temp_user_forum = context.userForums.SingleOrDefault(t => t.user_id == user_id && t.forum_id == forum_id);
            if (temp_user_forum != null)
            {
                context.userForums.Remove(temp_user_forum);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<object> GetUserForumByUserId(long user_id)
        {
            var result = from forums in context.forums
                         join userForums in context.userForums on forums.id equals userForums.forum_id
                         where userForums.user_id == user_id
                         select new
                         {
                             id = forums.id,
                             name = forums.name,
                             description = forums.description,
                             publisher_id = forums.publisher_id
                         };
            return result;
        }
    }
}
