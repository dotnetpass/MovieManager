using System.Collections.Generic;
using MovieManager.Entities;

namespace MovieManager.DataAccess.Interface
{
    public interface IDiscussionDao
    {

        object GetForumAndDiscussion(int forum_id);

        int AddDiscussion(Discussion discussion);

        bool DeleteDiscussion(int discussion_id);

    }
}
