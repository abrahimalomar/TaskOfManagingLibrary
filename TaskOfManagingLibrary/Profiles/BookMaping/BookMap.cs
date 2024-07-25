using AutoMapper;
using Domain.Models;
using TaskOfManagingLibrary.DTOs.BookDtos;

namespace TaskOfManagingLibrary.Profiles.BookMaping
{
    public class BookMap : Profile
    {
        public BookMap()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<PagedResult<Book>, PagedResult<BookDto>>()
             .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
        }
    }
}
