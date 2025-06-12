using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAll()
        {
            var packages = await _service.GetAllPackageMembershipsAsync();
            return Ok(packages);
        }
    }
}