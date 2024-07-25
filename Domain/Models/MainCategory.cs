

namespace Domain.Models
{
    public class MainCategory
    {
        public int MainCategoryId { get; set; }
        public string? Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        //public string? CreatedBy { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }


}
