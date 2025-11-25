using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SufraSync.Data;
using SufraSyncAPI.Models.DTOs.OrderDtos;
using SufraSyncAPI.Models.Entities;
using SufraSyncAPI.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace SufraSyncAPI.Services
{    [Authorize]

    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        //protected string? UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
        public OrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetAllOrders()
        {
            return _mapper.Map<OrderDto>(await _context.Orders.ToListAsync());
        }

        public async Task<OrderDto?> CreateOrder(string userId, CreateOrderDto createDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //userid
                //create order
                var order = new Order
                {
                    OrderDate = DateTime.UtcNow,
                    UserId = userId,
                    OrderProducts = new List<OrderProduct>()
                };

                //loop each product in order
                foreach (var itemDto in createDto.OrderProducts)
                {
                    var product = await _context.Products
                    .Include(p => p.ProductIngredients)!
                    .ThenInclude(pi => pi.Ingredient)
                    .FirstOrDefaultAsync(p => p.ProductId == itemDto.ProductId);

                    if (product == null)
                        throw new ArgumentException($"Product ID {itemDto.ProductId} not found");

                    //create orderproduct
                    var orderProduct = new OrderProduct
                    {
                        ProductId = product.ProductId,
                        Quantity = itemDto.Quantity,
                        Price = product.Price,
                        
                        
                    };
                    order.OrderProducts.Add(orderProduct);

                    //calculate order cost = quantity * Price
                    order.TotalAmount += product.Price * itemDto.Quantity;

                    //track ingredient stock
                    foreach (var pingredient in product.ProductIngredients)
                    {
                        var ingredient = pingredient.Ingredient;

                        var totalIngredientNeeded = pingredient.QuantityRequired * itemDto.Quantity;
                        if (ingredient.Stock < totalIngredientNeeded)
                            throw new InvalidOperationException($"Ingredient {ingredient.Name} is out of stock");

                        ingredient.Stock -= totalIngredientNeeded;
                    }
                }

                //add order and save
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return _mapper.Map<OrderDto>(order);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }

}