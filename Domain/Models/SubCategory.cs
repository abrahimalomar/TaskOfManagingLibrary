

namespace Domain.Models
{
    public class SubCategory
    {
        public int SubCategoryId { get; set; }
        public string? Name { get; set; }
        public int MainCategoryId { get; set; }

        //public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }

        public MainCategory MainCategory { get; set; }
    }

}
