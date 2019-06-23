using MovieEntity;
using MovieManagerContext;
using MovieInterface;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MovieManagerImplement
{
    public class ForumDao : IForumDao
    {
        private ForumContext context;

        public ForumDao(ForumContext context)
        {
            this.context = context;
        }

        public object GetAllForums(int page, int size)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            IEnumerable<Forum> data = null;
            int count = context.forums.Count();
            if (page == 1)
            {
                data = context.forums.Take(size);
            }
            else if (page * size <= count)
            {
                data = context.forums.Skip((page - 1) * size).Take(size);
            }
            else
            {
                data = context.forums.Skip((page - 1) * size).ToList();
            }
            int total_page = 0;
            if (count % size == 0)
            {
                total_page = count / size;
            }
            else
            {
                total_page = count / size + 1;
            }
            result.Add("page", page);
            result.Add("totalPage", total_page);
            result.Add("count", count);
            result.Add("pageSize", size);
            result.Add("data", data);
            return JsonConvert.SerializeObject(result);
        }

        public IEnumerable<Forum> GetForumsByName(string name)
        {
            return context.forums.Where(f => f.name.Contains(name));
        }

        public object GetForumById(int id)
        {
            Forum forum =  context.forums.SingleOrDefault(f => f.id == id);
            User user = context.users.SingleOrDefault(u => u.id == forum.publisher_id);
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("name", forum.name);
            result.Add("description", forum.description);
            if (user != null)
            {
                result.Add("nick", user.nick);
            }
            else
            {
                result.Add("nick", null);
            }
            
            return JsonConvert.SerializeObject(result);

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
