using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BLL.Services;
using BLL.DTOs.RequestDTO;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Member,Admin,Coach")]
public class PlanDetailController : ControllerBase
{
    private readonly PlanDetailService _planDetailService;
    public PlanDetailController(PlanDetailService planDetailService)
    {
        _planDetailService = planDetailService;
    }

    [HttpPost]
    public async Task<IActionResult> AddOrUpdatePlanDetail([FromBody] PlanDetailRequestDTO dto)
    {
        var accountId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        try
        {
            var ok = await _planDetailService.AddOrUpdatePlanDetail(accountId, dto.TodayCigarettes, dto.Date);
            if (!ok) return BadRequest("Không thể cập nhật kế hoạch");
            return Ok("Cập nhật thành công");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
} 