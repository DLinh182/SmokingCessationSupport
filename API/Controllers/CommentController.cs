using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(CommentService _service) : ControllerBase
    {
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetAllCommentsAsync(int postId)
        {
            try
            {
                var comments = await _service.GetAllCommentsAsync(postId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
