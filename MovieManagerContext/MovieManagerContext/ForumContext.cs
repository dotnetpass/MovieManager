﻿using Microsoft.EntityFrameworkCore;
using MovieEntity;

namespace MovieManagerContext
{
    public class ForumContext : DbContext
    {
        public ForumContext(DbContextOptions<ForumContext> options) : base(options) { }

        public DbSet<Forum> forums { get; set; }
    }
}