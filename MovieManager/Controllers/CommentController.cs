using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Entities;
using MovieManager.DataAccess.Interface;
using Newtonsoft.Json;

namespace MovieManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private ICommentDao dao;

        public CommentController(ICommentDao dao)
        {
            this.dao = dao;
        }

        [HttpPost("create")]
        public async Task<ActionResult<object>> CreateComment(Comment comment)
        {
            bool user_state = false;
            int comment_id = -1;
            if (CheckUserState() > 0)
            {
                user_state = true;
                long user_id = long.Parse(Request.Cookies["user"]);
                comment.user_id = user_id;
                comment.time = DateTime.Now;
                comment_id =  dao.CreateComment(comment);
            }
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("user_state", user_state);
            result.Add("comment_id", comment_id);
            return JsonConvert.SerializeObject(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<object> DeleteCommentById(int id)
        {
            bool user_state = false;
            bool delete_state = false;
            if (CheckUserState() > 0)
            {
                user_state = true;
                delete_state =  dao.DeleteCommentById(id);
            }
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("user_state", user_state);
            result.Add("delete_state", delete_state);
            return JsonConvert.SerializeObject(result);
            
        }

        [HttpGet("get/user")]
        public async Task<ActionResult<object>> GetCommentByUserId()
        {
            bool user_state = false;
            IEnumerable<Comment> comments = null;
            if (CheckUserState() > 0)
            {
                user_state = true;
                comments = dao.GetCommentsByUserId(long.Parse(Request.Cookies["user"]));
            }
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("user_state", user_state);
            result.Add("comments", comments);
            return result;
        }

        [HttpGet("get/movie/{id}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentByMovieId(int id)
        {
            return new ActionResult<IEnumerable<Comment>>(dao.GetCommentsByMovieId(id));
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<object>> UpdateCommentById(int id, Comment comment)
        {
            bool user_state = false;
            bool modified = false;
            if (CheckUserState() > 0)
            {
                user_state = true;
                comment.time = DateTime.Now;
                modified = dao.UpdateCommentById(id, comment);
            }
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("user_state", user_state);
            result.Add("modified", modified);
            return result;
            
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