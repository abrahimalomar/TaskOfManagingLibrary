namespace TaskOfManagingLibrary.DTOs
{
    namespace Domain.Models
    {
        public class MainCategoryDto
        {
            public int? MainCategoryId { get; set; }
            public string? Name { get; set; }

            public string? Description { get; set; }
            public bool IsDeleted { get; set; }

        }


    }

}
