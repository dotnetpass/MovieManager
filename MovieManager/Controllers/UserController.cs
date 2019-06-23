using Microsoft.AspNetCore.Mvc;
using MovieInterface;
using MovieEntity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserDao dao;

        public UserController(IUserDao dao)
        {
            this.dao = dao;
        }

        // 创建用户
        [HttpPost("create")]
        public async Task<ActionResult<object>> CreateUser(User user)
        {
            long id = dao.CreateUser(user);
            Dictionary<string, object> result = new Dictionary<string, object>();
            if (id > 0)
            {
                var options = new CookieOptions();
                options.Expires = DateTime.Now.AddSeconds(24 * 60 * 60);
                Response.Cookies.Append("user", id.ToString(), options);
                HttpContext.Session.SetString(id.ToString(), id.ToString());
                result.Add("id", id);
                result.Add("nick", user.nick);
                result.Add("avatar_url", user.avatar_url);
            }
            else
            {
                Response.StatusCode = 400;
                result.Add("error", "该用户已经存在/密码不能为空");
            }

            return JsonConvert.SerializeObject(result);
        }

        //
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<bool>> DeleteUser(long id)
        {
            return dao.DeleteUserById(id);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<bool>> UpdateUser(long id, User user)
        {
            return dao.UpdateUserById(id, user);
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<User>> GetUserById(long id)
        {
            return dao.GetUserById(id);
        }

        [HttpPost("login")]
        public async Task<ActionResult<object>> Login([FromBody]JObject user_information)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            string user_name = user_information["nick"].ToString();
            string password = user_information["password"].ToString();
            long is_login = dao.Login(user_name, password);
            if (is_login < 0)
            {
                Response.StatusCode = 400;
                result.Add("error", "用户名或密码错误");
            }
            else
            {
                var options = new CookieOptions();
                options.Expires = DateTime.Now.AddSeconds(24 * 60 * 60);
                Response.Cookies.Append("user", is_login.ToString(), options);
                HttpContext.Session.SetString(is_login.ToString(), is_login.ToString());
                User user = dao.GetUserById(is_login);
                result.Add("id", is_login.ToString());
                result.Add("nick", user_name);
                result.Add("avatar_url", user.avatar_url);
            }
            return JsonConvert.SerializeObject(result);
        }

        [HttpGet("logout")]
        public string logout()
        {
            string id = Request.Cookies["user"];
            if (id != null)
            {
                Response.Cookies.Delete("user");
                HttpContext.Session.Remove(id);
            }
            return "注销成功！";
        }

       
        public string getState()
        {
            if (Request.Cookies.ContainsKey("user"))
            {
                string id = Request.Cookies["user"];
                if (HttpContext.Session.GetString(id) != null)
                {
                    return "验证成功";
                }
                return "session 未查到";
            }
            return "cookie 未查到";
        }
    }
}
