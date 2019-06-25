﻿using PageDLL;
using MovieManagerContext;
using MovieInterface;
using MovieEntity;
using System.Linq;
using Newtonsoft.Json;
using System.Runtime.Loader;
using System.Reflection;
using System.IO;
using System;

namespace MovieManagerImplement
{
    public class MovieDao : IMovieDao
    {
        public MovieContext context;

        public MovieDao(MovieContext context)
        {
            this.context = context;
        }

        public IQueryable<Movie> GetMoviesSortedByValue(string item, string text)
        {
            if (item == "name")
            {
                return context.movies.Where(m => m.name.Contains(text));
            }
            else if (item == "category")
            {
                return context.movies.Where(m => m.category.Contains(text));
            }
            else if (item == "director")
            {
                return context.movies.Where(m => m.director.Contains(text));
            }
            else if (item == "actor")
            {
                return context.movies.Where(m => m.star.Contains(text));
            }
            else
            {
                return context.movies;
            }
        }

        public IQueryable<Movie> GetMoviesOrderedByValue(string item, IQueryable<Movie> movies)
        {
            if (item == "score")
            {
                return movies.OrderByDescending(m => m.score);
            }
            else if (item == "date")
            {
                return movies.OrderByDescending(m => m.release_day);
            }
            else
            {
                return movies;
            }
        }

        public object GetMoviesByPages(int page, int size, IQueryable<Movie> movies)
        {
            string dir = "C:/Users/Stephen/Desktop/MovieManager/dll/PageDLL/PageDLL/bin/DebugPageDLL.dll";
            AssemblyLoadContext.Default.LoadFromAssemblyPath(dir);
            var result = new Page().QueryData(page, size, movies);
            //Dictionary<string, object> result = new Dictionary<string, object>();
            //IEnumerable<Movie> data = null;
            //int count = movies.Count();
            //if (page == 1)
            //{
            //    data =  movies.Take(size);
            //}
            //else if (page * size <= count)
            //{
            //    data =  movies.Skip((page - 1) * size).Take(size);
            //}
            //else
            //{
            //    data = movies.Skip((page - 1) * size).ToList();
            //}
            //int total_page = 0;
            //if (count % size == 0)
            //{
            //    total_page = count / size;
            //}
            //else
            //{
            //    total_page = count / size + 1;
            //}
            //result.Add("page", page);
            //result.Add("totalPage", total_page);
            //result.Add("count", count);
            //result.Add("pageSize", size);
            //result.Add("data", data);

            return JsonConvert.SerializeObject(result);
        }
    }
}
