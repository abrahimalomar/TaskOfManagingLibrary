using AutoMapper;
using Domain.Models;
using TaskOfManagingLibrary.DTOs.AuthorDtos;

namespace TaskOfManagingLibrary.Profiles.AuthorMaping
{
    public class AuthorForCreateMap : Profile
    {
        public AuthorForCreateMap()
        {
            CreateMap<AuthorForCreateDto, Author>().ReverseMap();
        }
       
    }
}
