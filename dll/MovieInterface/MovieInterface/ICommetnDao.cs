using System.Collections.Generic;
using MovieEntity;

namespace MovieInterface
{
    public interface ICommentDao
    {
        int CreateComment(Comment comment);

        bool DeleteCommentById(int id);

        bool UpdateCommentById(int id, Comment comment);

        object GetCommentsByUserId(long id, int page, int size);

        object GetCommentsByMovieId(int id, int page, int size);

        /*double GetMeanScoreByMovieId(int id);*/

    }
}
