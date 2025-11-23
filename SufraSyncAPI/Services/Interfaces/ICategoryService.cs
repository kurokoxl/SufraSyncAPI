using Microsoft.AspNetCore.Mvc;
using SufraSyncAPI.Models.DTOs.CategoryDtos;
using SufraSyncAPI.Models.Entities;

namespace SufraSyncAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategories();
        Task<CategoryDto> GetCategory(int categoryId);
        Task<CategoryDto> UpdateCategory(CategoryDto categoryDto);

        Task<CategoryDto> AddCategory(CreateCategoryDto categoryDto);
        Task<bool> DeleteCategory(int id);
    }
}
