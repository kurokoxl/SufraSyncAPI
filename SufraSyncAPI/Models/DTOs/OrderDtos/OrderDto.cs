using Microsoft.EntityFrameworkCore;
using SufraSyncAPI.Models.Entities;

namespace SufraSyncAPI.Models.DTOs.OrderDtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string? UserId { get; set; }
        public List<OrderProductDto> OrderProducts { get; set; }
    }
}