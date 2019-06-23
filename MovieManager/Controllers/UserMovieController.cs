using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieEntity;
using MovieInterface;
using Newtonsoft.Json;
using UserCheck;

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
        public async Task<ActionResult<object>> AddMovie(int movie_id)
        {
            bool login = false;
            bool add_state = false;
            if (Check.CheckUserState(Request, HttpContext) > 0)
            {
                login = true;
                var user_movie = new UserMovie(long.Parse(Request.Cookies["user"]), movie_id);
                add_state = dao.AddUserMovie(user_movie);
            }
            else
            {
                Response.StatusCode = 403;
            }
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("login_state", login);
            result.Add("add_state", add_state);
            return JsonConvert.SerializeObject(result);
        }

        [HttpDelete("delete/{movie_id}")]
        public async Task<ActionResult<object>> DeleteMovie(int movie_id)
        {
            bool login = false;
            bool delete_state = false;
            if (Check.CheckUserState(Request, HttpContext) > 0)
            {
                login = true;
                delete_state = dao.DeleteUserMovie(long.Parse(Request.Cookies["user"]), movie_id);
            }
            else
            {
                Response.StatusCode = 403;
            }
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("login_state", login);
            result.Add("delete_state", delete_state);
            return JsonConvert.SerializeObject(result);
        }

        [HttpGet("get/favorite")]
        public async Task<ActionResult<object>> GetFavorateMovies()
        {
            bool login = false;
            IEnumerable<object> data = null;
            if (Check.CheckUserState(Request, HttpContext) > 0)
            {
                login = true;
                data = dao.GetUserMovie(long.Parse(Request.Cookies["user"]));
            }
            else
            {
                Response.StatusCode = 403;
            }
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("login_state", login);
            result.Add("delete_state", data);
            return result;
        }
    }
}