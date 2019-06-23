using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieEntity;
using MovieInterface;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MovieManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserForumController : ControllerBase
    {

        private IUserForumDao dao;

        public UserForumController(IUserForumDao dao)
        {
            this.dao = dao;
        }

        // 用户关注某个forum
        [HttpGet("add/{forum_id}")]
        public object CreateUserForum(int forum_id)
        {
            bool login = false;
            int id = -1;
            if (CheckUserState() > 0)
            {
                login = true;
                var userForum = new UserForum(long.Parse(Request.Cookies["user"]), forum_id);
                id =  dao.CreateUserForum(userForum);
            }
            else
            {
                Response.StatusCode = 403;
            }
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("login_state", login);
            result.Add("id", id);
            return JsonConvert.SerializeObject(result);

        }

        // 根据用户ID搜索关注的所有forum
        [HttpGet("get/all")]
        public async Task<ActionResult<object>> GetAllForumByUserId()
        {
            bool login_state = false;
            IEnumerable<object> data = null;
            if (CheckUserState() > 0)
            {
                login_state = true;
                long user_id = long.Parse(Request.Cookies["user"]);
                data = dao.GetUserForumByUserId(user_id);
            }
            else
            {
                Response.StatusCode = 403;
            }
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("login_state", login_state);
            result.Add("data", data);
            return JsonConvert.SerializeObject(result);
        }

        // 删除特定的forum
        [HttpDelete("delete/{forum_id}")]
        public ActionResult<object> DeleteUserForum(int forum_id)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            bool tag_login = false;
            bool tag = false;
            if (CheckUserState() > 0)
            {
                tag_login = true;
                long user_id = long.Parse(Request.Cookies["user"]);
                tag = dao.DeleteUserForum(user_id, forum_id);
            }
            else
            {
                Response.StatusCode = 403;
            }
            result.Add("login_state", tag_login);
            result.Add("delete_state", tag);
            return JsonConvert.SerializeObject(result);
        }


        // 检查用户状态
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