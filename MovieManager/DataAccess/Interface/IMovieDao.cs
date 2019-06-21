using MovieManager.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MovieManager.DataAccess.Interface
{
    public interface IMovieDao
    {
        IQueryable<Movie> GetMoviesSortedByValue(string item, string text);

        IQueryable<Movie> GetMoviesOrderedByValue(string item, IQueryable<Movie> movies);
        
        IEnumerable<Movie> GetMoviesByPages(int page, int size, IQueryable<Movie> movies);

    }
}
