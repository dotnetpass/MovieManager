using Microsoft.EntityFrameworkCore;
using MovieManager.Entities;

namespace MovieManager.DataAccess.Base
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> users { get; set; }
    }
}
