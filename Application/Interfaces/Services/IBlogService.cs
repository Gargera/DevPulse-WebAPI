using Domain.Common;
using Application.DTOs.BlogDTOs;

namespace Application.Interfaces.Services
{
    public interface IBlogService
    {
        public Task<ResponseResult<List<GetBlogDto>>> GetAllBlogsAsync();

        public Task<ResponseResult<GetBlogDto>> GetBlogByIdAsync(int id);

        public Task<ResponseResult<CreateBlogDto>> CreateBlogAsync(CreateBlogDto createBlogDto, string userId);

        public Task<ResponseResult<int>> DeleteBlogAsync(int id, string userId);

        public Task<ResponseResult<UpdateBlogDto>> UpdateBlogAsync(int id, UpdateBlogDto updateBlogDto, string userId);

        public Task<ResponseResult<List<GetBlogDto>>> GetBlogsByCategoryIdAsync(int categoryId);

        public Task<ResponseResult<List<GetBlogDto>>> GetBlogsByUserIdAsync(string userId);
    }
}