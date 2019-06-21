using Microsoft.EntityFrameworkCore;
using MovieManager.Entities;

namespace MovieManager.DataAccess.Base
{
    public class UserForumContext :DbContext
    {

        public UserForumContext(DbContextOptions<UserForumContext> options) : base(options) { }

        public DbSet<UserForum> userForums { get; set; }
        public DbSet<Forum> forums { get; set; }

    }
}
