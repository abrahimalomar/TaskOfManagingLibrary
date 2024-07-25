using AutoMapper;
using Domain.Models;
using TaskOfManagingLibrary.DTOs.MainCategoryDtos;

namespace TaskOfManagingLibrary.Profiles.MainCategoryMaping
{
    public class MainCategoryForCreateMap: Profile
    {
        public MainCategoryForCreateMap()
        {
            CreateMap<MainCategoryForCreateDto, MainCategory>().ReverseMap();
        }
    }
}
