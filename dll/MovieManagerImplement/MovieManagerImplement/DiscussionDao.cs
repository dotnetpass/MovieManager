using System.Collections.Generic;
using System.Linq;
using MovieInterface;
using MovieManagerContext;
using MovieEntity;
using System.Runtime.InteropServices;
using System;
using Newtonsoft.Json;

namespace MovieManagerImplement
{
    public class DiscussionDao : IDiscussionDao
    {
        private DiscussionContext context;

        public DiscussionDao(DiscussionContext context)
        {
            this.context = context;
        }

        public int AddDiscussion(Discussion discussion)
        {
            discussion.time = DateTime.Now;
            var new_discussion = context.discussions.Add(discussion);
            context.SaveChanges();
            return new_discussion.Entity.id;
            
        }

        public bool DeleteDiscussion(int discussion_id)
        {
            Discussion discussion = context.discussions.SingleOrDefault(d => d.id == discussion_id);
            if (discussion != null)
            {
                context.discussions.Remove(discussion);
                return context.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public object GetForumAndDiscussion(int forum_id, int page, int size)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            var temp_result = from discussion in context.discussions
                              join users in context.users on discussion.user_id equals users.id
                              orderby discussion.time descending
                              where discussion.forum_id == forum_id
                              select new
                              {
                                  id = discussion.id,
                                  avatar_url = users.avatar_url,
                                  gender = users.gender,
                                  user_id = users.id,
                                  nick = users.nick,
                                  content = discussion.content,
                                  time = discussion.time,
                                  forum_id = discussion.forum_id
                              };
            int count = temp_result.Count();
            result.Add("data", temp_result);
            if (temp_result == null || temp_result.Count() == 0)
            {
                result.Add("data", null);
            }
            if (page == 1)
            {
                result.Add("data", temp_result.Take(size));
            }
            else if (page * size <= count)
            {
                result.Add("data", temp_result.Skip((page - 1) * size).Take(size));
            }
            else
            {
                result.Add("data", temp_result.Skip((page - 1) * size).ToList());
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
            

            return JsonConvert.SerializeObject(result);
        }
    }
}
