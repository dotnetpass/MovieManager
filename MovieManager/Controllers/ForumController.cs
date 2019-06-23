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
    public class ForumController : ControllerBase
    {
        private IForumDao dao;

        public ForumController(IForumDao dao)
        {
            this.dao = dao;
        }

        //获取所有的forum
        [HttpGet("get/all/{page}/{size}")]
        public async Task<ActionResult<object>> GetAllForums(int page, int size)
        {
            return dao.GetAllForums(page, size);
        }

        //根据forum的名字来搜索forum
        [HttpGet("get/name/{name}")]
        public async Task<ActionResult<object>> GetForumsByName(string name)
        {
            return dao.GetForumsByName(name);
        }

        [HttpGet("get/id/{id}")]
        public async Task<ActionResult<object>> GetForumById(int id)
        {
            return dao.GetForumById(id);
        }

        //登陆用户创建新的forum
        [HttpPost("create")]
        public async Task<ActionResult<int>> CreateForum(Forum forum)
        {
            if (Check.CheckUserState(Request, HttpContext) > 0)
            {
                forum.publisher_id = long.Parse(Request.Cookies["user"]);
                return dao.CreateForum(forum);
            }
            else
            {
                Response.StatusCode = 403;
                return -1;
            }
        }
        
        //登陆用户根据id获取他创建的forum
        [HttpGet("get/user")]
        public async Task<ActionResult<IEnumerable<Forum>>> GetForumsByUserId()
        {
            if (Check.CheckUserState(Request, HttpContext) > 0)
            {
                return new ActionResult<IEnumerable<Forum>>(dao.GetForumsByUserId(long.Parse(Request.Cookies["user"])));
            } else
            {
                return null;
            }
        }

        //根据forum id删除forum
        [HttpDelete("delete/{forum_id}")]
        public bool DeleteForumById(int forum_id)
        {
            if (Request.Cookies.ContainsKey("user"))
            {
                string id = Request.Cookies["user"];
                return dao.DeleteForumById(forum_id, long.Parse(id));
            }
            return false;
        }
    }
}