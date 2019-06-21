using System.Collections.Generic;
using System.Linq;
using MovieInterface;
using MovieManagerContext;
using MovieEntity;

namespace MovieManagerImplement
{
    public class CommentDao : ICommentDao
    {
        public CommentContext context;

        public CommentDao(CommentContext context)
        {
            this.context = context;
        }

        public int CreateComment(Comment comment)
        {
            context.Add(comment);
            int max_id = context.comments.Max(c => c.id);
            var new_comment = context.comments.SingleOrDefault(c => c.id == max_id);
            context.SaveChanges();
            return new_comment.id;
        }

        public bool DeleteCommentById(int id)
        {
            var comment = context.comments.SingleOrDefault(c => c.id == id);
            if (comment != null)
            {
                context.Remove(comment);
                return context.SaveChanges() > 0;
            }
            return false;

        }

        public IEnumerable<Comment> GetCommentsByUserId(long id)
        {
            return context.comments.OrderByDescending(c => c.time).Where(c => c.user_id == id);
        }

        public IEnumerable<Comment> GetCommentsByMovieId(int id)
        {
            return context.comments.OrderByDescending(c => c.time).Where(c => c.movie_id == id);
        }

        public bool UpdateCommentById(int id, Comment comment)
        {
            Comment new_comment = context.comments.SingleOrDefault(c => c.id == id);
            if (comment != null)
            {
                new_comment.id = comment.id;
                new_comment.score = comment.score;
                new_comment.time = comment.time;
                new_comment.content = comment.content;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
