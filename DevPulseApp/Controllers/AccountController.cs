using Application.Interfaces.Services;
using Application.DTOs.AccountDTOs;
using Microsoft.AspNetCore.Mvc;

namespace DevPulseApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _accountService.RegisterAsync(dto);

            if (!result.IsSuccess) return Conflict(result.Message);

            return Created();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _accountService.LoginAsync(dto);

            if (!result.IsSuccess) return Unauthorized(result.Message);

            return Ok(result.Data);
        }
    }
}