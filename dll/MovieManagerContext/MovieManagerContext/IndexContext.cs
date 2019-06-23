using Microsoft.EntityFrameworkCore;
using MovieEntity;

namespace MovieManagerContext
{
    public class IndexContext : DbContext
    {
        public IndexContext(DbContextOptions<IndexContext> options) : base(options) { }

        public DbSet<Movie> movies;
        public DbSet<Comment> comments;
        public DbSet<Discussion> discussions;
        public DbSet<User> users;
    }
}
