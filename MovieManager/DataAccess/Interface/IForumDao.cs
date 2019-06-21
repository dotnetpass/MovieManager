using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieManager.Entities;

namespace MovieManager.DataAccess.Interface
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
