using System.Collections.Generic;
using MovieEntity;

namespace MovieInterface
{
    public interface ICommentDao
    {
        int CreateComment(Comment comment);

        bool DeleteCommentById(int id);

        bool UpdateCommentById(int id, Comment comment);

        IEnumerable<Comment> GetCommentsByUserId(long id);

        object GetCommentsByMovieId(int id, int page, int size);

        /*double GetMeanScoreByMovieId(int id);*/

    }
}
