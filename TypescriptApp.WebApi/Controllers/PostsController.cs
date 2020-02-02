using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TypescriptApp.Logics;
using TypescriptApp.Logics.Models;

namespace TypescriptApp.WebApi.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        [HttpGet("list")]
        public async Task<IReadOnlyCollection<PostModel>> GetPosts()
        {
            var @operator = new PostsOperator();
            return await @operator.GetPostsAsync()
                .ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<PostModel> GetPost([FromRoute] int id)
        {
            var @operator = new PostsOperator();
            return await @operator.GetPostAsync(id)
                .ConfigureAwait(false);
        }

        [HttpPost]
        public async Task SavePost([FromBody] PostModel post)
        {
            var @operator = new PostsOperator();
            await @operator.SavePostAsync(post)
                .ConfigureAwait(false);
        }
    }
}