using System.Collections.Generic;
using MovieEntity;

namespace MovieInterface
{
    public interface IForumDao
    {
        IEnumerable<Forum> GetAllForums();

        IEnumerable<Forum> GetForumsByName(string name);

        IEnumerable<Forum> GetForumsByUserId(long id);

        int CreateForum(Forum forum);

        bool DeleteForumById(int id, long user_id);
    }
}
