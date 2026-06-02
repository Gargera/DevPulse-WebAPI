using Application.Common;
using Application.DTOs.BlogDTOs;

namespace Application.Interfaces.Services
{
    public interface IBlogService
    {
        public Task<ResponseResult<List<GetBlogDto>>> GetAllBlogsDtosAsync();

        public Task<ResponseResult<GetBlogDto>> GetBlogDtoByIdAsync(int id);

        public Task<ResponseResult<CreateBlogDto>> CreateBlogDtoAsync(CreateBlogDto createBlogDto);

        public Task<ResponseResult<int>> DeleteBlogDtoAsync(int id);

        public Task<ResponseResult<UpdateBlogDto>> UpdateBlogDtoAsync(int id, UpdateBlogDto updateBlogDto);
    }
}