using MovieEntity;
using System.Collections.Generic;

namespace MovieInterface
{
    public interface IUserMovieDao
    {
        bool AddUserMovie(UserMovie userMovie);

        bool DeleteUserMovie(long user_id, int movie_id);

        IEnumerable<UserMovie> GetUserMoviePairs(long user_id);

        IEnumerable<object> GetUserMovie(long user_id);
    }
}
