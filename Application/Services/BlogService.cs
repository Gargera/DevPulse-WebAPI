using Application.Common;
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

        public async Task<ResponseResult<List<GetBlogDto>>> GetAllBlogsDtosAsync()
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

        public async Task<ResponseResult<GetBlogDto>> GetBlogDtoByIdAsync(int id)
        {
            var result = await _unitOfWork.Blogs.GetEntityByIdAsync(id, b => b.User, b => b.Category);
            var mappedResult = _mapper.Map<GetBlogDto>(result);

            return new ResponseResult<GetBlogDto>
            (
                true,
                null,
                mappedResult
            );
        }

        public async Task<ResponseResult<CreateBlogDto>> CreateBlogDtoAsync(CreateBlogDto createBlogDto)
        {
            var mappedResult = _mapper.Map<Blog>(createBlogDto);
            await _unitOfWork.Blogs.AddEntityAsync(mappedResult);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseResult<CreateBlogDto>
            (
                true,
                null,
                createBlogDto
            );
        }

        public async Task<ResponseResult<int>> DeleteBlogDtoAsync(int id)
        {
            var result = await _unitOfWork.Blogs.GetEntityByIdAsync(id);

            if (result != null)
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

        public async Task<ResponseResult<UpdateBlogDto>> UpdateBlogDtoAsync(int id, UpdateBlogDto updateBlogDto)
        {
            var result = await _unitOfWork.Blogs.GetEntityByIdAsync(id);
            if (result != null)
            {
                result.Title = updateBlogDto.Title;
                result.Content = updateBlogDto.Content;
                result.CategoryId = updateBlogDto.CategoryId;
                result.ImagePath = updateBlogDto.ImagePath;

                _unitOfWork.Blogs.UpdateEntity(result);
                await _unitOfWork.SaveChangesAsync();

                return new ResponseResult<UpdateBlogDto>
                (
                    true,
                    null,
                    updateBlogDto
                );
            }
            else
            {
                return new ResponseResult<UpdateBlogDto>
                (
                    false,
                    "Blog not found.",
                    updateBlogDto
                );
            }
        }
    }
}