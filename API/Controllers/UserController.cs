using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using BLL.DTOs.ResponseDTO;
using BLL.DTOs.RequestDTO;

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

    [HttpGet("{accountId}")]
    public async Task<ActionResult<UserProfileResponseDTO>> GetProfile(int accountId)
    {
        var result = await _userService.GetUserProfile(accountId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPut("{accountId}")]
    public async Task<IActionResult> UpdateProfile(int accountId, [FromBody] UserProfileUpdateRequestDTO dto)
    {
        var success = await _userService.UpdateUserProfile(accountId, dto);
        if (!success) return NotFound();
        return NoContent();
    }
} 