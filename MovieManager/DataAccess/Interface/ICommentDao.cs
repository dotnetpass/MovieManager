using System.Collections.Generic;
using MovieManager.Entities;


namespace MovieManager.DataAccess.Interface
{
    public interface ICommentDao
    {
        int CreateComment(Comment comment);

        bool DeleteCommentById(int id);

        bool UpdateCommentById(int id, Comment comment);

        IEnumerable<Comment> GetCommentsByUserId(long id);

        IEnumerable<Comment> GetCommentsByMovieId(int id);

    }
}
