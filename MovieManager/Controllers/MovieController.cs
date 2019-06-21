using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Entities;
using MovieManager.DataAccess.Interface;
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
        public async Task<ActionResult<IEnumerable<Movie>>> GetItems(string sort_item, string sort_text, string order_item, int page, int size)
        {
            return new ActionResult<IEnumerable<Movie>>(dao.GetMoviesByPages(page, size,
                dao.GetMoviesOrderedByValue(order_item,
                dao.GetMoviesSortedByValue(sort_item, sort_text))));
        }

    }
}