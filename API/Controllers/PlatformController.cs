using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.DTOs.RequestDTO;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController(PlatformService platformService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPlatformsAsync()
        {
            try
            {
                var response = await platformService.GetPlatform();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = $"Admin")]
        public async Task<IActionResult> UpdatePlatformAsync([FromBody] PlatformUpdateRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }
            try
            {
                await platformService.UpdatePlatformAsync(request);
                return Ok("Platform updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating platform: {ex.Message}");
            }
        }
    }
}
