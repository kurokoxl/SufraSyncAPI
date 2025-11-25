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
        
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Success(await _orderService.GetAllOrders());
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MakeOrder([FromBody]CreateOrderDto createDto)
        {
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized(new ApiResponse<object> { Success = false, Message = "Invalid user token" });
            try
            {
               return Success(await _orderService.CreateOrder(UserId, createDto));
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
    }
}
