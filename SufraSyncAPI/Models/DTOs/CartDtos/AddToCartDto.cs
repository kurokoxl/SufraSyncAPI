using System.ComponentModel.DataAnnotations;

namespace SufraSyncAPI.Models.DTOs.CartDtos
{
    public class AddToCartDto
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, 50, ErrorMessage = "Quantity must be between 1 and 50")]
        public int Quantity { get; set; }
    }
}