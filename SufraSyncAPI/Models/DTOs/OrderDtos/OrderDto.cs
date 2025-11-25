using Microsoft.EntityFrameworkCore;
using SufraSyncAPI.Models.Entities;

namespace SufraSyncAPI.Models.DTOs.OrderDtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}