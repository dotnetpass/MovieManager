using System.Collections.Generic;
using System.Linq;
using MovieInterface;
using MovieManagerContext;
using MovieEntity;
using System.Runtime.InteropServices;

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

        public object GetCommentsByUserId(long id, int page, int size)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            var comments = context.comments.OrderByDescending(c => c.time).Where(c => c.user_id == id);
            IQueryable<Comment> data = null;
            int count = comments.Count();
            if (page == 1)
            {
                data = comments.Take(size);
            }
            else if (page * size <= count)
            {
                data = comments.Skip((page - 1) * size).Take(size);
            }
            else
            {
                data = comments.Skip((page - 1) * size);
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
            return result;
        }

        public object GetCommentsByMovieId(int id, int page, int size)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            var comments = context.comments.OrderByDescending(c => c.time).Where(c => c.movie_id == id);
            var new_data = from _comments in comments
                           join _users in context.users on _comments.user_id equals _users.id
                           select new
                           {
                               id = _comments.id,
                               user_id = _comments.user_id,
                               movie_id = _comments.movie_id,
                               content = _comments.content,
                               time = _comments.time,
                               score = _comments.score,
                               nick = _users.nick,
                               avatar_url = _users.avatar_url
                           };
            IQueryable<object> data = null;
            int count = comments.Count();
            if (page == 1)
            {
                data = new_data.Take(size);
            }
            else if (page * size <= count)
            {
                data = new_data.Skip((page - 1) * size).Take(size);
            }
            else
            {
                data = new_data.Skip((page - 1) * size);
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
            return result;
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

        //public double GetMeanScoreByMovieId(int id)
        //{
        //    int[] scores = context.comments.Where(c => c.movie_id == id).Select(c => c.score).ToArray();
        //    double[] temp_scores = new double[scores.Length];
        //    for (int i = 0; i < scores.Length; i++)
        //    {
        //        temp_scores[i] = scores[i];
        //    }
        //    return RefCppDll.CalculateMeanScore(ref temp_scores[0], temp_scores.Length);
        //}
    }

    //public class RefCppDll
    //{
    //    [DllImport("MovieScoreDLL.dll", EntryPoint = "CalculateMeanScore", CallingConvention = CallingConvention.Cdecl)]
    //    public extern static double CalculateMeanScore(ref double arr, int len);
    //}
}
