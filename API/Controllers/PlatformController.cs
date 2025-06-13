using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using BLL.DTOs.ResponseDTO;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformController : ControllerBase
{
    private readonly PlatformService _platformService;
    public PlatformController()
    {
        _platformService = new PlatformService();
    }

    [HttpGet]
    public async Task<ActionResult<List<PlatformResponseDTO>>> GetAll()
    {
        var result = await _platformService.GetAllPlatformsAsync();
        return Ok(result);
    }
} 