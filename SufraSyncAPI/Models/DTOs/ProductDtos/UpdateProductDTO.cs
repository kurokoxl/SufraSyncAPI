using System.ComponentModel.DataAnnotations;

namespace SufraSyncAPI.Models.DTOs.ProductDto
{
    public class UpdateProductDTO
    {
        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(0.01, 10000)]
        public decimal Price { get; set; }

        [Required]
        public int? CategoryId { get; set; }
    }
}
