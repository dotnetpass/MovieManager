using System.Collections.Generic;
using MovieManagerContext;
using MovieInterface;
using MovieEntity;
using System.Linq;

namespace MovieManagerImplement
{
    public class MovieDao : IMovieDao
    {
        public MovieContext context;

        public MovieDao(MovieContext context)
        {
            this.context = context;
        }

        public IQueryable<Movie> GetMoviesSortedByValue(string item, string text)
        {
            if (item == "name")
            {
                return context.movies.Where(m => m.name.Contains(text));
            }
            else if (item == "category")
            {
                return context.movies.Where(m => m.category.Contains(text));
            }
            else if (item == "director")
            {
                return context.movies.Where(m => m.director.Contains(text));
            }
            else if (item == "actor")
            {
                return context.movies.Where(m => m.star.Contains(text));
            }
            else
            {
                return context.movies;
            }
        }

        public IQueryable<Movie> GetMoviesOrderedByValue(string item, IQueryable<Movie> movies)
        {
            if (item == "score")
            {
                return movies.OrderByDescending(m => m.score);
            }
            else if (item == "date")
            {
                return movies.OrderByDescending(m => m.release_day);
            }
            else
            {
                return movies;
            }
        }

        public IEnumerable<Movie> GetMoviesByPages(int page, int size, IQueryable<Movie> movies)
        {
            int count = movies.Count();
            if (page == 1)
            {
                return movies.Take(size);
            }
            else if (page * size <= count)
            {
                return movies.Skip((page - 1) * size).Take(size);
            }
            else
            {
                return movies.Skip((page - 1) * size).ToList();
            }
        }
    }
}
