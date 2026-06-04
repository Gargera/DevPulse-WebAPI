using Domain.Common;
using Application.DTOs.BlogDTOs;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BlogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseResult<List<GetBlogDto>>> GetAllBlogsAsync()
        {
            var result = await _unitOfWork.Blogs.GetAllEntitiesAsync(null, b => b.User, b => b.Category);
            var mappedResult = _mapper.Map<List<GetBlogDto>>(result);

            return new ResponseResult<List<GetBlogDto>>
            (
                true,
                null,
                mappedResult
            );
        }

        public async Task<ResponseResult<GetBlogDto>> GetBlogByIdAsync(int id)
        {
            var result = await _unitOfWork.Blogs.GetEntityByIdAsync(id, b => b.User, b => b.Category);
            
            if(result == null)
            {
                return new ResponseResult<GetBlogDto>
                (
                    false,
                    "Blog not found.",
                    null
                );
            }
            else
            {
                var mappedResult = _mapper.Map<GetBlogDto>(result);
                return new ResponseResult<GetBlogDto>
                (
                    true,
                    null,
                    mappedResult
                );
            }
        }

        public async Task<ResponseResult<List<GetBlogDto>>> GetBlogsByCategoryIdAsync(int categoryId)
        {
            var cat = await _unitOfWork.Categories.GetEntityByIdAsync(categoryId);

            if (cat == null)
            {
                return new ResponseResult<List<GetBlogDto>>
                (
                    false,
                    "Category not found.",
                    null
                );
            }
            else
            {
                var result = await _unitOfWork.Blogs.GetAllEntitiesAsync(b => b.CategoryId == categoryId, b => b.User, b => b.Category);
                var mappedResult = _mapper.Map<List<GetBlogDto>>(result);
                return new ResponseResult<List<GetBlogDto>>
                (
                    true,
                    null,
                    mappedResult
                );
            }
        }

        public async Task<ResponseResult<List<GetBlogDto>>> GetBlogsByUserIdAsync(string userId)
        {
            var result = await _unitOfWork.Blogs.GetAllEntitiesAsync(b => b.UserId == userId, b => b.User, b => b.Category);
            var mappedResult = _mapper.Map<List<GetBlogDto>>(result);
            return new ResponseResult<List<GetBlogDto>>
            (
                true,
                null,
                mappedResult
            );
        }

        public async Task<ResponseResult<CreateBlogDto>> CreateBlogAsync(CreateBlogDto createBlogDto, string userId)
        {
            var category = await _unitOfWork.Categories.FirstOrDefaultAsync(c => c.Name == createBlogDto.CategoryName);

            if (category == null)
            {
                return new ResponseResult<CreateBlogDto>
                (
                    false,
                    "Category not found.",
                    createBlogDto
                );
            }
            else
            {
                var mappedResult = _mapper.Map<Blog>(createBlogDto);
                mappedResult.CategoryId = category.Id;
                mappedResult.UserId = userId;

                await _unitOfWork.Blogs.AddEntityAsync(mappedResult);
                await _unitOfWork.SaveChangesAsync();

                return new ResponseResult<CreateBlogDto>
                (
                    true,
                    null,
                    createBlogDto
                );
            }
        }

        public async Task<ResponseResult<int>> DeleteBlogAsync(int id, string userId)
        {
            var result = await _unitOfWork.Blogs.GetEntityByIdAsync(id);

            if (result != null && result.UserId == userId)
            {
                await _unitOfWork.Blogs.DeleteEntityAsync(id);
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
                "Blog not found.",
                id
                );
            }
        }

        public async Task<ResponseResult<UpdateBlogDto>> UpdateBlogAsync(int id, UpdateBlogDto updateBlogDto, string userId)
        {
            var result = await _unitOfWork.Blogs.GetEntityByIdAsync(id);
            if (result != null && userId == result.UserId)
            {
                var category = await _unitOfWork.Categories.FirstOrDefaultAsync(c => c.Name == updateBlogDto.CategoryName);
                if (category != null)
                {
                    result.Title = updateBlogDto.Title;
                    result.Content = updateBlogDto.Content;
                    result.CategoryId = category.Id;
                    result.ImageUrl = updateBlogDto.ImageUrl;

                    _unitOfWork.Blogs.UpdateEntity(result);
                    await _unitOfWork.SaveChangesAsync();

                    return new ResponseResult<UpdateBlogDto>
                    (
                        true,
                        null,
                        updateBlogDto
                    );
                }
            }

            return new ResponseResult<UpdateBlogDto>
            (
                false,
                "Bad request.",
                updateBlogDto
            );
        }
    }
}