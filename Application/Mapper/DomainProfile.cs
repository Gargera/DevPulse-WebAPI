using Application.DTOs.BlogDTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Blog, GetBlogDto>()
                    .ForMember(
                        dest => dest.CategoryName,
                        opt => opt.MapFrom(src => src.Category.Name)
                    )
                    .ForMember(
                        dest => dest.UserName,
                        opt => opt.MapFrom(src => src.User.UserName)
                    );

            CreateMap<CreateBlogDto, Blog>();
        }
    }
}
