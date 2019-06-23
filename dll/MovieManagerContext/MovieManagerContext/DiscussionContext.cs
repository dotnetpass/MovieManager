using Microsoft.EntityFrameworkCore;
using MovieEntity;

namespace MovieManagerContext
{
    public class DiscussionContext : DbContext
    {
        public DiscussionContext(DbContextOptions<DiscussionContext> options): base(options) { }

        public DbSet<Discussion> discussions { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Forum> forums { get; set; }
    }
}
