using MovieManager.Entities;
using System.Collections.Generic;

namespace MovieManager.DataAccess.Interface
{
    public interface IUserForumDao
    {
        // 添加用户关注的讨论组
        int CreateUserForum(UserForum userForum);

        // 查看用户关注的讨论组
        IEnumerable<object> GetUserForumByUserId(long user_id);

        // 用户取关讨论组
        bool DeleteUserForum(long user_id, int forum_id);

    }
}
