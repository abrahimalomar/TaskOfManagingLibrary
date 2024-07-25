

namespace Domain.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Isbn { get; set; } // رقم الكتاب الدولي الموحد
        

        public int PageCount { get; set; }
        public string? Language { get; set; }
        public decimal Price { get; set; }

        public int AuthorId { get; set; }
        public int MainCategoryId { get; set; }
        public int SubCategoryId { get; set; }


        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        //public string? CreatedBy { get; set; }

        public Author Author { get; set; }
        public MainCategory MainCategory { get; set; }
        public SubCategory SubCategory { get; set; }
    }

}
