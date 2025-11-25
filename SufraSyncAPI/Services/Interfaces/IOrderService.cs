using SufraSyncAPI.Models.DTOs.OrderDtos;

namespace SufraSyncAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto?> GetAllOrders();
        Task<OrderDto?> CreateOrder(string userId,CreateOrderDto createDto);
    }
}
