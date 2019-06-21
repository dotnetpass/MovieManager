using System.Linq;
using MovieManager.Entities;
using MovieManager.DataAccess.Interface;
using MovieManager.DataAccess.Base;
using System.Collections.Generic;

namespace MovieManager.DataAccess.Implement
{
    public class UserMovieDao : IUserMovieDao
    {
        public UserMovieContext context;

        public UserMovieDao(UserMovieContext context)
        {
            this.context = context;
        }

        public bool AddUserMovie(UserMovie userMovie)
        {
            var newUserMovie = context.userMovies.Where(u => u.user_id == userMovie.user_id && u.movie_id == userMovie.movie_id);
            if (newUserMovie.Count() > 0)
            {
                return false;
            }
            else
            {
                context.userMovies.Add(userMovie);
                return context.SaveChanges() > 0;
            }
        }

        public bool DeleteUserMovie(long user_id, int movie_id)
        {
            var userMovie = context.userMovies.SingleOrDefault(u => u.user_id == user_id && u.movie_id == movie_id);
            if (userMovie != null)
            {
                context.userMovies.Remove(userMovie);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<object> GetUserMovie(long user_id)
        {
            var result = from userMovies in context.userMovies
                         join movies in context.movies on userMovies.movie_id equals movies.id
                         where userMovies.user_id == user_id
                         select new
                         {
                             id = movies.id,
                             img = movies.img,
                             score = movies.score,
                             name = movies.name,
                             duration = movies.duration,
                             category = movies.category,
                             ori_lang = movies.ori_lang,
                             star = movies.star
                         };
            return result;
        }

        public IEnumerable<UserMovie> GetUserMoviePairs(long user_id)
        {
            return context.userMovies.Where(u => u.user_id == user_id).ToList();
        }
    }
}
