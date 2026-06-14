using Application.DTOs.AccountDTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevPulseApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public AdminController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;   
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = _userManager.Users.ToList();
            var mappedResult = new List<GetUserAdminDto>();
            foreach(var user in users)
            {
                var mappedUser = _mapper.Map<GetUserAdminDto>(user);
                mappedUser.Roles = await _userManager.GetRolesAsync(user);
                mappedResult.Add(mappedUser);
            }

            return Ok(mappedResult);
        }
    }
}