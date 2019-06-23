using Microsoft.AspNetCore.Mvc;
using MovieInterface;
using MovieEntity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using UserCheck;

namespace MovieManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private IIndexDao dao;

        public IndexController(IIndexDao dao)
        {
            this.dao = dao;
        }

        [HttpGet]
        public async Task<ActionResult<object>> Index()
        {
            return dao.Index();
        }
    }
}