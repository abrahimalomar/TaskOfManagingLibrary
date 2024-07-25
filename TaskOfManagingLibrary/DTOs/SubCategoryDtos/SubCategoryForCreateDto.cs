using Domain.Models;

namespace TaskOfManagingLibrary.DTOs.SubCategoryDtos
{
    public class SubCategoryForCreateDto
    {

        public string? Name { get; set; }
        public int? MainCategoryId { get; set; }

    }
}
