using Application.DTOs.AccountDTOs;
using Application.Interfaces.Services;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileStorageService _fileStorageService;
        public UserProfileService(UserManager<ApplicationUser> userManager, IFileStorageService fileStorageService)
        {
            _userManager = userManager;
            _fileStorageService = fileStorageService;
        }

        public async Task<ResponseResult<GetUserDto>> GetProfileAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
            {
                return new ResponseResult<GetUserDto>
                (
                    false,
                    "User not found.",
                    null
                );
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new ResponseResult<GetUserDto>
            (
                true,
                null,
                new GetUserDto
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName!,
                    Email = user.Email!,
                    ImageUrl = user.ImageUrl,
                    Roles = roles
                }
            );
        }
        public async Task<ResponseResult<UpdateUserDto>> UpdateProfileAsync(string userId, UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return new ResponseResult<UpdateUserDto>
                (
                    false,
                    "User not found.",
                    updateUserDto
                );
            }

            user.FirstName = updateUserDto.FirstName.Trim();
            user.LastName = updateUserDto.LastName.Trim();
            if (updateUserDto.Image != null)
            {
                if (user.ImageUrl != null) _fileStorageService.DeleteFile(user.ImageUrl);
                var res = await _fileStorageService.SaveFileAsync(updateUserDto.Image, "Users");
                if (!res.IsSuccess)
                {
                    return new ResponseResult<UpdateUserDto>
                    (
                        res.IsSuccess,
                        res.Message,
                        updateUserDto
                    );
                }

                user.ImageUrl = res.Data;
            }

            var result = await _userManager.UpdateAsync(user);

            return new ResponseResult<UpdateUserDto>
            (
                true,
                "Profile updated successfully.",
                null
            );
        }
    }
}