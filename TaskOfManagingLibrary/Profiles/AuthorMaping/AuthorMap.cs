using AutoMapper;
using Domain.Models;

using TaskOfManagingLibrary.DTOs.AuthorDtos;

namespace TaskOfManagingLibrary.Profiles.AuthorMaping
{
    public class AuthorMap : Profile
    {
        public AuthorMap()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<PagedResult<Author>, PagedResult<AuthorDto>>()
    .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
        }
    }
}
