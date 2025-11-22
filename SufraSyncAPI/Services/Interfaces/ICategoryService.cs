using SufraSyncAPI.Models.DTOs.CategoryDtos;

namespace SufraSyncAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategories();
        Task<IEnumerable<CategoryDto>> GetCategory(int CategoryId);
    }
}
