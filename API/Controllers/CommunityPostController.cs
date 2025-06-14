using System.Security.Claims;
using BLL.DTOs.RequestDTO;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityPostController(CommunityPostService communityPostService) : ControllerBase
    {
        [HttpGet("get")]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            try
            {
                var response = await communityPostService.GetAllCommunityPost();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("up")]
        public async Task<IActionResult> AddPostAsync([FromBody] CommunityPostRequestDTO request)
        {
            var accountIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (accountIdClaim == null)
                return Unauthorized();

            int accountId = int.Parse(accountIdClaim.Value);
            try
            {
                await communityPostService.AddCommunityPost(request, accountId);
                return Ok("Post added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> SearchPostsAsync([FromQuery] string keyword)//url: .....&keyword=abc
        {
            try
            {
                var response = await communityPostService.SearchCommunityPostByNameOrContent(keyword);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{postId}")]
        [Authorize]
        public async Task<IActionResult> DeletePostAsync(int postId)
        {
            var accountIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (accountIdClaim == null)
                return Unauthorized();

            if (!int.TryParse(accountIdClaim.Value, out int accountId))
                return Unauthorized();

            bool isAdmin = User.IsInRole("Admin");

            bool result = await communityPostService.DeleteCommunityPostAsync(postId, accountId, isAdmin);
            if (result)
                return Ok("Post and all related comments deleted successfully");
            else
                return NotFound("Post not found or you do not have permission to delete it");
        }
    }
}
