using Application.DTOs.AccountDTOs;
using Application.DTOs.JwtDTOs;
using Application.Interfaces.Services;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        public AccountService(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
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

        public async Task<ResponseResult<string>> LoginAsync(LoginDto logInDto)
        {
            var user = await _userManager.FindByNameAsync(logInDto.UserName);

            if (user == null)
            {
                return new ResponseResult<string>
                (
                    false,
                    "Invalid email or password.",
                    null
                );
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, logInDto.Password);

            if (!isPasswordValid)
            {
                return new ResponseResult<string>
                (
                    false,
                    "Invalid email or password.",
                    null
                );
            }


            var jwtDto = new JwtDto
            {
                UserId = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                Roles = await _userManager.GetRolesAsync(user)
            };

            return new ResponseResult<string>
            (
                true,
                "User Logged in successfully",
                _jwtService.GenerateToken(jwtDto)
            );
        }
    }
}