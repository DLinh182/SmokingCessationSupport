using System.Security.Claims;
using BLL.DTOs.RequestDTO;
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

        [HttpPost]
        public async Task<IActionResult> AddCommentAsync(int postId, [FromBody] CommentCreateRequestDTO request)
        {
            var accountIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (accountIdClaim == null)
                return Unauthorized();

            int accountId = int.Parse(accountIdClaim.Value);
            try
            {
                await _service.AddCommentAsync(postId, request, accountId);
                return Ok("Comment added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteCommentAsync(int commentId)
        {
            var accountIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (accountIdClaim == null)
                return Unauthorized();

            int accountId = int.Parse(accountIdClaim.Value);
            bool isAdmin = User.IsInRole("Admin");
            try
            {
                bool result = await _service.DeleteCommentAsync(commentId, accountId, isAdmin);
                if (result)
                    return Ok("Comment deleted successfully");
                else
                    return NotFound("Comment not found or you do not have permission to delete it");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
