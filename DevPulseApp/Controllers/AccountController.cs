using Application.DTOs.AccountDTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevPulseApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserProfileService _userProfileService;
        public AccountController(IAccountService accountService, IUserProfileService userProfileService)
        {
            _accountService = accountService;
            _userProfileService = userProfileService;
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

            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(new { token = result.Data });
        }

        [Authorize(Roles = "User")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var res = await _userProfileService.UpdateProfileAsync(userId,dto);
            if (!res.IsSuccess) return BadRequest(res.Message); 
            
            return Ok(res.Data);
        }
    }
}