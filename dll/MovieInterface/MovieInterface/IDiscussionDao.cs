using MovieEntity;

namespace MovieInterface
{
    public interface IDiscussionDao
    {

        object GetForumAndDiscussion(int forum_id, int page, int size);

        int AddDiscussion(Discussion discussion);

        bool DeleteDiscussion(int discussion_id);

    }
}
