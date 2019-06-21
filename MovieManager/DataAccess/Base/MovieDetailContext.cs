using Microsoft.EntityFrameworkCore;
using MovieManager.Entities;

namespace MovieManager.DataAccess.Base
{
    public class MovieDetailContext : DbContext
    {
        public MovieDetailContext(DbContextOptions<MovieDetailContext> options) : base(options) { }

        public DbSet<Movie> movies { get; set; }
        public DbSet<MovieDetail> details { get; set; }
    }
}
