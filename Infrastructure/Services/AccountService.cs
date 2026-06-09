using Application.DTOs.AccountDTOs;
using Application.DTOs.BlogDTOs;
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
        private readonly IFileStorageService _fileStorageService;
        private readonly IJwtService _jwtService;
        public AccountService(UserManager<ApplicationUser> userManager, IJwtService jwtService, IFileStorageService fileStorageService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _fileStorageService = fileStorageService;
        }

        public async Task<ResponseResult<RegisterDto>> RegisterAsync(RegisterDto registerDto)
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
                return new ResponseResult<RegisterDto>
                (
                    false,
                    "Email already exists",
                    registerDto
                );
            }

            var existingUserName = await _userManager.FindByNameAsync(registerDto.UserName);
            if (existingUserName != null)
            {
                return new ResponseResult<RegisterDto>
                (
                    false,
                    "UserName already exists",
                    registerDto
                );
            }

            if(registerDto.Image != null)
            {
                var res = await _fileStorageService.SaveFileAsync(registerDto.Image, "Users");
                if (!res.IsSuccess)
                {
                    return new ResponseResult<RegisterDto>
                    (
                        res.IsSuccess,
                        res.Message,
                        registerDto
                    );
                }

                user.ImageUrl = res.Data;
            }

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            await _userManager.AddToRoleAsync(user, "User");

            return new ResponseResult<RegisterDto>
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