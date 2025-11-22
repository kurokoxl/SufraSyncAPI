using System.ComponentModel.DataAnnotations;

namespace SufraSyncAPI.Models.DTOs.ProductDto
{
    public class UpdateProductIngredientDto
    {
        [Required]
        public decimal? QuantityRequired { get; set; }
        [Required]
        public string Unit { get; set; }
    }
}