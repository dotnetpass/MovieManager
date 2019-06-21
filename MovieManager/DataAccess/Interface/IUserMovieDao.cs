using MovieManager.Entities;
using System.Collections.Generic;

namespace MovieManager.DataAccess.Interface
{
    public interface IUserMovieDao
    {
        bool AddUserMovie(UserMovie userMovie);

        bool DeleteUserMovie(long user_id, int movie_id);

        IEnumerable<UserMovie> GetUserMoviePairs(long user_id);

        IEnumerable<object> GetUserMovie(long user_id);
    }
}
