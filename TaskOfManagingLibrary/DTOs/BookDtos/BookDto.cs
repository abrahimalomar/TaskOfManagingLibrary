using Domain.Models;

namespace TaskOfManagingLibrary.DTOs.BookDtos
{
    public class BookDto
    {
        public string? Title { get; set; }
        public string? Isbn { get; set; }
        public int PageCount { get; set; }
        public string? Language { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
