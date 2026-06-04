using Domain.Common;
using Application.DTOs.AccountDTOs;

namespace Application.Interfaces.Services
{
    public interface IAccountService
    {
        public Task<ResponseResult<string>> RegisterAsync(RegisterDto dto);

        public Task<ResponseResult<string>> LoginAsync(LoginDto dto);
    }
}
