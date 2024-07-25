using AutoMapper;
using Domain.Models;
using TaskOfManagingLibrary.DTOs.BookDtos;

namespace TaskOfManagingLibrary.Profiles.BookMaping
{
    public class BookForCreateMap: Profile
    {
        public BookForCreateMap()
        {
            CreateMap<BookForCreateDto, Book>().ReverseMap();
        }

     
    }
}
