using SufraSyncAPI.Models.DTOs.OrderDtos;

namespace SufraSyncAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto?>> GetAllOrders();
        Task<IEnumerable<OrderDto?>> GetAllUserOrders(string userId);
        Task<OrderDto?> GetOrder(int orderId, string userId, string role);

        Task<OrderDto?> CreateOrder(string userId,CreateOrderDto createDto);
        Task<OrderDto?> AdvanceOrderStatus(int orderId);

        Task<OrderDto> CancelOrder(int orderId, string userId, string userRole);
    }
}
