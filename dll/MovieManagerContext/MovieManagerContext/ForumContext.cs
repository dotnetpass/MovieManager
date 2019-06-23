using Microsoft.EntityFrameworkCore;
using MovieEntity;

namespace MovieManagerContext
{
    public class ForumContext : DbContext
    {
        public ForumContext(DbContextOptions<ForumContext> options) : base(options) { }

        public DbSet<Forum> forums { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<UserForum> userForums { get; set; }
    }
}
