using System.ComponentModel.DataAnnotations;

namespace SufraSyncAPI.Models.DTOs.CategoryDtos
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(100, MinimumLength = 3)]
        public string? Name { get; set; }
    }
}
