using Microsoft.AspNetCore.Mvc;
using MovieInterface;
using MovieEntity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

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
        [HttpGet("get/all")]
        public async Task<ActionResult<IEnumerable<Forum>>> GetAllForums()
        {
            return new ActionResult<IEnumerable<Forum>>(dao.GetAllForums());
        }

        //根据forum的名字来搜索forum
        [HttpGet("get/name/{name}")]
        public async Task<ActionResult<IEnumerable<Forum>>> GetForumsByName(string name)
        {
            return new ActionResult<IEnumerable<Forum>>(dao.GetForumsByName(name));
        }

        //登陆用户创建新的forum
        [HttpPost("create")]
        public async Task<ActionResult<int>> CreateForum(Forum forum)
        {
            if (CheckUserState() > 0)
            {
                forum.publisher_id = long.Parse(Request.Cookies["user"]);
                return dao.CreateForum(forum);
            }
            else
            {
                return -1;
            }
        }
        
        //登陆用户根据id获取他创建的forum
        [HttpGet("get/user")]
        public async Task<ActionResult<IEnumerable<Forum>>> GetForumsByUserId()
        {
            if (CheckUserState() > 0)
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

        public int CheckUserState()
        {
            if (Request.Cookies.ContainsKey("user"))
            {
                string id = Request.Cookies["user"];
                if (HttpContext.Session.GetString(id) != null)
                {
                    return 1;
                }
                return -1;
            }
            return -1;
        }
    }
}