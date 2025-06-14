using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BLL.Services;
using BLL.DTOs.ResponseDTO;
using BLL.DTOs.RequestDTO;
using System.Security.Claims;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Coach")]
public class MemberController : ControllerBase
{
    private readonly MemberService _memberService;
    public MemberController(MemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MemberResponseDTO>>> GetAllMember()
    {
        var result = await _memberService.GetAllMemberAsync();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("status/{accountId}")]
    public async Task<IActionResult> UpdateMemberStatus(int accountId, [FromQuery] bool status)
    {
        var success = await _memberService.UpdateMemberStatusAsync(accountId, status);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<MemberResponseDTO>>> SearchMember([FromQuery] string keyword)
    {
        var result = await _memberService.SearchMemberAsync(keyword);
        return Ok(result);
    }
}