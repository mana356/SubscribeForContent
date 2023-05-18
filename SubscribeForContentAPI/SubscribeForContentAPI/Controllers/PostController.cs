using Microsoft.AspNetCore.Mvc;

namespace SubscribeForContentAPI.Controllers
{
    [ApiController]
    [Route("api/v1/Posts")]
    public class PostController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetPostById(int id)
        {
            string[] posts = { "a", "b", "c" };

            return Ok(posts);
        }
    }
}
