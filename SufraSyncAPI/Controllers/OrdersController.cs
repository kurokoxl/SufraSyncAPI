using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SufraSync.Controllers;
using SufraSyncAPI.Models.DTOs.OrderDtos;
using SufraSyncAPI.Models.Responses;
using SufraSyncAPI.Services;
using SufraSyncAPI.Services.Interfaces;
using System.Security.Claims;

namespace SufraSyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseApiController
    {
        protected string? UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
        private readonly IOrderService _orderService;
        protected string? UserRole => User.FindFirstValue(ClaimTypes.Role);
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetOrders()
        {
            return Success(await _orderService.GetAllOrders());
        }

        [HttpGet("{orderId}")]
        [Authorize]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            try
            {
                return Success(await _orderService.GetOrder(orderId, UserId, UserRole)); 
            }
            catch (Exception ex)
            {
                return BadRequestError<object>(ex.Message);
            }
            
        }

        [HttpGet("my-orders")]
        [Authorize]
        public async Task<IActionResult> GetUserOrders()
        {

            return Success(await _orderService.GetAllUserOrders(UserId!));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MakeOrder([FromBody]CreateOrderDto createDto)
        {
            //if (string.IsNullOrEmpty(UserId))
            //    return Unauthorized(new ApiResponse<object> { Success = false, Message = "Invalid user token" });
            try
            {
               return Success(await _orderService.CreateOrder(UserId!, createDto));
            }
            catch (InvalidOperationException ex)
            {
                return ConflictError<object>(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequestError<object>(ex.Message);
            }
        }
        [HttpPut("{id}/advance-status")]
        [Authorize(Roles = "Admin")] // Staff/Admin only
        public async Task<IActionResult> AdvanceStatus(int id)
        {
            try
            {
                var order = await _orderService.AdvanceOrderStatus(id);
                return Success(order, "Order status updated");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequestError<object>(ex.Message);
            }
        }
        [HttpPut("{id}/cancel")]
        [Authorize]
        public async Task<IActionResult> CancelOrder(int id)
        {
            try
            {
                // Pass ID and Role to the service
                var order = await _orderService.CancelOrder(id, UserId, UserRole);

                return Success(order, "Order cancelled successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFoundError<object>(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequestError<object>(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new ApiResponse<object>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

    }
}
