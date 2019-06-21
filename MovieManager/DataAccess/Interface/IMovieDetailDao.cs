using System;
using MovieManager.Entities;
using System.Collections.Generic;


namespace MovieManager.DataAccess.Interface
{
    public interface IMovieDetailDao
    {
        object GetMovieDetailsById(int id);
    }
}
