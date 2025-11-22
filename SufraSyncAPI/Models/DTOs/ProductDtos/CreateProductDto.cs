using Microsoft.EntityFrameworkCore;
using SufraSyncAPI.Models.Entities;
using System.ComponentModel.DataAnnotations; // Required namespace

namespace SufraSyncAPI.Models.DTOs.ProductDto
{

    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100, MinimumLength = 3)] // Must be between 3 and 100 chars
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(0.01, 10000)] // Price must be positive
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int? CategoryId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one ingredient is required.")] // Prevents empty []
        public List<CreateProductIngredientDto> Ingredients { get; set; }

    }
}
