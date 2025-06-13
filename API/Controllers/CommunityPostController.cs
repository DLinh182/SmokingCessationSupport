using System.Security.Claims;
using BLL.DTOs.RequestDTO;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityPostController(CommunityPostService communityPostService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            try
            {
                var response = await communityPostService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPostAsync([FromBody] CommunityPostRequestDTO request)
        {
            var accountIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (accountIdClaim == null)
                return Unauthorized();

            int accountId = int.Parse(accountIdClaim.Value);
            try
            {
                await communityPostService.AddAsync(request, accountId);
                return Ok("Post added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPostsAsync([FromQuery] string keyword)//url: .....&keyword=abc
        {
            try
            {
                var response = await communityPostService.SearchByNameOrContentAsync(keyword);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("admin/{postId}")] //url: api/CommunityPost/{postId} --> api/CommunityPost/1
        [Authorize(Roles = $"Admin")]
        public async Task<IActionResult> DeletePostForAdminAsync(int postId)
        {
            try
            {
                await communityPostService.DeleteAsync(postId);
                return Ok("Post deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var accountIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (accountIdClaim == null)
                return Unauthorized();

            int accountId = int.Parse(accountIdClaim.Value);
            var post = await communityPostService.GetPostById(postId);
            if(post != null)
            {
                if(accountId == post.AccountId)
                {
                    try
                    {
                        await communityPostService.DeleteAsync(postId);
                        return Ok("Post deleted successfully");
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
            return BadRequest("This post does not belong to you");
        }
    }
}
