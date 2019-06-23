using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MovieManagerContext;
using MovieInterface;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using MovieManagerImplement;
using System.Net;

namespace MovieManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            //添加session
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(24 * 60 * 60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            

            services.AddDbContext<UserContext>(opt =>
                opt.UseMySql(Configuration.GetConnectionString("MovieConnection")));
            services.AddDbContext<CommentContext>(opt =>
                opt.UseMySql(Configuration.GetConnectionString("MovieConnection")));
            services.AddDbContext<MovieContext>(opt =>
                opt.UseMySql(Configuration.GetConnectionString("MovieConnection")));
            services.AddDbContext<UserMovieContext>(opt =>
                opt.UseMySql(Configuration.GetConnectionString("MovieConnection")));
            services.AddDbContext<ForumContext>(opt =>
                opt.UseMySql(Configuration.GetConnectionString("MovieConnection")));
            services.AddDbContext<UserForumContext>(opt =>
                opt.UseMySql(Configuration.GetConnectionString("MovieConnection")));
            services.AddDbContext<MovieDetailContext>(opt =>
                opt.UseMySql(Configuration.GetConnectionString("MovieConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddScoped<IUserDao, UserDao>();
            services.AddScoped<ICommentDao, CommentDao>();
            services.AddScoped<IMovieDao, MovieDao>();
            services.AddScoped<IUserMovieDao, UserMovieDao>();
            services.AddScoped<IForumDao, ForumDao>();
            services.AddScoped<IUserForumDao, UserForumDao>();
            services.AddScoped<IMovieDetailDao, MovieDetailDao>();
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseSession();
            app.UseMvc();
        }
    }
}
