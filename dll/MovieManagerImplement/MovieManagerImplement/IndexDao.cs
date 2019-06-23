using System.Collections.Generic;
using MovieManagerContext;
using MovieInterface;
using MovieEntity;
using System.Linq;
using Newtonsoft.Json;


namespace MovieManagerImplement
{
    public class IndexDao :IIndexDao
    {

        private IndexContext context;

        public IndexDao(IndexContext context)
        {
            this.context = context;
        }

        public object Index()
        {
            var movies = context.movies.OrderByDescending(m => m.release_day).Take(12);
            var comments = (from _comments in context.comments
                            join _movies in context.movies on _comments.movie_id equals _movies.id
                            join _users in context.users on _comments.user_id equals _users.id
                            orderby _comments.time descending
                            select new
                            {
                                id = _comments.id,
                                user_id = _comments.user_id,
                                movie_id = _comments.movie_id,
                                content = _comments.content,
                                time = _comments.time,
                                score = _comments.score,
                                movie_name = _movies.name,
                                movie_url = _movies.img,
                                user_name = _users.nick,
                                user_avatar_url = _users.avatar_url
                            }).Take(9);
            var discussions = (from _discussions in context.discussions
                               join _forums in context.forums on _discussions.forum_id equals _forums.id
                               join _users in context.users on _discussions.user_id equals _users.id
                               orderby _discussions.time descending
                               select new
                               {
                                   id = _discussions.id,
                                   content = _discussions.content,
                                   time = _discussions.time,
                                   forum_id = _discussions.forum_id,
                                   user_id = _discussions.user_id,
                                   forum_name = _forums.name,
                                   user_name = _users.nick,
                                   user_avatar_url = _users.avatar_url
                               }).Take(9);
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("movie", movies);
            result.Add("comment", comments);
            result.Add("discussion", discussions);
            
            return JsonConvert.SerializeObject(result);
        }

    }
}
