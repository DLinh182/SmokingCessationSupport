using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using System.Threading.Tasks;
using BLL.DTOs.RequestDTO;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageMembershipController : ControllerBase
    {
        private readonly PackageMembershipService _service;

        public PackageMembershipController(PackageMembershipService service)
        {
            _service = service;
        }

        // GET: api/PackageMembership
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var packages = await _service.GetAllPackageMembershipsAsync();
            return Ok(packages);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"Admin")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PackageMembershipUpdateRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }
            try
            {
                await _service.UpdateAsync(id, request);
                return Ok("Package membership updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating package membership: {ex.Message}");
            }
        }
    }
}