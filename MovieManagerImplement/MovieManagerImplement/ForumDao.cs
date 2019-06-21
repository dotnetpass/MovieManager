using MovieEntity;
using MovieManagerContext;
using MovieInterface;
using System.Collections.Generic;
using System.Linq;

namespace MovieManagerImplement
{
    public class ForumDao : IForumDao
    {
        private ForumContext context;

        public ForumDao(ForumContext context)
        {
            this.context = context;
        }

        public IEnumerable<Forum> GetAllForums()
        {
            return context.forums.ToList();
        }

        public IEnumerable<Forum> GetForumsByName(string name)
        {
            return context.forums.Where(f => f.name.Contains(name));
        }

        public int CreateForum(Forum forum)
        {
            var new_forum = context.forums.Add(forum);
            context.SaveChanges();
            return new_forum.Entity.id;
        }

        public IEnumerable<Forum> GetForumsByUserId(long id)
        {
            return context.forums.Where(f => f.publisher_id == id);
        }

        public bool DeleteForumById(int id, long user_id)
        {
            var forum = context.forums.SingleOrDefault(f => f.id == id && f.publisher_id == user_id);
            if (forum != null)
            {
                context.forums.Remove(forum);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
