using SufraSyncAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SufraSyncAPI.Models.DTOs.CategoryDtos
{
    public class CategoryDto
    {
        [Required(ErrorMessage = "Category Id is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(100, MinimumLength = 3)]
        public string? Name { get; set; }
    }
}