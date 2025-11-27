using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SufraSync.Data;
using SufraSyncAPI.Models.DTOs.CartDtos;
using SufraSyncAPI.Models.Entities;
using SufraSyncAPI.Services.Interfaces;

namespace SufraSyncAPI.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CartService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CartDto> GetUserCart(string userId)
        {
            var cartItems = await _context.Set<CartItem>()
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return MapToCartDto(userId, cartItems);
        }

        public async Task<CartDto> AddToCart(string userId, int productId, int quantity)
        {
            // 1. Check if Product Exists
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new KeyNotFoundException("Product not found");

            // 2. Check if item already in cart
            var cartItem = await _context.Set<CartItem>()
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                // Add New
                cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.Set<CartItem>().Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return await GetUserCart(userId); // Return full updated cart
        }

        public async Task<CartDto> RemoveFromCart(string userId, int productId)
        {
            var cartItem = await _context.Set<CartItem>()
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem != null)
            {
                _context.Set<CartItem>().Remove(cartItem);
                await _context.SaveChangesAsync();
            }

            return await GetUserCart(userId);
        }

        public async Task<bool> ClearCart(string userId)
        {
            var items = await _context.Set<CartItem>()
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!items.Any()) return false;

            _context.Set<CartItem>().RemoveRange(items);
            await _context.SaveChangesAsync();
            return true;
        }

        private CartDto MapToCartDto(string userId, List<CartItem> items)
        {
            var itemDtos = _mapper.Map<List<CartItemDto>>(items);
            return new CartDto
            {
                UserId = userId,
                Items = itemDtos,
                TotalPrice = itemDtos.Sum(i => i.SubTotal)
            };
        }
    }
}