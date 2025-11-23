using SufraSyncAPI.Models.DTOs;

namespace SufraSyncAPI.Services.Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync();
        Task<IngredientDto?> GetIngredientByIdAsync(int id);
        Task<IngredientDto?> AddIngredientAsync(CreateIngredientDto createDto);
        Task<IngredientDto?> UpdateIngredientAsync(int id, UpdateIngredientDto updateDto);
        Task<bool> DeleteIngredientAsync(int id);
    }
}