using MovieInterface;
using MovieEntity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
            return dao.GetMovieDetailsById(id);
        }
        
    }
}