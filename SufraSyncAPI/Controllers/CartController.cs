using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using SufraSync.Controllers;
using SufraSyncAPI.Models.DTOs.CartDtos;
using SufraSyncAPI.Services.Interfaces;
using System.Security.Claims;

namespace SufraSyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Only logged-in users have carts
    public class CartController : BaseApiController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyCart()
        {
            var cart = await _cartService.GetUserCart(UserId);
            return Success(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddItem([FromBody] AddToCartDto dto)
        {
            try
            {
                var cart = await _cartService.AddToCart(UserId, dto.ProductId, dto.Quantity);
                return Success(cart, "Item added to cart");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFoundError<object>(ex.Message);
            }
        }

        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            var cart = await _cartService.RemoveFromCart(UserId, productId);
            return Success(cart, "Item removed from cart");
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            await _cartService.ClearCart(UserId);
            return Success(true, "Cart cleared successfully");
        }
    }
}