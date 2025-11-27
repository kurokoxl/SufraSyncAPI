using SufraSyncAPI.Models.DTOs.CartDtos; 

namespace SufraSyncAPI.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetUserCart(string userId);
        Task<CartDto> AddToCart(string userId, int productId, int quantity);
        Task<CartDto> RemoveFromCart(string userId, int productId);
        Task<bool> ClearCart(string userId);
    }
}