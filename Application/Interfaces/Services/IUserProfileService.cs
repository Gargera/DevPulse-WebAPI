using Application.DTOs.AccountDTOs;
using Domain.Common;

namespace Application.Interfaces.Services
{
    public interface IUserProfileService
    {
        public Task<ResponseResult<GetProfileDto>> GetProfileAsync(string userId);
        public Task<ResponseResult<UpdateUserDto>> UpdateProfileAsync(string userId, UpdateUserDto updateUserDto);
    }
}