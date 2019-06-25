using Microsoft.AspNetCore.Mvc;
using MovieInterface;
using System.Threading.Tasks;

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