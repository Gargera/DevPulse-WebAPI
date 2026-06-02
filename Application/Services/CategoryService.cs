using AutoMapper;
using Domain.Entities;
using Application.Common;
using Application.DTOs.CategoryDTOs;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseResult<List<GetCategoryDto>>> GetAllCategoriesDtosAsync()
        {
            var result = await _unitOfWork.Categories.GetAllEntitiesAsync(null, c => c.Blogs);
            var mappedResult = _mapper.Map<List<GetCategoryDto>>(result);

            return new ResponseResult<List<GetCategoryDto>>
            (
                true,
                null,
                mappedResult
            );
        }

        public async Task<ResponseResult<GetCategoryDto>> GetCategoryDtoByIdAsync(int id)
        {
            var result = await _unitOfWork.Categories.GetEntityByIdAsync(id, c => c.Blogs);
            var mappedResult = _mapper.Map<GetCategoryDto>(result);

            return new ResponseResult<GetCategoryDto>
            (
                true,
                null,
                mappedResult
            );
        }

        public async Task<ResponseResult<CreateCategoryDto>> CreateCategoryDtoAsync(CreateCategoryDto createCategoryDto)
        {
            var mappedResult = _mapper.Map<Category>(createCategoryDto);
            await _unitOfWork.Categories.AddEntityAsync(mappedResult);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseResult<CreateCategoryDto>
            (
                true,
                null,
                createCategoryDto
            );
        }

        public async Task<ResponseResult<int>> DeleteCategoryDtoAsync(int id)
        {
            var result = await _unitOfWork.Categories.GetEntityByIdAsync(id);

            if (result != null)
            {
                await _unitOfWork.Categories.DeleteEntityAsync(id);
                await _unitOfWork.SaveChangesAsync();

                return new ResponseResult<int>
                (
                    true,
                    null,
                    id
                );
            }
            else
            {
                return new ResponseResult<int>
                (
                false,
                "Category not found.",
                id
                );
            }
        }

        public async Task<ResponseResult<UpdateCategoryDto>> UpdateCategoryDtoAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            var result = await _unitOfWork.Categories.GetEntityByIdAsync(id);
            if (result != null)
            {
                result.Name = updateCategoryDto.Name;

                _unitOfWork.Categories.UpdateEntity(result);
                await _unitOfWork.SaveChangesAsync();

                return new ResponseResult<UpdateCategoryDto>
                (
                    true,
                    null,
                    updateCategoryDto
                );
            }
            else
            {
                return new ResponseResult<UpdateCategoryDto>
                (
                    false,
                    "Category not found.",
                    updateCategoryDto
                );
            }
        }
    }
}
