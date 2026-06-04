using AutoMapper;
using Domain.Entities;
using Domain.Common;
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

        public async Task<ResponseResult<List<GetCategoryWithoutBlogsDto>>> GetAllCategoriesAsync()
        {
            var result = await _unitOfWork.Categories.GetAllEntitiesAsync();
            var mappedResult = _mapper.Map<List<GetCategoryWithoutBlogsDto>>(result);

            return new ResponseResult<List<GetCategoryWithoutBlogsDto>>
            (
                true,
                null,
                mappedResult
            );
        }

        public async Task<ResponseResult<GetCategoryDto>> GetCategoryByIdAsync(int id)
        {
            var result = await _unitOfWork.Categories.GetEntityByIdAsync(id, c => c.Blogs);
            
            if(result != null)
            {
                var mappedResult = _mapper.Map<GetCategoryDto>(result);
                return new ResponseResult<GetCategoryDto>
                (
                    true,
                    null,
                    mappedResult
                );
            }
            else
            {
                return new ResponseResult<GetCategoryDto>
                (
                    false,
                    "Category not found.",
                    null
                );
            }
        }

        public async Task<ResponseResult<CreateCategoryDto>> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = await _unitOfWork.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == createCategoryDto.Name.ToLower());

            if(category == null)
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
            else
            {
                return new ResponseResult<CreateCategoryDto>
                (
                    false,
                    "Category with the same name already exists.",
                    createCategoryDto
                );
            }
        }

        public async Task<ResponseResult<int>> DeleteCategoryAsync(int id)
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

        public async Task<ResponseResult<UpdateCategoryDto>> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            var result = await _unitOfWork.Categories.GetEntityByIdAsync(id);
            if (result != null)
            {
                var category = await _unitOfWork.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == updateCategoryDto.Name.ToLower() && c.Id != id);

                if (category == null)
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
                        "Category with the same name already exists.",
                        updateCategoryDto
                    );
                }
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
