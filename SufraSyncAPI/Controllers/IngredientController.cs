using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SufraSync.Controllers;
using SufraSyncAPI.Models.DTOs;
using SufraSyncAPI.Services.Interfaces;

namespace SufraSyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : BaseApiController
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetIngredients()
        {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            return Success(ingredients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredient(int id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null) return NotFoundError<IngredientDto>("Ingredient not found");

            return Success(ingredient);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddIngredient(CreateIngredientDto createDto)
        {
            var created = await _ingredientService.AddIngredientAsync(createDto);

            if (created == null)
                return ConflictError<IngredientDto>("Ingredient with this name already exists");

            return CreatedSuccess(nameof(GetIngredient), new { id = created.IngredientId }, created, "Ingredient added successfully");
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredient(int id, UpdateIngredientDto updateDto)
        {
            var updated = await _ingredientService.UpdateIngredientAsync(id, updateDto);

            if (updated == null)
                return NotFoundError<IngredientDto>("Ingredient not found");

            return Success(updated, "Ingredient updated successfully");
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            try
            {
                var success = await _ingredientService.DeleteIngredientAsync(id);
                if (!success) return NotFoundError<bool>("Ingredient not found");

                return Success(true, "Ingredient deleted successfully");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequestError<bool>(ex.Message); // Returns error if used in a recipe
            }
        }
    }
}