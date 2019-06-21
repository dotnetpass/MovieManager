﻿using Microsoft.EntityFrameworkCore;
using MovieManager.Entities;

namespace MovieManager.DataAccess.Base
{
    public class UserMovieContext : DbContext
    {

        public UserMovieContext(DbContextOptions<UserMovieContext> options) : base(options) {}

        public DbSet<Movie> movies { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<UserMovie> userMovies { get; set; }
    }
}
