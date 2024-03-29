﻿using System.Linq;
using System.Runtime.InteropServices;
using MovieInterface;
using MovieManagerContext;

namespace MovieManagerImplement
{
    public class MovieDetailDao : IMovieDetailDao
    {

        private MovieDetailContext context;

        public MovieDetailDao(MovieDetailContext context)
        {
            this.context = context;
        }

        public object GetMovieDetailsById(int id, long user_id)
        {
            var new_user_movie = context.userMovies.SingleOrDefault(u => u.user_id == user_id && u.movie_id == id);
            var like = false;
            if (new_user_movie != null)
            {
                like = true;
            }
            var result = from details in context.details
                         join movies in context.movies on details.id equals movies.id
                         where movies.id == id
                         select new
                         {
                             id = movies.id,
                             img = movies.img,
                             score = movies.score,
                             name = movies.name,
                             duration = movies.duration,
                             category = movies.category,
                             ori_lang = movies.ori_lang,
                             star = movies.star,
                             director = movies.director,
                             release_day = movies.release_day,
                             source = details.source,
                             score_count = details.score_count,
                             release_desc = details.release_desc,
                             version = details.version,
                             description = details.description,
                             eng_name = details.eng_name,
                             average_score = 0,
                             like = like
                         };
            return result.First();
        }
    }

    
}
