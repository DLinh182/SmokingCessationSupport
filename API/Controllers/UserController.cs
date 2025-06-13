using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using BLL.DTOs.ResponseDTO;
using BLL.DTOs.RequestDTO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    public UserController()
    {
        _userService = new UserService();
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<ActionResult<UserProfileResponseDTO>> GetProfile()
    {
        var accountId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _userService.GetUserProfile(accountId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [Authorize]
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UserProfileUpdateRequestDTO dto)
    {
        var accountId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var success = await _userService.UpdateUserProfile(accountId, dto);
        if (!success) return NotFound();
        return NoContent();
    }
} 