using Microsoft.AspNetCore.Mvc;
using SufraSyncAPI.Services.Interfaces;
using SufraSync.Controllers;
using SufraSyncAPI.Models.DTOs.CategoryDtos;
namespace SufraSyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Success(await _categoryService.GetAllCategories());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            return Success(await _categoryService.GetCategory(id));
        }
        // 1. Add the Attribute and Route ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            if (id != categoryDto.CategoryId)
            {
                return BadRequestError<object>("URL ID does not match Body ID");
            }

            var updatedCategory = await _categoryService.UpdateCategory(categoryDto);

            if (updatedCategory == null)
            {
                return NotFoundError<object>("No category with this id");
            }

            return Success(updatedCategory);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDto categoryDto)
        {
             var createdCategory = await _categoryService.AddCategory(categoryDto);
            if (createdCategory == null)
                return ConflictError<object>("Category With The Same Name Already Exisits");

            return CreatedSuccess(nameof(GetCategory),new {id=createdCategory.CategoryId},createdCategory);
        }

    }
}
