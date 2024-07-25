namespace TaskOfManagingLibrary.DTOs.BookDtos
{
    public class BookForCreateDto
    {
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int PageCount { get; set; }
        public string Language { get; set; }
        public decimal Price { get; set; }

        public int AuthorId { get; set; }
        public int MainCategoryId { get; set; }
        public int SubCategoryId { get; set; }

    }
}
