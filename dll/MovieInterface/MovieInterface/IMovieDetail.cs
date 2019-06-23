using System;
using MovieEntity;
using System.Collections.Generic;

namespace MovieInterface
{
    public interface IMovieDetailDao
    {
        object GetMovieDetailsById(int id, long user_id);
    }
}
