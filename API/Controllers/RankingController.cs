using System.Security.Claims;
using BLL.DTOs.ResponseDTO;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingController(RankingService _rankingService) : ControllerBase
    {
        // GET: api/Ranking
        [HttpGet]
        public async Task<IActionResult> GetAllRankings()
        {
            try
            {
                var rankings = await _rankingService.GetAllAsync();
                return Ok(rankings);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyRanking()
        {
            var accountIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (accountIdClaim == null)
                return Unauthorized();

            int accountId = int.Parse(accountIdClaim.Value);
            var ranking = await _rankingService.GetByAccountIdAsync(accountId);
            if (ranking == null) return NotFound();
            return Ok(ranking);

        }
    }
}
