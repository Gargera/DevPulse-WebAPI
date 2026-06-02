using Application.Common;
using Application.DTOs.CategoryDTOs;

namespace Application.Interfaces.Services
{
    public interface ICategoryService
    {
        public Task<ResponseResult<List<GetCategoryDto>>> GetAllCategoriesDtosAsync();

        public Task<ResponseResult<GetCategoryDto>> GetCategoryDtoByIdAsync(int id);

        public Task<ResponseResult<CreateCategoryDto>> CreateCategoryDtoAsync(CreateCategoryDto createCategoryDto);

        public Task<ResponseResult<int>> DeleteCategoryDtoAsync(int id);

        public Task<ResponseResult<UpdateCategoryDto>> UpdateCategoryDtoAsync(int id, UpdateCategoryDto updateCategoryDto);
    }
}
