﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieEntity;
using MovieInterface;
using Newtonsoft.Json;

namespace MovieManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DiscussionController : ControllerBase
    {
        private IDiscussionDao dao;

        public DiscussionController(IDiscussionDao dao)
        {
            this.dao = dao;
        }

        [HttpGet("get/forum/{forum_id}")]
        public async Task<ActionResult<object>> GetForumAndDiscussionsByForumId(int forum_id)
        {
            return dao.GetForumAndDiscussion(forum_id);
        }

        [HttpPost("create")]
        public object AddDiscussion(Discussion discussion)
        {
            bool user_state = false;
            int id = -1;
            if (CheckUserState() > 0)
            {
                user_state = true;
                id = dao.AddDiscussion(discussion);
            }
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("user_state", user_state);
            result.Add("id", id);
            return result;
        }

        [HttpDelete()]

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