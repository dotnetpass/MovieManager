using Microsoft.EntityFrameworkCore;
using MovieManager.Entities;

namespace MovieManager.DataAccess.Base
{
    public class CommentContext:DbContext
    {
        public CommentContext(DbContextOptions<CommentContext> options) : base(options) {}

        public DbSet<Comment> comments { get; set; }
    }
}
