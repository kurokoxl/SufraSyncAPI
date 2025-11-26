using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SufraSync.Data;
using SufraSyncAPI.Models.DTOs.OrderDtos;
using SufraSyncAPI.Models.Entities;
using SufraSyncAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;


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
        
        public async Task<IEnumerable<OrderDto?>> GetAllOrders()
        {
            return _mapper.Map<IEnumerable < OrderDto >>(await _context.Orders
                .Include(o=>o.OrderProducts)
                .ThenInclude(op=>op.Product)
                .ToListAsync()
                );
        }
        public async Task<IEnumerable<OrderDto?>> GetAllUserOrders(string userId)
        {
            return _mapper.Map<IEnumerable<OrderDto>>(await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ToListAsync()
                );
        }

        public async Task<OrderDto?> GetOrder(int orderId, string userId, string role)
        {

            var order = new Order();
            if (role == "Admin")
            {
                order = await _context.Orders
               .Include(o => o.OrderProducts)
               .ThenInclude(op => op.Product)
               .FirstOrDefaultAsync(o => o.OrderId == orderId);


            }
            else
            {
                // Regular user only sees THEIR OWN order
                order = await _context.Orders
                    .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                    .FirstOrDefaultAsync(o => o.UserId == userId && o.OrderId == orderId);
            }

            if (order == null)
                throw new KeyNotFoundException("Can't find this order");

            return _mapper.Map<OrderDto?>(order);

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
                        Price = product.Price
                        
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

        public async Task<OrderDto?> AdvanceOrderStatus(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
                throw new KeyNotFoundException("Order not found");

            if (order.OrderStatus == OrderStatus.Delivered || order.OrderStatus == OrderStatus.Cancelled)
            {
                throw new InvalidOperationException($"Cannot advance status. Current status is {order.OrderStatus}");
            }

            order.OrderStatus++;

            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDto>(order);
        }
        public async Task<OrderDto> CancelOrder(int orderId, string userId, string userRole)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderProducts)
                        .ThenInclude(op => op.Product)
                            .ThenInclude(p => p.ProductIngredients)!
                                .ThenInclude(pi => pi.Ingredient)
                    .FirstOrDefaultAsync(o => o.OrderId == orderId);

                if (order == null)
                    throw new KeyNotFoundException("Order not found");

                
                if (userRole != "Admin" && order.UserId != userId)
                {
                    throw new UnauthorizedAccessException("You are not authorized to cancel this order.");
                }

                if (order.OrderStatus == OrderStatus.Delivered)
                    throw new InvalidOperationException("Cannot cancel a delivered order.");

                if (order.OrderStatus == OrderStatus.Cancelled)
                    throw new InvalidOperationException("Order is already cancelled.");


                order.OrderStatus = OrderStatus.Cancelled;

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