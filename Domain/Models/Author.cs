

namespace Domain.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string? Name { get; set; }

        public string? AuthorInfo { get; set; } // لمحه عن المؤلف
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        //public string? CreatedBy { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }

}
