using Domain.Common;
using Application.DTOs.AccountDTOs;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResponseResult<string>> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.UserName
            };

            var existingEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingEmail != null)
            {
                return new ResponseResult<string>
                (
                    false,
                    "Email already exists",
                    null
                );
            }

            var existingUserName = await _userManager.FindByNameAsync(registerDto.UserName);
            if (existingUserName != null)
            {
                return new ResponseResult<string>
                (
                    false,
                    "UserName already exists",
                    null
                );
            }

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            await _userManager.AddToRoleAsync(user, "User");

            return new ResponseResult<string>
            (
                true,
                "User registered successfully",
                null
            );
        }

        //public async Task<ResponseResult<string>> LoginAsync(LoginDto dto)
        //{

        //}
    }
}