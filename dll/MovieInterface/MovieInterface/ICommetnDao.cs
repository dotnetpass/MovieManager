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

        IEnumerable<Comment> GetCommentsByMovieId(int id);

        /*double GetMeanScoreByMovieId(int id);*/

    }
}
