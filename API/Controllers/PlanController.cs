using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BLL.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Member,Admin,Coach")]
public class PlanController : ControllerBase
{
    private readonly PlanService _planService;
    private readonly MemberService _memberService;
    public PlanController(PlanService planService, MemberService memberService)
    {
        _planService = planService;
        _memberService = memberService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlan()
    {
        var accountId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var plan = await _planService.GetPlanByAccountId(accountId);
        if (plan == null) return NotFound();
        return Ok(plan);
    }

    [HttpGet("is-failed")]
    public async Task<IActionResult> IsPlanFailed()
    {
        var accountId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var failed = await _planService.IsPlanFailed(accountId);
        return Ok(new { isFailed = failed });
    }

    [Authorize(Roles = "Member")]
    [HttpPut("form")]
    public async Task<IActionResult> UpdateMemberForm([FromBody] BLL.DTOs.RequestDTO.MemberFormRequestDTO dto)
    {
        var accountId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var success = await _memberService.UpdateMemberForm(accountId, dto);
        if (!success) return NotFound();
        return Ok("Cập nhật thông tin thành công");
    }
} 