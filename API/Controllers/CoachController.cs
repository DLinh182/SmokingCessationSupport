using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BLL.Services;
using BLL.DTOs.RequestDTO;
using BLL.DTOs.ResponseDTO;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class CoachController : ControllerBase
{
    private readonly CoachService _coachService;
    public CoachController(CoachService coachService)
    {
        _coachService = coachService;
    }

    [HttpPost]
    public async Task<ActionResult<CoachResponseDTO>> CreateCoach([FromBody] CoachCreateRequestDTO dto)
    {
        var result = await _coachService.CreateCoachAsync(dto);
        if (result == null) return BadRequest("Tạo coach thất bại");
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<CoachResponseDTO>>> GetAllCoach()
    {
        var result = await _coachService.GetAllCoachAsync();
        return Ok(result);
    }

    [HttpPut("status/{accountId}")]
    public async Task<IActionResult> UpdateCoachStatus(int accountId, [FromQuery] bool status)
    {
        var success = await _coachService.UpdateCoachStatusAsync(accountId, status);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<CoachResponseDTO>>> SearchCoach([FromQuery] string keyword)
    {
        var result = await _coachService.SearchCoachAsync(keyword);
        return Ok(result);
    }
} 