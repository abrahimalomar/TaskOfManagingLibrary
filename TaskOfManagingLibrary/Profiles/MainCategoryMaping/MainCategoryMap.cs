using AutoMapper;
using Domain.Models;
using TaskOfManagingLibrary.DTOs.Domain.Models;

namespace TaskOfManagingLibrary.Profiles.MainCategoryMaping
{
    public class MainCategoryMap : Profile
    {

        public MainCategoryMap()
        {
            CreateMap<MainCategory, MainCategoryDto>().ReverseMap();
            CreateMap<PagedResult<MainCategory>, PagedResult<MainCategoryDto>>()
             .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
        }
    }
}
