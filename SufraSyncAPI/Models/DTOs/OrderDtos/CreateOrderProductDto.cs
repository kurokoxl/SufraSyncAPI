using System.ComponentModel.DataAnnotations;

namespace SufraSyncAPI.Models.DTOs.OrderDtos
{
    public class CreateOrderProductDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range (1,20, ErrorMessage = "Minimum quantity is 1 and maximum is 20 per order")]
        public int Quantity { get; set; }
 
    }
}
