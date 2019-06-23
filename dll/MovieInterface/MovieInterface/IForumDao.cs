using System.Collections.Generic;
using MovieEntity;

namespace MovieInterface
{
    public interface IForumDao
    {
        object GetAllForums(int page, int size);

        object GetForumById(int id, long user_id);

        object GetForumsByName(string name);

        IEnumerable<Forum> GetForumsByUserId(long id);

        int CreateForum(Forum forum);

        bool DeleteForumById(int id, long user_id);
    }
}
