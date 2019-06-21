using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieEntity;
using MovieInterface;

namespace MovieManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserMovieController : ControllerBase
    {

        private IUserMovieDao dao;

        public UserMovieController(IUserMovieDao dao)
        {
            this.dao = dao;
        }

        [HttpGet("add/{movie_id}")]
        public bool AddMovie(int movie_id)
        {
            return dao.AddUserMovie(new UserMovie(1, movie_id));
        }

        [HttpDelete("delete/{movie_id}")]
        public bool DeleteMovie(int movie_id)
        {
            return dao.DeleteUserMovie(1, movie_id);
        }

        [HttpGet("get/favorite")]
        public async Task<ActionResult<IEnumerable<object>>> GetFavorateMovies()
        {
            return new ActionResult<IEnumerable<object>>(dao.GetUserMovie(1));
        }

    }
}