using Application.Common;
using Application.DTOs.BlogDTOs;

namespace Application.Interfaces.Services
{
    public interface IBlogService
    {
        public Task<ResponseResult<List<GetBlogDto>>> GetAllBlogsAsync();

        public Task<ResponseResult<GetBlogDto>> GetBlogByIdAsync(int id);

        public Task<ResponseResult<CreateBlogDto>> CreateBlogAsync(CreateBlogDto createBlogDto);

        public Task<ResponseResult<int>> DeleteBlogAsync(int id);

        public Task<ResponseResult<UpdateBlogDto>> UpdateBlogAsync(int id, UpdateBlogDto updateBlogDto);

        public Task<ResponseResult<List<GetBlogDto>>> GetBlogsByCategoryIdAsync(int categoryId);

        public Task<ResponseResult<List<GetBlogDto>>> GetBlogsByUserIdAsync(string userId);
    }
}