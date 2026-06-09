using AutoMapper;
using Domain.Entities;
using Application.DTOs.BlogDTOs;
using Application.DTOs.CategoryDTOs;
using Application.DTOs.AccountDTOs;

namespace Application.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<CreateBlogDto, Blog>();
            CreateMap<Blog, GetBlogDto>()
            .ForMember(
                dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name)
            )
            .ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => src.User.UserName)
            );


            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, GetCategoryDto>();
            CreateMap<Category, GetCategoryWithoutBlogsDto>();

            CreateMap<ApplicationUser, GetUserAdminDto>();
        }
    }
}
