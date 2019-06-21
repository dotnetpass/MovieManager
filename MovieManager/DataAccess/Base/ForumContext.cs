using Microsoft.EntityFrameworkCore;
using MovieManager.Entities;

namespace MovieManager.DataAccess.Base
{
    public class ForumContext:DbContext
    {
        public ForumContext(DbContextOptions<ForumContext> options) : base(options) { }

        public DbSet<Forum> forums { get; set; }
    }
}
