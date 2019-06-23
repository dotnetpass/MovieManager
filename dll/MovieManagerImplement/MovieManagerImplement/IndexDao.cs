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
            //var movies = context.movies.OrderByDescending(m => m.release_day).Take(12);
            //var comments = from _comments in context.comments
            //               join _movies in context.movies on _comments.movie_id equals _movies.id
            //               join _users in context.users on _comments.user_id equals _users.id
            //               select new
            //               {

            //               }
            return null;
        }

    }
}
