using Domain.Common;
using Application.DTOs.CategoryDTOs;

namespace Application.Interfaces.Services
{
    public interface ICategoryService
    {
        public Task<ResponseResult<List<GetCategoryDto>>> GetAllCategoriesAsync();

        public Task<ResponseResult<GetCategoryDto>> GetCategoryByIdAsync(int id);

        public Task<ResponseResult<CreateCategoryDto>> CreateCategoryAsync(CreateCategoryDto createCategoryDto);

        public Task<ResponseResult<int>> DeleteCategoryAsync(int id);

        public Task<ResponseResult<UpdateCategoryDto>> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto);
    }
}
