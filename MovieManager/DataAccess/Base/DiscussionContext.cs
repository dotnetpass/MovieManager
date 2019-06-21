using Microsoft.EntityFrameworkCore;
using MovieManager.Entities;

namespace MovieManager.DataAccess.Base
{
    public class DiscussionContext : DbContext
    {
        public DiscussionContext(DbContextOptions<DiscussionContext> options) { }

        public DbSet<Discussion> discussions { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Forum> forums { get; set; }
    }
}
