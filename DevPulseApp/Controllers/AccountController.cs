using Application.Interfaces.Services;
using Application.DTOs.AccountDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

            if (!result.IsSuccess) return Unauthorized(result.Message);

            return Ok(result.Data);
        }

        [Authorize(Roles = "User")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(string userId, UpdateUserDto dto)
        {
            var res = await _userProfileService.UpdateProfileAsync(userId,dto);
            if (!res.IsSuccess) return BadRequest(res.Message); 
            return Ok(res.Data);
        }
    }
}