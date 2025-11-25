using Microsoft.EntityFrameworkCore;
using SufraSyncAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SufraSyncAPI.Models.DTOs.OrderDtos
{
    public class CreateOrderDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "At least one Product is required.")]
        public ICollection<CreateOrderProductDto> OrderProducts { get; set; }
    }
}