using Microsoft.AspNetCore.Mvc;
using SufraSyncAPI.Models.Responses;

namespace SufraSync.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected IActionResult Success<T>(T data, string message = "")
        {
            return Ok(new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            });
        }

        protected IActionResult CreatedSuccess<T>(string routeName, object routeValues, T data, string message = "")
        {
            return CreatedAtAction(routeName, routeValues, new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            });
        }

        protected IActionResult NotFoundError<T>(string message)
        {
            return NotFound(new ApiResponse<T>
            {
                Success = false,
                Message = message
            });
        }

        protected IActionResult ConflictError<T>(string message)
        {
            return Conflict(new ApiResponse<T>
            {
                Success = false,
                Message = message
            });
        }

        protected IActionResult BadRequestError<T>(string message)
        {
            return BadRequest(new ApiResponse<T>
            {
                Success = false,
                Message = message
            });
        }
    }
}