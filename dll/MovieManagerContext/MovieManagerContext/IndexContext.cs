using Microsoft.EntityFrameworkCore;
using MovieEntity;

namespace MovieManagerContext
{
    public class IndexContext : DbContext
    {
        public IndexContext(DbContextOptions<IndexContext> options) : base(options) { }

        public DbSet<Movie> movies { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Discussion> discussions { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Forum> forums { get; set; }
    }
}
