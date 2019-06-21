using Microsoft.EntityFrameworkCore;
using MovieEntity;


namespace MovieManagerContext
{
    public class MovieDetailContext : DbContext
    {
        public MovieDetailContext(DbContextOptions<MovieDetailContext> options) : base(options) { }

        public DbSet<Movie> movies { get; set; }
        public DbSet<MovieDetail> details { get; set; }
    }
}
