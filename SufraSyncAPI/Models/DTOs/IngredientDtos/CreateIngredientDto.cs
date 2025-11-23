using System.ComponentModel.DataAnnotations;

namespace SufraSyncAPI.Models.DTOs
{
    public class CreateIngredientDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Range(0, 100000)]
        public int Stock { get; set; }
    }
}