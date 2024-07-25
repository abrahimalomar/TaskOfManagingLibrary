using AutoMapper;
using Domain.Models;
using TaskOfManagingLibrary.DTOs.SubCategoryDtos;

namespace TaskOfManagingLibrary.Profiles.SubCategoryMaping
{
    public class SubCategoryMap : Profile
    {
        public SubCategoryMap()
        {
            CreateMap<SubCategory, SubCategoryDto>();
            CreateMap<PagedResult<SubCategory>, PagedResult<SubCategoryDto>>()
 .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
        }
    }
}
