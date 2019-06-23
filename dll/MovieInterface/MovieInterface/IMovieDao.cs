using MovieEntity;
using System.Collections.Generic;
using System.Linq;

namespace MovieInterface
{
    public interface IMovieDao
    {
        IQueryable<Movie> GetMoviesSortedByValue(string item, string text);

        IQueryable<Movie> GetMoviesOrderedByValue(string item, IQueryable<Movie> movies);

        object GetMoviesByPages(int page, int size, IQueryable<Movie> movies);

    }
}
