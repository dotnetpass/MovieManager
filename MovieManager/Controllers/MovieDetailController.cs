using Microsoft.AspNetCore.Mvc;
using MovieManager.DataAccess.Interface;
using MovieManager.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using Newtonsoft.Json.Linq;

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