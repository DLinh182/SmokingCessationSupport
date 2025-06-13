using BLL.Services;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.Entities;
using BLL.DTOs.RequestDTO;
using BLL.DTOs.ResponseDTO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SmokingCessationContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly AccountService _accountService;

        public AuthController(SmokingCessationContext context, IConfiguration configuration, IEmailService emailService, AccountService accountService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            LoginResponseDTO? response = null;
            try
            {
                response = await _accountService.LoginAync(loginRequest);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(response);
        }
    }
}