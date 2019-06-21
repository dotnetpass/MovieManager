using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieManager.DataAccess.Interface;
using MovieManager.DataAccess.Base;
using MovieManager.Entities;
using Newtonsoft.Json;

namespace MovieManager.DataAccess.Implement
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
            var new_discussion = context.discussions.Add(discussion);
            context.SaveChanges();
            return new_discussion.Entity.id;
        }

        public bool DeleteDiscussion(int discussion_id)
        {
            var temp_discussion = context.discussions.SingleOrDefault(d => d.id == discussion_id);
            if (temp_discussion != null)
            {
                context.discussions.Remove(temp_discussion);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public object GetForumAndDiscussion(int forum_id)
        {
            var discussion_result = from discussions in context.discussions
                                    join users in context.users on discussions.user_id equals users.id
                                    where discussions.forum_id == forum_id
                                    select new
                                    {
                                        url = users.avatar_url,
                                        nick = users.nick,
                                        discussion_id = discussions.id,
                                        content = discussions.content,
                                        time = discussions.time,
                                        user_id = discussions.user_id,
                                        forum_id = discussions.forum_id
                                    };
            var forum_result = context.forums.SingleOrDefault(f => f.id == forum_id);
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("discussions", discussion_result);
            result.Add("forum", forum_result);
            return JsonConvert.SerializeObject(result);

        }
    }
}
