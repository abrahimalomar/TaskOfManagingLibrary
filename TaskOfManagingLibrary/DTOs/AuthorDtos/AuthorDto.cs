using Domain.Models;

namespace TaskOfManagingLibrary.DTOs.AuthorDtos
{
    public class AuthorDto
    {

        public string Name { get; set; }

        public string? AuthorInfo { get; set; }
        public bool? IsDeleted { get; set; } = false;


    }
}
