using MovieInterface;
using MovieEntity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserCheck;

namespace MovieManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieDetailController : ControllerBase
    {

        public IMovieDetailDao dao;

        public MovieDetailController(IMovieDetailDao dao)
        {
            this.dao = dao;
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<object>> GetMovieDetailById(int id)
        {
            if (Check.CheckUserState(Request, HttpContext) > 0)
            {
                var user_id = long.Parse(Request.Cookies["user"]);
                return dao.GetMovieDetailsById(id, user_id);
            }
            else
            {
                Response.StatusCode = 403;
                return -1;
            }
            
        }
        
    }
}