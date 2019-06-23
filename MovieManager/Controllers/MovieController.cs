using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieEntity;
using MovieInterface;
using Newtonsoft.Json;

namespace MovieManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {

        private IMovieDao dao;

        public MovieController(IMovieDao dao)
        {
            this.dao = dao;
        }

        [HttpGet("get/{sort_item}/{sort_text}/{order_item}/{page}/{size}")]
        public async Task<ActionResult<object>> GetItems(string sort_item, string sort_text, string order_item, int page, int size)
        {
            var result = dao.GetMoviesByPages(page, size,
                dao.GetMoviesOrderedByValue(order_item,
                dao.GetMoviesSortedByValue(sort_item, sort_text)));
            return result;
        }

    }
}