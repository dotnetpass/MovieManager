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

        public object GetForumsByName(string name)
        {
            var result_forums = from forums in context.forums
                                join users in context.users on forums.publisher_id equals users.id
                                where forums.name.Contains(name)
                                select new
                                {
                                    id = forums.id,
                                    name = forums.name,
                                    description = forums.description,
                                    user_id = users.id,
                                    nick = users.nick
                                };
            return JsonConvert.SerializeObject(result_forums);
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
                UserForum userForum = context.userForums.SingleOrDefault(u => u.forum_id == id && u.user_id == user.id);
                if (userForum != null)
                {
                    result.Add("like", true);
                }
                else
                {
                    result.Add("like", false);
                }
            }
            else
            {
                result.Add("nick", null);
                result.Add("like", false);
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
