using AutoMapper;
using Domain.Models;
using TaskOfManagingLibrary.DTOs.SubCategoryDtos;

namespace TaskOfManagingLibrary.Profiles.SubCategoryMaping
{
    public class SubCategoryForCreateMap: Profile
    {
        public SubCategoryForCreateMap()
        {
            CreateMap<SubCategoryForCreateDto, SubCategory>().ReverseMap();
        }
    }
}
